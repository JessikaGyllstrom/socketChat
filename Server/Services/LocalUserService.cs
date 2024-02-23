
using System.Net.Sockets;
using System.Text;
using Server.Interfaces;
using Server.Models;
using Shared;

namespace Server.Services;

// Local user service connected to the database user service
public class LocalUserService(IUserRepository databaseUserService) : IUserService
{
  private IUserRepository databaseUserService = databaseUserService;
  private User? loggedIn = null;
  // local list of users
  private List<User> users = new List<User>();
  private List<IConnection> connections = new List<IConnection>();
  private Dictionary<string, IConnection>? connectedUsers;

  public User? RegisterUser(string username, string password)
  {
    User user = new User(username, password);
    databaseUserService.Save(user);

    return user;
  }
  public bool CheckIfUsernameExist(string username)
  {
    bool found = this.databaseUserService.CheckIfUsernameExist(username);

    if (found)
    {
      return true;
    }
    
    return false;
  }
  public User? Login(IConnection connection, string username, string password)
  {
    // check for user match in database with the provided username and password
    User? user = this.databaseUserService.GetUserByUsernameAndPassword(username, password);

    if (user == null)
    {
      return null;
    }

    this.loggedIn = user;
    users.Add(user);
    this.connections.Add(connection);

    return user;
  }
  public void PrintNumberOfLoggedInUsers()
  {
    string numberOfUsers = users.Count().ToString();
    Console.WriteLine("printing logged in users" + numberOfUsers);    
  }
  public void BroadcastMessage(string username, string message)
  {
      // TODO remove disconnected clients from the list
      foreach (IConnection client in connections)
      {
        Console.WriteLine($"User: {username} wrote: \"{message}\"");
        client.Send(new Shared.SendMessage(username, message));
      }
  }
}
// TODO add logout