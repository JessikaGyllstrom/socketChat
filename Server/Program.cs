using System.Net.Sockets;
using Server.Interfaces;
using Shared;
using Server.Services;

namespace Server;

public class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("starting server");

    ChatApp app = new ChatApp(
     new LocalUserService(new MongoDbRepository()),
     new SocketConnectionHandler());

    app.Start();
  }
}
public class ChatApp
{
  private IUserService _userService;

  private IConnectionHandler _connectionHandler;

  private List<IConnection> connections;

  private Dictionary<int, IMessageHandler> handlers;

  // constructor
  public ChatApp(
      IUserService userService,
      IConnectionHandler connectionHandler
  )
  {
    this.connections = new List<IConnection>();
    this._userService = userService;
    this._connectionHandler = connectionHandler;
    this.handlers = new Dictionary<int, IMessageHandler>();
    this.handlers[10] = new RegisterHandler();
    this.handlers[11] = new LoginHandler();
    this.handlers[12] = new MessageHandler();
  }

  public void Start()
  {
    while (true)
    {
      Shared.IConnection? potentialClient = this._connectionHandler.Accept();

      if (potentialClient != null)
      {
        Console.WriteLine("A client has connected!");
      }
      this._connectionHandler.HandleReads(_userService);
    }
  }
}
