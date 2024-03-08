using PayRollProject.Models;

namespace PayRollProject.Services.Interfaces
{
    public interface IAuditService
    {
        public List<Audit> GetAllAuditDetails();
        public void CreateAuditRecord(Audit auditDetails);
    }
}
