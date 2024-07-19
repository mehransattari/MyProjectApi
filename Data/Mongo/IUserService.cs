using Entittes.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mongo;

public interface IUserService
{
    Task<UserMongo> GetUserByIdAsync(Guid id);
    Task<IEnumerable<UserMongo>> GetAllUsersAsync();
    Task CreateUserAsync(UserMongo user);
    Task UpdateUserAsync(UserMongo user);
    Task DeleteUserAsync(Guid id);
}
