using System;

class Program
{
    static async Task Main(string[] args)
    {
        var connectionString = "mongodb://localhost:27017";
        var databaseName = "mydatabase";
        var collectionName = "users";
        var repo = new UserRepo(connectionString, databaseName, collectionName);

        // Insert a new user
        var newUser = new User
        {
            Id = 1,
            FirstName = "John",
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
            FirstName = "Jane",
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

        // Delete a user
        var isDeleted = await repo.DeleteUser(1);
        if (isDeleted)
        {
            Console.WriteLine("User deleted successfully.");
        }

        // Create an index on FirstName
        await repo.CreateFirstNameIndex();
        Console.WriteLine("Index created on FirstName.");

        Console.ReadLine();
    }
}
