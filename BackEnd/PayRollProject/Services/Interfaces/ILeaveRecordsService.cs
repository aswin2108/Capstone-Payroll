using PayRollProject.Models;

namespace PayRollProject.Services.Interfaces
{
    public interface ILeaveRecordsService
    {
        public void SubmitLeaveRequest(LeaveRecord leaveRecord);
        public List<LeaveRecord> GetUnapprovedLeaveRequests();
        public void ApproveLeaveRequest(string UserName);
        public void DeleteLeaveRequest(string UserName);
        public List<LeaveRecord> GetUserApprovedLeaveRequests(string UserName);
    }
}
