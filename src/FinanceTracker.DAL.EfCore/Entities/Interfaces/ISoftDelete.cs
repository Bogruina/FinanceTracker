namespace FinanceTracker.DAL.EfCore.Entities.Interfaces;

public interface ISoftDelete : IHasDeletionTime
{
    public bool IsDeleted {  get; set; }
}
