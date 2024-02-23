using Shared;
using Server.Interfaces;
using Server.Models;

namespace Server.Services;
public class LoginHandler : IMessageHandler
{   
  public void Handle(IConnection connection, Message message, IUserService localUserService)
  {
    Shared.LoginMessage login = (Shared.LoginMessage)message;
    User? user = localUserService.Login(connection, login.Username, login.Password);
     
    if (user == null)
    {
      connection.Send(new Shared.SendMessage("LOGIN_FAILED", "SERVER"));
    }
    else
    {
      connection.Send(new Shared.SendMessage("LOGIN_SUCCESSFUL", "SERVER"));
    }
  }
}