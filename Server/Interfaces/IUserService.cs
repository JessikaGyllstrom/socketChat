using Server.Models;
using Shared;

namespace Server.Interfaces;
public interface IUserService
{
  User? RegisterUser(string username, string password);
  bool CheckIfUsernameExist(string username);
  void PrintNumberOfLoggedInUsers();
  void BroadcastMessage(string username, string message);
  User? Login(IConnection connection, string username, string password);
}
