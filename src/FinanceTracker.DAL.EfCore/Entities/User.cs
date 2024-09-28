using FinanceTracker.DAL.EfCore.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.DAL.EfCore.Entities;

public class User : IPersistentEntity, IHasCreationTime, IHasModificationTime, ISoftDelete
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    public string LastName { get; set; }

    [Required]
    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public IEnumerable<Account> Accounts { get; set; }

    public DateTime CreatedTime { get; set; }
    public DateTime ModificationTime { get; set ; }
    public bool IsDeleted { get; set; }
    public DateTime DeletionTime { get; set; }
}
