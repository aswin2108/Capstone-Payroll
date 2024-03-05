using Microsoft.AspNetCore.Mvc;
using PayRollProject.Models;
using PayRollProject.Repository.Interface;
using PayRollProject.Repository.Interfaces;
using PayRollProject.Services.Interfaces;

namespace PayRollProject.Services
{
    public class LeaveDetailsService: ILeaveDetailsService
    {
        private readonly ILeaveDetailsRepository _leaveDetailsRepository;

        public LeaveDetailsService(ILeaveDetailsRepository leaveDetailsRepository)
        {
            _leaveDetailsRepository = leaveDetailsRepository;
        }

        public LeaveDetails GetLeaveDetails(string UserName)
        {
            return _leaveDetailsRepository.GetLeaveDetails(UserName);
        }

        public void CreateLeaveDetails(LeaveDetails leaveDetails)
        {
            _leaveDetailsRepository.CreateLeaveDetails(leaveDetails);
            return;
        }
        public void UpdateLeaveDetails(LeaveDetails leaveDetails)
        {
            _leaveDetailsRepository.UpdateLeaveDetails(leaveDetails);
        }
    }
}
