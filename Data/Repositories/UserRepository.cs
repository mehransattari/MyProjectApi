

using Common.Exceptions;
using Common.Utilities;
using Data.Context;
using Data.Contracts;
using Entittes;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
            
    }

    public  Task<User?> GetByUserAndPass( string userName, string password, CancellationToken cancellationToken)
    {
        var passHash = SecurityHelper.GetSha256Hash(password);  
        
        return Table.SingleOrDefaultAsync(x=>x.UserName == userName && password == passHash, cancellationToken);   
    }

    public async Task AddAsync(User user, string password, CancellationToken cancellationToken)
    {
        var exists = await TableNoTracking.AnyAsync(p => p.UserName == user.UserName);
        if (exists)
            throw new BadRequestExceptions("نام کاربری تکراری است");

        var passHash = SecurityHelper.GetSha256Hash(password);
        user.PasswordHash = passHash;
        await base.AddAsync(user, cancellationToken);
    }

}
