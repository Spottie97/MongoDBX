using System;

class Program
{
    static async Task Main(string[] args)
    {
        var connString = "mongodb://localhost:27017";
        var dbName = "myTestdb";
        var collectionName = "users";
        var repo = new UserRepo(connString, dbName, collectionName);

        // Insert a new user
        var newUser = new User
        {
            Id = 1,
            FirstName = "Koos",
            LastName = "Doe",
            Address = new Address
            {
                Street = "123 Main St",
                Suburb = "Anytown",
                City = "Springfield",
                PostalCode = "12345"
            }
        };
        await repo.InsertUser(newUser);
        
        // Create an index on FirstName
        await repo.CreateFirstNameIndex();
        Console.WriteLine("Index created on FirstName.");

        // Get all users
        var users = await repo.GetAllUsers();
        Console.WriteLine("All Users:");
        foreach (var user in users)
        {
            Console.WriteLine($"{user.Id} {user.FirstName} {user.LastName}");
        }

        // Get a user by id
        var userById = await repo.GetUserById(1);
        Console.WriteLine($"User with Id=1: {userById.FirstName} {userById.LastName}");

        // Update a user
        var updatedUser = new User
        {
            Id = 1,
            FirstName = "Sam",
            LastName = "Doe",
            Address = new Address
            {
                Street = "456 Oak St",
                Suburb = "Anytown",
                City = "Springfield",
                PostalCode = "12345"
            }
        };
        var isUpdated = await repo.UpdateUser(1, updatedUser);
        if (isUpdated)
        {
            Console.WriteLine("User updated successfully.");
        }
        Console.WriteLine($"User with Id=1: {updatedUser.FirstName} {updatedUser.LastName}");

        // Delete a user
        var isDeleted = await repo.DeleteUser(1);
        if (isDeleted)
        {
            Console.WriteLine("User deleted successfully.");
        }

        Console.ReadLine();
    }
}
