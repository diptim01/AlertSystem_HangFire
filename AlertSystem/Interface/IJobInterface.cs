namespace AlertSystem.Interface
{
    public interface IJobInterface
    {
        Task RecurringTransactionLimitJobs();

        Task RecurringLoginCheckJobs();
        Task DelayedJobs();

        Task ContinuationJob();
    }
}
