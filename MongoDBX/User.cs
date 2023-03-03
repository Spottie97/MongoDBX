using System.Security.Cryptography.X509Certificates;

namespace MongoDBX;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Address Address { get; set; }
    
}

public class Address
{
    public string Street { get; set; }
    public  string Suburb { get; set; }
    public string City { get; set; }
    public  string PostalCode { get; set; }
    
}