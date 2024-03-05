using PayRollProject.Models;

namespace PayRollProject.Repository.Interfaces
{
    public interface IAuthCredRepository
    {
        public AuthCred GetUserDetails(string UserName);
        public void InsertUser(AuthCred authCred);
    }
}
