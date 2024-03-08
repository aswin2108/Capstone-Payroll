using PayRollProject.Models;

namespace PayRollProject.Repository.Interfaces
{
    public interface ILeaveRecordsRepository
    {
        public void SubmitLeaveRequest(LeaveRecord leaveRecord);
        public List<LeaveRecord> GetUnapprovedLeaveRequests(string empType);
        public void ApproveLeaveRequest(string UserName, string FromDate, int Flag);
        public void DeleteLeaveRequest(string UserName);
        public List<LeaveRecord> GetUserApprovedLeaveRequests(string UserName);
    }
}
