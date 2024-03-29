﻿using PayRollProject.Models;
using PayRollProject.Repository;
using PayRollProject.Repository.Interfaces;

namespace PayRollProject.Services
{
    public class SalaryCreditService:ISalaryCreditRepository
    {
        private readonly ISalaryCreditRepository _salaryCreditRepository;
        public SalaryCreditService(ISalaryCreditRepository salaryCreditRepository)
        {
            _salaryCreditRepository = salaryCreditRepository;
        }
        public void InsertCreditTaxDetails(string userName, DateTime creditDate, decimal creditAmount, decimal taxCut, string TransactionId, int ExcemptionAmt, int Bonus, int OverTime)
        {
            _salaryCreditRepository.InsertCreditTaxDetails(userName, creditDate, creditAmount, taxCut, TransactionId, ExcemptionAmt, Bonus, OverTime); 
        }
        public List<SalaryCredit> GetCreditTaxDetailsByUserName(string userName)
        {
            return _salaryCreditRepository.GetCreditTaxDetailsByUserName(userName);
        }
        public void InsertCreditAndTaxDetails(string userName, DateTime creditDate, decimal creditAmount, decimal taxCut, SalaryCreditRepository salaryCreditRepository, string TransactionId, int ExcemptionAmt, int Bonus, int OverTime)
        {
            salaryCreditRepository.InsertCreditTaxDetails(userName, creditDate, creditAmount, taxCut, TransactionId, ExcemptionAmt, Bonus, OverTime);
        }
    }
}
