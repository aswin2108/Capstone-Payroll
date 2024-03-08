using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayRollProject.Repository;
using PayRollProject.Repository.Interface;
using PayRollProject.Services;

namespace PayRollProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly UserDetailsService _userDetailsService;
        private readonly JobService _jobService;
        private readonly SalaryCreditService _salaryCreditService;
        private readonly UserRepository _userRepository;
        private readonly SalaryCreditRepository _salaryCreditRepository;

        public JobController(UserDetailsService userDetailsService, JobService jobService, SalaryCreditService salaryCreditService, UserRepository userRepository, SalaryCreditRepository salaryCreditRepository)
        {
            _userDetailsService = userDetailsService;
            _jobService = jobService;
            _salaryCreditService = salaryCreditService;
            _userRepository = userRepository;
            _userRepository = userRepository;
            _salaryCreditRepository = salaryCreditRepository;
        }

        [HttpPost("ScheduleSalaryCrediting")]
        public IActionResult ScheduleSalaryCrediting()
        {
            // Clear any existing recurring jobs for salary crediting
            RecurringJob.RemoveIfExists(nameof(JobService.CreditSalaries));

            // Schedule a recurring job to run at 12:00 PM every 7th, 14th, 21st, and last day of the month
            RecurringJob.AddOrUpdate(nameof(_jobService.CreditSalaries), () => JobService.CreditSalaries(_userDetailsService, _salaryCreditService, _userRepository, _salaryCreditRepository), "0 12 7 * *");
            RecurringJob.AddOrUpdate(nameof(_jobService.CreditSalaries), () => JobService.CreditSalaries(_userDetailsService, _salaryCreditService,  _userRepository, _salaryCreditRepository), "0 12 14 * *");
            RecurringJob.AddOrUpdate(nameof(_jobService.CreditSalaries), () => JobService.CreditSalaries(_userDetailsService, _salaryCreditService,  _userRepository, _salaryCreditRepository), "0 12 21 * *");
            RecurringJob.AddOrUpdate(nameof(_jobService.CreditSalaries), () => JobService.CreditSalaries(_userDetailsService, _salaryCreditService, _userRepository, _salaryCreditRepository), "0 12 L * *");

            BackgroundJob.Schedule(() => JobService.CreditSalaries(_userDetailsService, _salaryCreditService, _userRepository, _salaryCreditRepository), TimeSpan.FromSeconds(1));

            return Ok("Salary crediting job scheduled successfully");
        }
    }
}
