namespace FinanceTracker.DAL.EfCore.Entities.Interfaces;

public interface IHasCreationTime
{
    public DateTime CreatedTime {  get; set; }
}
