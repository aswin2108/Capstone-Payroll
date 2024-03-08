using PayRollProject.Models;
using PayRollProject.Repository;
using PayRollProject.Services.Interfaces;
using System.Globalization;
using System.Net.Mail;
using System.Net;
using System.Net;
using System.Net.Mail;
using System.Text;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;
using System.Diagnostics;
using sib_api_v3_sdk.Client;

namespace PayRollProject.Services
{
    public class JobService: IJobService
    {
        public static void CreditSalaries(UserDetailsService userService, SalaryCreditService salaryCreditService, UserRepository userRepository, SalaryCreditRepository salaryCreditRepository)
        {
            // Get the current date and credit salaries for employees with today's credit date

           // DateTime currentDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
           // string currentDate_Formatted = currentDate.ToString("yyyy-MM-dd");
            //Console.WriteLine(currentDate);

            DateTime currentDate = DateTime.Now;
            //string formattedDate = currentDate.ToString("yyyy-MM-dd");
            //DateOnly parsedDate = DateTime.ParseExact(formattedDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            List<UserDetails> employees = userService.GetEmployeesToCreditSalaries(currentDate, userRepository);

            foreach (var employee in employees)
            {
                Console.WriteLine("working...");
                userService.CalculateNextPayDate(employee.UserName, currentDate, employee.PayFreq, userRepository);
                decimal annualEarning = ((employee.Salary * 12) - employee.ExcemptionAmt+ employee.Bonus + (employee.OverTime*700));
                decimal taxAmt;
                if (annualEarning <= 250000) taxAmt = 0;
                else if (annualEarning <= 500000) taxAmt = ((annualEarning * 5) / 100)/12;
                else if ((annualEarning <= 1000000)) taxAmt = ((annualEarning * 20) / 100)/12;
                else taxAmt = ((annualEarning * 30) / 100) / 12;

                decimal creditedAmount = (employee.Salary-taxAmt);
                string transactionId = GenerateTransactionId();
                Console.Write(transactionId);
                salaryCreditService.InsertCreditAndTaxDetails(employee.UserName, currentDate, creditedAmount, taxAmt, salaryCreditRepository, transactionId, employee.ExcemptionAmt,employee.Bonus, employee.OverTime);
                userService.UpdateOvertimeToZeroJobs(employee.UserName, userRepository);

                string senderEmail = "quixar@mail.com";
                string senderName = "Quixar";
                string subject = "Salary Credited";
                 string message = "Dear Employee,\r\n\r\nWe are pleased to inform you that your salary has been successfully credited to your bank account.\r\n\r\nContact our HR department if you have any questions or concerns.\r\n\r\nThank you for your hard work and dedication. We appreciate your contributions to our organization.\r\n\r\nBest regards, Quixar";
              //  string message = $@"Dear Employee,

               //   We are pleased to inform you that your salary has been successfully credited to your bank account.

               //   Contact our HR department if you have any questions or concerns.

                 // Thank you for your hard work and dedication. We appreciate your contributions to our organization.

              //    Best regards,
             //     Quixar";

                SendEmail(senderEmail, senderName, employee.Email,employee.FirstName+employee.LastName, subject, message);

                //SendEmail("aswins2108@gmail.com", "test1", "Sal credited, how is it....?");
                // Increment the salary credit date for the employee to the next possible one
                // userService.IncrementSalaryCreditDate(employee);

                // Insert the employee's username, salary credit amount, and tax count into the specified table
                // userService.InsertSalaryCreditData(employee.Username, employee.SalaryCreditAmount, employee.TaxCount);
            }
        }

        public static void SendEmail(string senderEmail, string senderName, string recieverMail, string recieverName, string subject, string message)
        {
            var apiInstance = new TransactionalEmailsApi();

            SendSmtpEmailSender sender = new SendSmtpEmailSender(senderName, senderEmail);

            SendSmtpEmailTo reciever1 = new SendSmtpEmailTo(recieverMail, recieverName);
            List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
            To.Add(reciever1);

            string HtmlContent = null;
            string TextContent = message;
            //string Subject = "My {{params.subject}}";

            try
            {
                var sendSmtpEmail = new SendSmtpEmail(sender, To, null, null, HtmlContent, TextContent, subject);
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);

                Console.WriteLine("Brevo response: " + result.ToJson());

            }
            catch (Exception e)
            {
                Debug.WriteLine("We have an exception: " + e.Message);
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }

        public static string GenerateTransactionId()
        {
            string idChars = "AB&!CDE%#F-G^@HI-J$KL-*M-N-OP!Q-R-ST-&^#UV-W-XYZab-c#-*d@efghijklm&-nopqr$^s*%tuv-wxyz@#0*1-234-*5678-9";
            StringBuilder newId = new StringBuilder();

            Random random = new Random();
            for (int i = 0; i < 30; i++)
            {
                newId.Append(idChars[random.Next(0, idChars.Length)]);
            }

            return newId.ToString();
        }
    }
}
