namespace AlertSystem.Model
{
    public class Transaction : BaseEntity
    {
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
    }
}
