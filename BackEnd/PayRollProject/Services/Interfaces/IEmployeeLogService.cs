using PayRollProject.Models;

namespace PayRollProject.Services.Interfaces
{
    public interface IEmployeeLogService
    {
        public EmployeeLog GetAttendanceByDateAndUserName(string date, string userName);
        public void UpdateAttendance(string date, string userName, string checkInTime, string checkOutTime);
        public void InsertAttendance(string userName, string checkInTime, string date);
    }
}
