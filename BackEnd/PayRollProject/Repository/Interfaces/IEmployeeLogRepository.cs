using PayRollProject.Models;

namespace PayRollProject.Repository.Interfaces
{
    public interface IEmployeeLogRepository
    {
        public EmployeeLog GetAttendanceByDateAndUserName(string date, string userName);
        public void UpdateAttendance(string date, string userName, string checkInTime, string checkOutTime);
        public void InsertAttendance(string userName, string checkInTime, string date);
    }
}
