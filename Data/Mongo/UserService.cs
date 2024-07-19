using Common.MongoSettings;
using Data.Mongo;
using Entittes.Mongo;
using MongoDB.Driver;

namespace Data.Mongo;

public class UserService : IUserService
{
    private readonly IMongoCollection<UserMongo> _users;

    public UserService(IMongoClient mongoClient, MongoSetting mongoSetting)
    {
        var database = mongoClient.GetDatabase(mongoSetting.DatabaseName);
        _users = database.GetCollection<UserMongo>("Users");
    }

    public async Task<UserMongo> GetUserByIdAsync(Guid id)
    {
        return await _users.Find(user => user.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<UserMongo>> GetAllUsersAsync()
    {
        return await _users.Find(user => true).ToListAsync();
    }

    public async Task CreateUserAsync(UserMongo user)
    {
        await _users.InsertOneAsync(user);
    }

    public async Task UpdateUserAsync(UserMongo user)
    {
        await _users.ReplaceOneAsync(u => u.Id == user.Id, user);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        await _users.DeleteOneAsync(user => user.Id == id);
    }
}