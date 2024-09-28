namespace FinanceTracker.DAL.EfCore.Entities.Interfaces;

public interface IHasModificationTime
{
    public DateTime ModificationTime { get; set; }
}
