using AlertSystem.Interface;
using AlertSystem.Persistence;

namespace AlertSystem.Services
{
    public class JobServices : IJobInterface
    {
        private readonly AlertContext alertDb;
        private readonly IEmailService emailService;

        public JobServices(AlertContext alertDb, IEmailService emailService)
        {
            this.alertDb = alertDb;
            this.emailService = emailService;
        }
        public Task ContinuationJob()
        {
            throw new NotImplementedException();
        }

        public Task DelayedJobs()
        {
            throw new NotImplementedException();
        }

        public Task RecurringLoginCheckJobs()
        {
            ProcessLoginDuration();

            return Task.CompletedTask;
        }

        private void ProcessLoginDuration()
        {
            var lastTransaction = alertDb.Transactions.OrderByDescending(t => t.TransactionDate)
                                                       .FirstOrDefault();
                                           
            if(lastTransaction != null && lastTransaction.TransactionDate.AddMinutes(5) <= DateTime.UtcNow)
            {
                emailService.Send();
            }
        }

        public Task RecurringTransactionLimitJobs()
        {
            ProcesTransactionLimit();

            return Task.CompletedTask;
        }

        private Task ProcesTransactionLimit()
        {
            //sum of all the transaction in the last hour if it lesser than a limit - 
            var isTransactionLow = alertDb.Transactions.
                Where(t => t.TransactionDate.AddHours(-1) <= DateTime.UtcNow).Sum(t => t.Amount) < 1000;

            if(isTransactionLow)
            {
                //send Mail
                emailService.Send();
            }

            return Task.CompletedTask;
        }
    }
}
