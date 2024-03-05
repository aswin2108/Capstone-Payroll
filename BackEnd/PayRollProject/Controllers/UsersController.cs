using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayRollProject.Models;
using PayRollProject.Services;

namespace PayRollProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserDetailsService _userDetailsService;

        public UsersController(UserDetailsService userDetailsService)
        {
            _userDetailsService = userDetailsService;
        }



        //[Authorize(Roles = "HR")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
       // [Authorize]
        [Authorize(Roles = "Admin, HR")]
        [HttpGet("allUsers")]
        public IActionResult GetUsersDetails()
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            Console.WriteLine($"JWT:  {token}");
            IEnumerable<UserDetails> userList = _userDetailsService.GetAllUsers();
            return Ok(userList);
        }

        [HttpGet("byDate")]
        public IActionResult GetUsersByDate(DateTime date)
        {
            List<UserDetails> userList = _userDetailsService.GetUsersByDate(date);
            return Ok(userList);
        }

        [Authorize(Roles ="Admin, HR, Employee")]
        [HttpGet("UserName/{UserName}")]
        public IActionResult GetUserByUserName(string UserName) {
            UserDetails user=_userDetailsService.GetUserByUserName(UserName);
            return Ok(user);
        }

        [Authorize(Roles = "Employee")]
        [HttpPut("{UserName}/Bonus")]
        public IActionResult UpdateUserBonus(string UserName, int Bonus)
        {
            _userDetailsService.UpdateUserBonus(UserName, Bonus);
            return Ok();
        }

        [Authorize(Roles = "Employee, HR, Admin")]
        [HttpPut("UserName/newExcemptionAmt/newTaxPercent")]
        public IActionResult UpdateUserExcemptionAmtAndTaxPercent([FromBody]UserDetails user)
        {
            _userDetailsService.UpdateUserExcemptionAmtAndTaxPercent(user.UserName, user.ExcemptionAmt, user.OverTime, user.Salary);
            return Ok();
        }

        [HttpPut("{UserName}/newNextPayDate")]
        public IActionResult UpdateUserNextPayDate(string UserName, DateTime newNextPayDate)
        {
            _userDetailsService.UpdateUserNextPayDate(UserName, newNextPayDate);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add-user-details")]
        public IActionResult InsertUserDetails([FromBody] UserDetails user)
        {
                Console.WriteLine(user.UserName);
                Console.WriteLine(user.DOB);
            try
            {

                _userDetailsService.InsertUserDetails(user);
                return Ok("New user inserted successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "An error occurred while inserting user details.");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("user/delete")]
        public IActionResult DeleteUserByUserName(string UserName)
        {
            try
            {
                _userDetailsService.DeleteUserByUserName(UserName);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "An error occurred while inserting user details.");
            }
        }

        
        [HttpPut("user/overTimeAndBonusZero")]
        public IActionResult UpdateOvertimeToZero(string UserName)
        {
            _userDetailsService.UpdateOvertimeToZero(UserName);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("userDetails/edit")]
        public IActionResult UpdateUserDetails(UserDetails user)
        {
            _userDetailsService.UpdateUserDetails(user);
            return Ok();  
        }
    }
} 
