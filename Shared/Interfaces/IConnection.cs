using System.Net.Sockets;

namespace Shared;

public interface IConnection
{
    void Send(Message message);
    List<Message> Receive();
    
    string ReceiveMessage();
}
