using AlertSystem.Interface;

namespace AlertSystem.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> logger;

        public EmailService(ILogger<EmailService> logger)
        {
            this.logger = logger;
        }
        public Task Send()
        {
            logger.LogInformation("Email sent successfully");

            return Task.CompletedTask;
        }
    }
}
