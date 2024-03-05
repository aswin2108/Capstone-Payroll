using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayRollProject.Models;
using PayRollProject.Services;

namespace PayRollProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRecordsController : ControllerBase
    {
        private readonly LeaveRecordsService _leaveRecordsService;
        public LeaveRecordsController(LeaveRecordsService leaveRecordsService)
        {
            _leaveRecordsService=leaveRecordsService;
        }

        [HttpPost("leaveSubmission")]
        [Authorize(Policy = "AdminHrEmployee")]
        public IActionResult SubmitLeaveRequest([FromBody] LeaveRecord leaveRecord)
        {
            try
            {
            _leaveRecordsService.SubmitLeaveRequest(leaveRecord);
                return Ok(leaveRecord);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500);
            }

        }

        [HttpGet("unapprovedLeave")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult GetUnapprovedLeaveRequests()
        {
            List<LeaveRecord>unapprovedList=_leaveRecordsService.GetUnapprovedLeaveRequests();
            return Ok(unapprovedList);
        }

        [HttpPut("approveLeave")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult ApproveLeaveRequest(string UserName)
        {
            _leaveRecordsService.ApproveLeaveRequest(UserName);
            return Ok();
        }

        [HttpDelete("rejectLeave")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult DeleteLeaveRequest(string UserName)
        {
            _leaveRecordsService.DeleteLeaveRequest(UserName);
            return Ok();
        }

        [HttpGet("approvedLeaves")]
        [Authorize(Policy = "AdminHrEmployee")]
        public IActionResult GetUserApprovedLeaveRequests(string UserName)
        {
            List<LeaveRecord>approvedList =_leaveRecordsService.GetUserApprovedLeaveRequests(UserName);
            return Ok(approvedList);
        }


    }
}
