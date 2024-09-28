namespace FinanceTracker.DAL.EfCore.Entities.Interfaces;

public interface IHasDeletionTime
{
    public DateTime DeletionTime { get; set; }
}
