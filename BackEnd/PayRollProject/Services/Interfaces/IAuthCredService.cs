using PayRollProject.Models;
using System.IdentityModel.Tokens.Jwt;

namespace PayRollProject.Services.Interfaces
{
    public interface IAuthCredService
    {
        public string GetUserDetails(string UserName, string password);
        public string HashThePassword(string Password, string Salt);
        public Boolean ValidatePassword(AuthCred currentUser, string Password);
        public string JWTTokenGenerator(string UserName, int roleType);
        public string InsertUser(string UserName, string Password, int Role);
    }
}
