namespace FinanceTracker.DAL.EfCore.Entities;

public class Account
{
    public Guid Id { get; set; }

    public string AccountNumber { get; set; }

    public decimal Balance { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    public IEnumerable<Transaction> Transactions { get; set; }
}
