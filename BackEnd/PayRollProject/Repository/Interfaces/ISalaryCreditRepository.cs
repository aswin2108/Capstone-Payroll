using PayRollProject.Models;

namespace PayRollProject.Repository.Interfaces
{
    public interface ISalaryCreditRepository
    {
        public void InsertCreditTaxDetails(string userName, DateTime creditDate, decimal creditAmount, decimal taxCut, string TransactionId);
        public List<SalaryCredit> GetCreditTaxDetailsByUserName(string userName);
    }
}
