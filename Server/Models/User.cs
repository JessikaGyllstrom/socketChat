using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Server.Models
{
  public interface IDGenerator
  {
    int? Generate();
  }

  public class CountingGenerator : IDGenerator
  {
    private int counter = 0;

    public int? Generate()
    {
      return counter++;
    }
  }

  public class NullIdGenerator : IDGenerator
  {
    public int? Generate()
    {
      return null;
    }
  }
  public class User
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }   // maps to `_id` in the document
    [BsonElement("id")]
    public string Username { get; set; }
    public string Password { get; set; }

    public User(
          string username,
          string password
      )
    {
      this.Username = username;
      this.Password = password;
    }
  }
}