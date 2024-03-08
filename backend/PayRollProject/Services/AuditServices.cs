using PayRollProject.Models;
using PayRollProject.Repository.Interfaces;
using PayRollProject.Services.Interfaces;

namespace PayRollProject.Services
{
    public class AuditServices:IAuditService
    {
        private readonly IAuditRepository _auditRepository;

        public AuditServices(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }

        public void CreateAuditRecord(Audit auditDetails)
        {
             _auditRepository.CreateAuditRecord(auditDetails);
            return;
        }
        public List<Audit> GetAllAuditDetails()
        {
            return _auditRepository.GetAllAuditDetails();
        }
    }
}
