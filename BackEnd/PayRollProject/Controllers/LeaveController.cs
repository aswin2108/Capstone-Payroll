using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayRollProject.Models;
using PayRollProject.Services;
using PayRollProject.Services.Interfaces;

namespace PayRollProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController:ControllerBase
    {
        private readonly LeaveDetailsService _leaveDetailsService;

        public LeaveController(LeaveDetailsService leaveDetailsService)
        {
            _leaveDetailsService = leaveDetailsService;
        }

        [HttpGet("{UserName}")]
        //[Authorize(Policy = "AdminHrEmployee")]
        public IActionResult GetLeaveDetails(string UserName)
        {
            LeaveDetails userLeave = _leaveDetailsService.GetLeaveDetails(UserName);

            return Ok(userLeave);
        }

        [HttpPost("addLeave")]
        [Authorize(Policy = "AdminHrEmployee")]
        public IActionResult CreateLeaveDetails([FromBody] LeaveDetails leaveDetails)
        {
            _leaveDetailsService.CreateLeaveDetails(leaveDetails);

            return CreatedAtAction(nameof(GetLeaveDetails), new { UserName = leaveDetails.UserName }, leaveDetails);
        }

        [HttpPut("updateLeave/UserName")]
        [Authorize(Policy = "AdminHrEmployee")]
        public IActionResult UpdateLeaveDetails([FromBody] LeaveDetails leaveDetails)
        {
            // Get the existing leave details for the specified user from the database
            LeaveDetails newLeaveDetails = _leaveDetailsService.GetLeaveDetails(leaveDetails.UserName);

            if (newLeaveDetails == null)
            {
                return NotFound(); // Return 404 Not Found if user is not found
            }

            // Update the leave details
            newLeaveDetails.SickLeave = newLeaveDetails.SickLeave-leaveDetails.SickLeave;
            newLeaveDetails.CasualLeave = newLeaveDetails.CasualLeave-leaveDetails.CasualLeave;
            newLeaveDetails.EarnedLeave = newLeaveDetails.EarnedLeave - leaveDetails.EarnedLeave;

            // Perform validation on the updated leave details
            if (newLeaveDetails.SickLeave < 0 || newLeaveDetails.CasualLeave < 0 || newLeaveDetails.EarnedLeave < 0)
            {
                return BadRequest("Negative leave balance is not allowed.");
            }

            // Save the updated leave details to the database using a service or repository
            _leaveDetailsService.UpdateLeaveDetails(newLeaveDetails);

            return Ok(newLeaveDetails);
        }
    }
}
