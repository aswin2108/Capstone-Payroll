using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayRollProject.Models;
using PayRollProject.Services;

namespace PayRollProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryCreditControler : ControllerBase
    {
        private readonly SalaryCreditService _salaryCreditService;
        public SalaryCreditControler(SalaryCreditService salaryCreditService)
        {
            _salaryCreditService = salaryCreditService;
        }

        [HttpPost("salaryCredit/update")]
        public IActionResult InsertCreditTaxDetails(string userName, DateTime creditDate, decimal creditAmount, decimal taxCut, string transactionId, int ExcemptionAmt, int Bonus, int OverTime) 
        {
            _salaryCreditService.InsertCreditTaxDetails(userName, creditDate, creditAmount, taxCut, transactionId, ExcemptionAmt, Bonus, OverTime);
            return Ok();
        }

        [Authorize(Roles = "Admin, HR, Employee")]
        [HttpGet("creditDetails/{UserName}")]
        public IActionResult GetCreditTaxDetailsByUserName(string UserName)
        {
            List<SalaryCredit> users = _salaryCreditService.GetCreditTaxDetailsByUserName(UserName);
            return Ok(users);
        }
    }
}
