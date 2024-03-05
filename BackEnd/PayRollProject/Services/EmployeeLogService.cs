using PayRollProject.Models;
using PayRollProject.Repository.Interfaces;
using PayRollProject.Services.Interfaces;

namespace PayRollProject.Services
{
    public class EmployeeLogService:IEmployeeLogService
    {
        private readonly IEmployeeLogRepository _employeeLogRepository;
        public EmployeeLogService(IEmployeeLogRepository employeeLogRepository)
        {
            _employeeLogRepository = employeeLogRepository;
        }

        public EmployeeLog GetAttendanceByDateAndUserName(string date, string userName)
        {
            return _employeeLogRepository.GetAttendanceByDateAndUserName(date, userName);
        }
        public void UpdateAttendance(string date, string userName, string checkInTime, string checkOutTime)
        {
             _employeeLogRepository.UpdateAttendance(date, userName, checkInTime, checkOutTime);
            return;
        }

        public void InsertAttendance(string userName, string checkInTime, string date)
        {
            _employeeLogRepository.InsertAttendance(userName, checkInTime, date);
            return;
        }
    }
}
