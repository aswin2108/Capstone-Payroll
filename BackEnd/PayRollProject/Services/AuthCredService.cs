using PayRollProject.Models;
using PayRollProject.Repository.Interfaces;
using PayRollProject.Services.Interfaces;
using BCrypt.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;



namespace PayRollProject.Services
{
    public class AuthCredService : IAuthCredService
    {
        private readonly IAuthCredRepository _authCredRepository;
        public AuthCredService(IAuthCredRepository authCredRepository)
        {
            _authCredRepository = authCredRepository;
        }
        public string GetUserDetails(string UserName, string Password)
        {
            AuthCred currentUser= _authCredRepository.GetUserDetails(UserName);

            if (currentUser.UserName == null) return null;

            if(ValidatePassword(currentUser, Password))
            {
                return JWTTokenGenerator(UserName, currentUser.UserRole);
            }

            return null;
        }

        public string InsertUser(string UserName, string Password, int Role) 
        {
            AuthCred authCred = new AuthCred();
            if(GetUserDetails(UserName, Password)!=null)
            {
                return "User Exist";
            }
            authCred.UserName = UserName;
            authCred.Salt= BCrypt.Net.BCrypt.GenerateSalt();
            authCred.HashedPassword=HashThePassword(Password, authCred.Salt);
            authCred.UserRole = Role;
            _authCredRepository.InsertUser(authCred);
            return "User Created";
        }

        public string HashThePassword(string Password, string Salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(Password, Salt);
        }

        public Boolean ValidatePassword(AuthCred currentUser, string Password) 
        {
            return (currentUser.HashedPassword == HashThePassword(Password, currentUser.Salt));
        }

        public string JWTTokenGenerator(string UserName, int roleType)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            // Build the configuration
            IConfigurationRoot configuration = builder.Build();
            string secretKey = configuration.GetSection("AppSettings:SecretKey").Value;

            // Get the secret key from the configuration
            //string secretKey = configuration.GetSection("AppSettings:SecretKey").Value;

            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(secretKey);
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {

            //new Claim(ClaimTypes.Name, UserName), // Add UserName claim

            //(roleType == 0) ? new Claim(ClaimTypes.Role, "Employee") :
            //(roleType == 1) ? new Claim(ClaimTypes.Role, "HR") :
            //new Claim(ClaimTypes.Role, "Admin"),
            //new Claim("aud", configuration["AppSettings:ValidAudience"]),
            //new Claim("iss", configuration["AppSettings:ValidIssuer"]),

            //    }),
            //    Expires = DateTime.UtcNow.AddDays(5), // Token expiration date/time
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};

            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //var jwtToken = tokenHandler.WriteToken(token);

            //// Parse the token string to JwtSecurityToken
            //var parsedToken = tokenHandler.ReadJwtToken(jwtToken);

            //return parsedToken;

            Claim roleClaim;
            if (roleType == 0)
            {
                roleClaim = new Claim("role", "Employee");
            }
            else if (roleType == 1)
            {
                roleClaim = new Claim("role", "HR");
            }
            else
            {
                roleClaim = new Claim("role", "Admin");
            }

            //Claim claim1 = new Claim("role", "Admin");
            //Claim claim2 = new Claim("role", "Employee");
            //Claim claim3 = new Claim("role", "HR");
            Claim userNameClaim = new Claim("unique_name", UserName);
            Claim audClaim = new Claim("aud", "http://localhost:4200");
            Claim issClaim = new Claim("iss", "https://localhost:7125");
            List<Claim> claims = new List<Claim>
            { roleClaim,userNameClaim,audClaim,issClaim};

            var exp = DateTime.UtcNow.AddDays(1);
            var c = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("6Gd#!9hsFWT72bds0=-11YQHS&2@9sa*VQG*")), SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(claims: claims,expires:exp, signingCredentials: c);

         var   tokken = new JwtSecurityTokenHandler().WriteToken(token);
            return tokken;
           
        }

    }
}
