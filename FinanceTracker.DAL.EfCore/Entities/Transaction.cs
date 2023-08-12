namespace FinanceTracker.DAL.EfCore.Entities;

public class Transaction
{
    public Guid Id { get; set; }

    public TransactionType Type { get; set; }

    public DateTime Timestamp { get; set; }

    public decimal Amount { get; set; }

    public Guid AccountId { get; set; }

    public Account Account { get; set; }

}

public enum TransactionType
{

}
