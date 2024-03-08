using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayRollProject.Models;
using PayRollProject.Services;
using System.Security.AccessControl;

namespace PayRollProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        private readonly AuditServices _auditService;
            public AuditController(AuditServices auditServices)
        {
            _auditService = auditServices;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("audit")]
        public IActionResult GetAllAuditDetails()
        {
            List<Audit> auditDetails=_auditService.GetAllAuditDetails();
            return Ok(auditDetails);
        }

        [HttpPost("createAudit")]
        public IActionResult CreateAuditRecord([FromBody]Audit audit) { 
            _auditService.CreateAuditRecord(audit);
            return Ok();
        }
    }
}
