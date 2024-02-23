using MongoDB.Driver;
using Server.Interfaces;
using Server.Models;
public class MongoDbRepository : IUserRepository
{
  MongoClient dbClient;
  IMongoDatabase db;
  IMongoCollection<User> collection;

  public MongoDbRepository()
  {
    this.dbClient = new MongoClient("mongodb://localhost:27017");

    // connect to database (in docker) and collection "users"
    this.db = dbClient.GetDatabase("mongodb");
    this.collection = db.GetCollection<User>("users");

    // connection successful
    Console.WriteLine("Connection to MongoDB successful.");
  }

  public List<User> GetAll()
  {
    var filter = Builders<User>.Filter.Empty;
    var users = collection.Find(filter).ToList();
    return users;
  }
  public bool CheckIfUsernameExist(string username)
  {
      var filter = Builders<User>.Filter.Eq(u => u.Username, username);
      var user = this.collection.Find(filter).FirstOrDefault();

      return user != null;
  }
  // check if username matches users password
  public User? GetUserByUsernameAndPassword(string username, string password)
  {
    // Find a user with the provided username and password
    User user = collection.Find(u => u.Username == username && u.Password == password).FirstOrDefault();

    if (user == null)
    {
      return null;
    }
    // If user is found, authentication is successful
    return user;
  }

  // save user to database
  public bool Save(User user)
  {
    try
    {
      this.collection.InsertOne(user);
      Console.WriteLine("User added successfully. User ID: " + user.Id + " " + "UserName: " + user.Username);
      // successfully inserted
      return true;
    }
    // failed to insert
    catch (Exception exception)
    {
      Console.WriteLine("Error registering user: " + exception.Message);
      return false;
    }
  }
}