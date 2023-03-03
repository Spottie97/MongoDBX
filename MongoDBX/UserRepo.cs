namespace MongoDBX;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

public class UserRepo
{
    public readonly IMongoCollection<User> _users;

    public UserRepo(string connString, string dbName)
    {
        var client = new MongoClient(connString);
        var database = client.GetDatabase(dbName);
        _users = database.GetCollection<User>("user");
    }

    public List<User> GetAll()
    {
        return _users.Find(user => true).ToList();
    }

    public User GetById(string id)
    {
        return _users.Find<User>(user => user.Id == id).FirstOrDefault();
    }

    public User Create(User user)
    {
        _users.InsertOne(user);
        return user;
    }

    public void Update(string id, User userIn)
    {
        _users.ReplaceOne(user => user.Id == id, userIn);
    }

    public void Remove(User userIn)
    {
        _users.DeleteOne(user => user.Id == userIn.Id);
    }

    public void Remove(string id)
    {
        _users.DeleteOne(user => user.Id == id);
    }
    
}