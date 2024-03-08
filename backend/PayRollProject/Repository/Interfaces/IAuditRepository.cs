using PayRollProject.Models;

namespace PayRollProject.Repository.Interfaces
{
    public interface IAuditRepository
    {
        public List<Audit> GetAllAuditDetails();
        public void CreateAuditRecord(Audit auditDetail);
    }
}
