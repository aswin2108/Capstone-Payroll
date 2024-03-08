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
        [Authorize(Roles = "HR, Employee")]
        //[Authorize(Policy = "AdminHrEmployee")]
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

        [HttpGet("unapprovedLeave/{empType}")]
        [Authorize(Roles = "Admin, HR")]
        public IActionResult GetUnapprovedLeaveRequests(string empType)
        {
            List<LeaveRecord>unapprovedList=_leaveRecordsService.GetUnapprovedLeaveRequests(empType);
            return Ok(unapprovedList);
        }

        [HttpPut("approveLeave/{UserName}/{FromDate}/{Flag}")]
        [Authorize(Roles = "Admin, HR")]
        public IActionResult ApproveLeaveRequest(string UserName, string FromDate, int Flag)
        {
            _leaveRecordsService.ApproveLeaveRequest(UserName, FromDate, Flag);
            return Ok();
        }

        [HttpDelete("rejectLeave")]
        [Authorize(Roles = "Admin, HR")]
        public IActionResult DeleteLeaveRequest(string UserName)
        {
            _leaveRecordsService.DeleteLeaveRequest(UserName);
            return Ok();
        }

        [HttpGet("approvedLeaves/{UserName}")]
        [Authorize(Roles = "Admin, HR, Employee")]
        public IActionResult GetUserApprovedLeaveRequests(string UserName)
        {
            List<LeaveRecord>approvedList =_leaveRecordsService.GetUserApprovedLeaveRequests(UserName);
            return Ok(approvedList);
        }


    }
}
