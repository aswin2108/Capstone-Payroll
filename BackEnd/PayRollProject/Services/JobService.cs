using PayRollProject.Models;
using PayRollProject.Repository;
using PayRollProject.Services.Interfaces;
using System.Globalization;
using System.Net.Mail;
using System.Net;
using System.Net;
using System.Net.Mail;
using System.Text;

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

            DateTime currentDate = DateTime.Today;
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
                salaryCreditService.InsertCreditAndTaxDetails(employee.UserName, currentDate, creditedAmount, taxAmt, salaryCreditRepository, transactionId);
                userService.UpdateOvertimeToZero(employee.UserName);

                SendEmail("aswins2108@gmail.com", "test1", "Sal credited, how is it....?");
                // Increment the salary credit date for the employee to the next possible one
                // userService.IncrementSalaryCreditDate(employee);

                // Insert the employee's username, salary credit amount, and tax count into the specified table
                // userService.InsertSalaryCreditData(employee.Username, employee.SalaryCreditAmount, employee.TaxCount);
            }
        }

        public static void SendEmail(string recipientEmail, string subject, string body)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();

            try
            {
                // Set the sender's email address
                message.From = new MailAddress("sender@example.com");
                // Set the recipient's email address
                message.To.Add(new MailAddress(recipientEmail));

                // Set the email subject and body
                message.Subject = subject;
                message.Body = body;

                // Configure the SMTP client
                smtpClient.Host = "smtp.example.com";
                smtpClient.Port = 587; // Use the appropriate SMTP port
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("username", "password");

                // Send the email
                smtpClient.Send(message);
            }
            finally
            {
                // Dispose of resources
                message.Dispose();
                smtpClient.Dispose();
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
