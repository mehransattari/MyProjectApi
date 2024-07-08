

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

    public async Task<User> GetByUserAndPass( string userName, string password, CancellationToken cancellationToken)
    {
        var passHash = SecurityHelper.GetSha256Hash(password);  
        
        var result = await Table.SingleOrDefaultAsync(x=>x.UserName == userName && x.PasswordHash == passHash, cancellationToken);   
    
        return result;
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

    public  Task UpdateSecurityStampAsync(User user, CancellationToken cancellationToken)
    {
        user.SecurityStamp = Guid.NewGuid().ToString();
        return  UpdateAsync(user, cancellationToken);
    }

    public Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken)
    {
        user.LastLoginDate = DateTimeOffset.Now;
        return UpdateAsync(user, cancellationToken);
    }
}
