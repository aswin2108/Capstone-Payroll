using PayRollProject.Models;
using PayRollProject.Repository;

namespace PayRollProject.Services.Interfaces
{
    public interface IUserDetailsService
    {
        public IEnumerable<UserDetails> GetAllUsers();
        public UserDetails GetUserByUserName(string UserName);
        public void UpdateUserBonus(string UserName, int newBonus);
        public void UpdateUserExcemptionAmtAndTaxPercent(string UserName, int newExcemptionAmt, int overTime, int salary);
        public void UpdateUserNextPayDate(string UserName, DateTime newNextPayDate);

        public void CalculateNextPayDate(string UserName, DateTime currentNextPayDate, int PayFreq, UserRepository userRepository);
        public void InsertUserDetails(UserDetails user);
        public void DeleteUserByUserName(string UserName);

        public void UpdateUserDetails(UserDetails user);

        public List<UserDetails> GetEmployeesToCreditSalaries(DateTime CurrentDate, UserRepository userRepository);
        public List<UserDetails> GetUsersByDate(DateTime date);
        public void UpdateOvertimeToZero(string UserName);
    }
}
