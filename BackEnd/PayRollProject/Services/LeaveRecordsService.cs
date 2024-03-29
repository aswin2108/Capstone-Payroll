﻿using PayRollProject.Models;
using PayRollProject.Repository.Interfaces;
using PayRollProject.Services.Interfaces;

namespace PayRollProject.Services
{
    public class LeaveRecordsService: ILeaveRecordsService
    {
        private readonly ILeaveRecordsRepository _leaveRecordsRepository;
        public LeaveRecordsService(ILeaveRecordsRepository leaveRecordsRepository)
        {
            _leaveRecordsRepository = leaveRecordsRepository;
        }
        public void SubmitLeaveRequest(LeaveRecord leaveRecord)
        {
            _leaveRecordsRepository.SubmitLeaveRequest(leaveRecord);
            return;
        }
        public List<LeaveRecord> GetUnapprovedLeaveRequests(string empType)
        {
            return _leaveRecordsRepository.GetUnapprovedLeaveRequests(empType);
        }

        public void ApproveLeaveRequest(string UserName, string FromDate, int Flag)
        {
            _leaveRecordsRepository.ApproveLeaveRequest(UserName, FromDate, Flag);
            return;
        }
        public void DeleteLeaveRequest(string UserName)
        {
            _leaveRecordsRepository.DeleteLeaveRequest(UserName);
            return;
        }
        public List<LeaveRecord> GetUserApprovedLeaveRequests(string UserName)
        {
            return _leaveRecordsRepository.GetUserApprovedLeaveRequests(UserName);
        }
    }
}
