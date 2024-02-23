using Server.Models;

namespace Server.Interfaces;
public interface IUserRepository
{
  bool Save(User user);
  bool CheckIfUsernameExist(string username);

  User? GetUserByUsernameAndPassword(string username, string password);
  List<User> GetAll();
}