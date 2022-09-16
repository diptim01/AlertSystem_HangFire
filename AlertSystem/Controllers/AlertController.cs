using AlertSystem.Interface;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlertSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertController : ControllerBase
    {
        private readonly ILogger<AlertController> _logger;
        private readonly IJobInterface jobService;
        private readonly IBackgroundJobClient backgroundJobClient;
        private readonly IRecurringJobManager recurringJobManager;

        public AlertController(ILogger<AlertController> logger, IJobInterface jobService, IBackgroundJobClient backgroundJobClient,
            IRecurringJobManager recurringJobManager)
        {
            _logger = logger;
            this.jobService = jobService;
            this.backgroundJobClient = backgroundJobClient;
            this.recurringJobManager = recurringJobManager;
        }

        [HttpGet("/TransactionLimitAlert")]
        public IActionResult TransactionLimitAlert()
        {
            recurringJobManager.AddOrUpdate(nameof(TransactionLimitAlert), () => jobService.RecurringTransactionLimitJobs(), Cron.Minutely);
            return Ok();
        }


        [HttpGet("/LastLoginTime")]
        public IActionResult LastLoginTime()
        {
            recurringJobManager.AddOrUpdate(nameof(LastLoginTime), () => jobService.RecurringLoginCheckJobs(), Cron.Minutely);
            return Ok();
        }
    }
}
