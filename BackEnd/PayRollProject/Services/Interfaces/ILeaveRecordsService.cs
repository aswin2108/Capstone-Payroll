using PayRollProject.Models;

namespace PayRollProject.Services.Interfaces
{
    public interface ILeaveRecordsService
    {
        public void SubmitLeaveRequest(LeaveRecord leaveRecord);
        public List<LeaveRecord> GetUnapprovedLeaveRequests(string empType);
        public void ApproveLeaveRequest(string UserName, string FromData, int Flag);
        public void DeleteLeaveRequest(string UserName);
        public List<LeaveRecord> GetUserApprovedLeaveRequests(string UserName);
    }
}
