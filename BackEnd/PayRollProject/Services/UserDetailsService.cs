using PayRollProject.Models;
using PayRollProject.Repository;
using PayRollProject.Repository.Interface;
using PayRollProject.Services.Interfaces;

namespace PayRollProject.Services
{
    public class UserDetailsService : IUserDetailsService
    {
        private readonly IUserDetailsRepository _userDetailsRepository;
        public UserDetailsService(IUserDetailsRepository userDetailsRepository)
        {
            _userDetailsRepository = userDetailsRepository;
        }
        public IEnumerable<UserDetails> GetAllUsers()
        {
            return _userDetailsRepository.GetAllUsers().ToList();
        }

        public UserDetails GetUserByUserName(string UserName)
        {
            return _userDetailsRepository.GetUserByUserName(UserName);
        }

        public void UpdateUserBonus(string UserName, int newBonus)
        {
            _userDetailsRepository.UpdateUserBonus(UserName, newBonus);
            return;
        }

        public void UpdateUserExcemptionAmtAndTaxPercent(string UserName, int newExcemptionAmt, int overTime, int salary)
        {
            _userDetailsRepository.UpdateUserExcemptionAmtAndTaxPercent(UserName, newExcemptionAmt, calculateNewTax(overTime,salary,newExcemptionAmt), overTime);
            return;
        }

        public void UpdateUserNextPayDate(string UserName, DateTime newNextPayDate)
        {

            _userDetailsRepository.UpdateUserNextPayDate(UserName, newNextPayDate);
            return;
        }

        public void InsertUserDetails(UserDetails user)
        {
            _userDetailsRepository.InsertUserDetails(user);
            return;
        }

        public void DeleteUserByUserName(string UserName)
        {
            _userDetailsRepository.DeleteUserByUserName(UserName);
            return;
        }

        public List<UserDetails> GetUsersByDate(DateTime date)
        {
            Console.WriteLine(date.ToString());
            return _userDetailsRepository.GetUsersByDate(date);
        }

        public List<UserDetails> GetEmployeesToCreditSalaries(DateTime CurrentDate, UserRepository userRepository)
        {
            Console.WriteLine(CurrentDate);
            List<UserDetails>user= userRepository.GetUsersByDate(CurrentDate);
            return user;
        }

        public void UpdateUserDetails(UserDetails user)
        {
            _userDetailsRepository.UpdateUserDetails(user);
            return;
        }

        public void UpdateOvertimeToZero(string UserName)
        {
            _userDetailsRepository.UpdateOvertimeToZero(UserName);
        }

        public int calculateNewTax(int overTime, int salary, int newExcemptionAmt)
        {
            int newTax = 0;
            int annualMoneyLeft = (salary * 12) - newExcemptionAmt + (overTime * 700);
            if (annualMoneyLeft < 250000) newTax = 0;
            else if (annualMoneyLeft < 500000) newTax = 5;
            else if (annualMoneyLeft < 1000000) newTax = 20;
            else newTax = 30;
            return newTax;  
        }

        public void CalculateNextPayDate(string UserName, DateTime currentNextPayDate, int PayFreq, UserRepository userRepository)
        {
            int currentDay = currentNextPayDate.Day;
            int currentMonth = currentNextPayDate.Month;
            int currentYear = currentNextPayDate.Year;

            int daysInMonth = DateTime.DaysInMonth(currentYear, currentMonth);

            if (PayFreq == 1)
            {
                if (currentDay == 21)
                {
                    userRepository.UpdateUserNextPayDate(UserName, new DateTime(currentYear, currentMonth, daysInMonth));
                }
                else if (currentDay == daysInMonth)
                {
                    int nextMonth = currentMonth + 1;
                    int nextYear = currentYear;
                    if (nextMonth > 12)
                    {
                        nextMonth = 1;
                        nextYear++;
                    }
                    userRepository.UpdateUserNextPayDate(UserName, new DateTime(nextYear, nextMonth, 7));
                }
                else
                    userRepository.UpdateUserNextPayDate(UserName, currentNextPayDate.AddDays(7));
            }
            else if (PayFreq == 2)
            {
                if (currentDay == 14)
                    userRepository.UpdateUserNextPayDate(UserName, new DateTime(currentYear, currentMonth, daysInMonth));
                else
                {
                    int nextMonth = currentMonth + 1;
                    int nextYear = currentYear;
                    if (nextMonth > 12)
                    {
                        nextMonth = 1;
                        nextYear++;
                    }
                    userRepository.UpdateUserNextPayDate(UserName, new DateTime(nextYear, nextMonth, 14));
                }
            }
            else
            {
                int nextMonth = currentMonth + 1;
                int nextYear = currentYear;
                if (nextMonth > 12)
                {
                    nextMonth = 1;
                    nextYear++;
                }
                userRepository.UpdateUserNextPayDate(UserName, new DateTime(nextYear, nextMonth, DateTime.DaysInMonth(nextYear, nextMonth)));

            }
        }


    }
}
