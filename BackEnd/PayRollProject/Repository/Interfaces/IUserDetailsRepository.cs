using PayRollProject.Models;

namespace PayRollProject.Repository.Interface
{
    public interface IUserDetailsRepository
    {
        public List<UserDetails> GetAllUsers();

        public UserDetails GetUserByUserName(string UserName);

        public void UpdateUserBonus(string UserName, int newBonus);
        public void UpdateUserExcemptionAmtAndTaxPercent(string UserName, int newExcemptionAmt, int newTaxPercent, int overTime);

        public void UpdateUserNextPayDate(string UserName, DateTime newNextPayDate);

        public void InsertUserDetails(UserDetails user);
        public void DeleteUserByUserName(string UserName);
        public List<UserDetails> GetUsersByDate(DateTime date);
        public void UpdateOvertimeToZero(string UserName);
        public void UpdateUserDetails(UserDetails user);
    }
}
