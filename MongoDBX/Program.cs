using System;
using MongoDB.Driver;
using MongoDBX;

class Program
{
    static void Main(string[] args)
    {
        var connString = "";
        var dbName = "";

        var repository = new UserRepo(connString, dbName);

        var user1 = new User
        {
            FirstName = "Jason",
            LastName = "Cloete",
            Address = new Address
            {
                Street = "123 Dev St",
                Suburb = "Downtown",
                City = "Cape Town",
                PostalCode = "1010"
            }
        };

        repository.Create(user1);
        
        //Get All users
        var users = repository.GetAll();
        foreach (var user in users)
        {
            Console.WriteLine($"Name: {user.FirstName} {user.LastName}. Address: {user.Address.Street}, {user.Address.Suburb}, {user.Address.City}, {user.Address.PostalCode}");
        }
        
        //Update a user
        var user2 = repository.GetById(user1.Id);
        user2.FirstName = "NotJason";
        repository.Update(user2.Id, user2);
        
        //Delete a user
        var user3 = repository.GetById(user1.Id);
        repository.Remove(user3.Id);
        
        //Add a Index on FirstName
        var indexModel = new CreateIndexModel<User>(
            Builders<User>.IndexKeys.Ascending(u => u.FirstName),
            new CreateIndexOptions { Unique = true });
        repository._users.Indexes.CreateOne(indexModel);
    }
}