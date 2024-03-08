using PayRollProject.Models;

namespace PayRollProject.Repository.Interfaces
{
    public interface ISalaryCreditRepository
    {
        public void InsertCreditTaxDetails(string userName, DateTime creditDate, decimal creditAmount, decimal taxCut, string TransactionId, int Excemption, int Bonus, int OverTime);
        public List<SalaryCredit> GetCreditTaxDetailsByUserName(string userName);
    }
}
