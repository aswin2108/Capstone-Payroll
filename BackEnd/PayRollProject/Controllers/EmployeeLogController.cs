using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayRollProject.Models;
using PayRollProject.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PayRollProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeLogController : ControllerBase
    {
        private readonly EmployeeLogService _employeeLogService;

        public EmployeeLogController(EmployeeLogService employeeLogService)
        {
            _employeeLogService = employeeLogService;
        }

        [HttpGet("{UserName}/{Date}")]
        [Authorize(Roles = "Admin, HR, Employee")]
        public IActionResult GetAttendanceByDateAndUserName(string Date, string UserName)
        {
            Console.WriteLine(Date + " " + UserName);
            EmployeeLog Attendance = _employeeLogService.GetAttendanceByDateAndUserName(Date, UserName);
            return Ok(Attendance);
        }

        [Authorize(Roles = "Admin, HR, Employee")]
        [HttpPut("updateCheckTime")]
        public IActionResult UpdateAttendance([FromBody] EmployeeLog Attendance)
        {
            _employeeLogService.UpdateAttendance(Attendance.Date, Attendance.UserName, Attendance.CheckInTime, Attendance.CheckOutTime);
            return Ok();
        }

        [Authorize(Roles = "Admin, HR, Employee")]
        [HttpPost("{UserName}/{date}/{CheckInTime}")]
        public IActionResult InsertAttendance(string UserName, string CheckInTime, string date)
        {
            _employeeLogService.InsertAttendance(UserName, CheckInTime, date);
            return Ok();
        }
    }
}
