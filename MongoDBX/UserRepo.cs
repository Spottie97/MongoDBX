using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

public class UserRepo
{
    private readonly IMongoCollection<User> _users;

    public UserRepo(string connString, string dbName, string collectionName)
    {
        var client = new MongoClient(connString);
        var database = client.GetDatabase(dbName);
        _users = database.GetCollection<User>(collectionName);
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _users.Find(user => true).ToListAsync();
    }

    public async Task<User> GetUserById(int id)
    {
        return await _users.Find(user => user.Id == id).FirstOrDefaultAsync();
    }

    public async Task InsertUser(User user)
    {
        // Generate a unique Id for the new user
        var highestId = await _users.CountDocumentsAsync(Builders<User>.Filter.Empty);
        user.Id = highestId != 0 ? (int)(highestId + 1) : 1;

        await _users.InsertOneAsync(user);
    }
    
    public async Task<bool> UpdateUser(int id, User user)
    {
        var updateResult = await _users.ReplaceOneAsync(filter: user => user.Id == id, replacement: user);
        return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }

    public async Task<bool> DeleteUser(int id)
    {
        var deleteResult = await _users.DeleteOneAsync(user => user.Id == id);
        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }

    public async Task CreateFirstNameIndex()
    {
        var indexKeysDefinition = Builders<User>.IndexKeys.Ascending(user => user.FirstName);
        var indexOptions = new CreateIndexOptions { Unique = true };
        await _users.Indexes.CreateOneAsync(new CreateIndexModel<User>(indexKeysDefinition, indexOptions));
    }
}
