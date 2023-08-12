using FinanceTracker.DAL.EfCore.Context;
using FinanceTracker.DAL.EfCore.Entities;

namespace FinanceTracker.DAL.EfCore.Repositories;

public class UserRepository : RepositoryBase<User>
{
    private readonly ApplicationContext _context;
    public UserRepository(ApplicationContext context): base(context)
    {
        _context = context;
    }
}
