using Microsoft.AspNetCore.Mvc;
using PayRollProject.Models;

namespace PayRollProject.Services.Interfaces
{
    public interface ILeaveDetailsService
    {
        public LeaveDetails GetLeaveDetails(string UserName);
        public void CreateLeaveDetails(LeaveDetails leaveDetails);
        public void UpdateLeaveDetails(LeaveDetails leaveDetails);
    }
}
