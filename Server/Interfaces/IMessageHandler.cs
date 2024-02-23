using Shared;

namespace Server.Interfaces;
public interface IMessageHandler
{
  void Handle(IConnection connection, Message message, IUserService userService);
}