using PayRollProject.Models;
using PayRollProject.Repository;

namespace PayRollProject.Services.Interfaces
{
    public interface ISalaryCreditService
    {
        public void InsertCreditTaxDetails(string userName, DateTime creditDate, decimal creditAmount, decimal taxCut);
        public List<SalaryCredit> GetCreditTaxDetailsByUserName(string userName);
        public void InsertCreditAndTaxDetails(string userName, DateTime creditDate, decimal creditAmount, decimal taxCut, SalaryCreditRepository salaryCreditRepository);
    }
}
