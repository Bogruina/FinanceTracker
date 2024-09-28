using FinanceTracker.DAL.EfCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.DAL.EfCore.Repositories;

public class UserRepository : BaseRepository
{
    private readonly DbSet<User> _users;
    public UserRepository(DbContext context)
        : base(context)
    {
        _users = DataContext.Set<User>();
    }

    public async Task<User> CreateUserAsync(User user)
    {
        PrepareForCreation(user);
        var createdEntity = (await _users.AddAsync(user)).Entity;
        await DataContext.SaveChangesAsync();

        return createdEntity;
    }
}
