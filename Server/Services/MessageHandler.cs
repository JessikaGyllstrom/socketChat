using Shared;
using Server.Interfaces;

namespace Server.Services;
public class MessageHandler : IMessageHandler
{   
  public void Handle(IConnection connection, Message message, IUserService localUserService)
  {
    Shared.SendMessage chatMessage = (Shared.SendMessage)message;
    localUserService.BroadcastMessage(chatMessage.Username, chatMessage.Message);
  }
}