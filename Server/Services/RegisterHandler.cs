using Server.Interfaces;
using Shared;

namespace Server.Services;
public class RegisterHandler : IMessageHandler
{
  public void Handle(IConnection connection, Message message, IUserService userService)
  {
    Shared.RegisterUserMessage register = (Shared.RegisterUserMessage)message;
    
    // check if username is already taken
    bool usernameExists = userService.CheckIfUsernameExist(register.Username);

    if (usernameExists)
    {
      Console.WriteLine("Username taken");
      connection.Send(new Shared.SendMessage("REGISTRATION_FAILED", "SERVER"));
    }
    else
    {
      userService.RegisterUser(register.Username, register.Password);
      connection.Send(new Shared.SendMessage("REGISTRATION_SUCCESSFUL", "SERVER"));
    }
  }
}