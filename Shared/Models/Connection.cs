using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Shared;

public class SocketConnection : IConnection
{
  private Socket socket;

  private byte[] _buffer = new byte[1024];

  public SocketConnection(Socket socket)
  {
    this.socket = socket;
  }

  public static SocketConnection Connect(byte[] ip, int port)
  {
    IPAddress iPAddress = new IPAddress(ip);
    IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, port);

    Socket socket = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

    socket.Connect(iPEndPoint);

    return new SocketConnection(socket);
  }

  public List<Message> Receive()
  {
    List<Message> messages = new List<Message>();
   
    if (this.socket.Available != 0)
    {
      byte[] buffer = new byte[1024];
      int read = this.socket.Receive(buffer);
      string content = System.Text.Encoding.UTF8.GetString(buffer, 0, read);

      string[] split = content.Split("|");
      for (int i = 0; i < split.Length - 1; i++)
      {
        string packet = split[i];
        string stringId = packet.Substring(0, 2);
        string message = packet.Substring(2);

        if (stringId == "10")
        {
          messages.Add(RegisterUserMessage.Decode(message));
        }
        if (stringId == "11")
        {
          messages.Add(LoginMessage.Decode(message));
        }
        else if (stringId == "12")
        {
          messages.Add(SendMessage.Decode(message));
        }
      }
    }

    return messages;
  }
  public string ReceiveMessage()
  {
    int receivedBytes = socket.Receive(_buffer);
    return Encoding.ASCII.GetString(_buffer, 0, receivedBytes);
  }

  public void Send(Message message)
  {
    string toSend = message.Id() + message.Encode() + "|";
    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(toSend);
    socket.Send(buffer);
  }
}