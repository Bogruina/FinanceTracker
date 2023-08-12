

using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.DAL.EfCore.Entities;

public class User
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    public string LastName { get; set; }

    [Required]
    public string Email { get; set; }

    public DateTime RegistratedDate { get; set; }

    public string PasswordHash { get; set; }

    public IEnumerable<Account> Accounts { get; set; }
}
