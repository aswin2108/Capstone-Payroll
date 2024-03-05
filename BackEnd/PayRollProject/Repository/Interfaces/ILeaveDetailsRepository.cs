using PayRollProject.Models;

namespace PayRollProject.Repository.Interfaces
{
    public interface ILeaveDetailsRepository
    {
        public LeaveDetails GetLeaveDetails(string UserName);
        public void CreateLeaveDetails(LeaveDetails leaveDetails);
        void UpdateLeaveDetails(LeaveDetails leaveDetails);
    }
}
