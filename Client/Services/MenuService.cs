using Client.Interfaces;
using Shared;

namespace Client.Services;
public class MenuService : IMenuService
{
  private readonly IConnection _databaseUserService;
  private string? _username;

  public MenuService(IConnection databaseUserService)
  {
    _databaseUserService = databaseUserService;
  }
  public void ShowMainMenu()
  {
    Console.WriteLine("Welcome to ChatApp!");
    Console.WriteLine("============ MENU ============");
    Console.WriteLine("1. Register");
    Console.WriteLine("2. Login");
    Console.WriteLine("4. Exit");
  }
  public void ShowLoggedInMenu()
  {
    Console.WriteLine("============ MENU ============");
    Console.WriteLine("3. Send a message");
    Console.WriteLine("13. Exit ChatApp");
  }

  public void HandleUserInput(IConnection connection)
  {
    Console.Write("Please enter a choice: ");
    string userInput = Console.ReadLine() ?? "";

    switch (userInput)
    {
      case "1":
        Register(connection);
        break;
      case "2":
        Login(connection);
        break;
      case "4":
        Console.WriteLine("Exiting...");
        return;
      default:
        Console.WriteLine("Invalid choice. Please try again.");
        break;
    }
  }

  public void HandleLoggedInUserInput(IConnection connection)
  {
    Console.Write("Please enter a choice: ");
    string userInput = Console.ReadLine() ?? "";

    switch (userInput)
    {
      case "3":
        SendMessage(connection);
        break;
      case "4":
        Console.WriteLine("Exiting...");
        return;
      default:
        Console.WriteLine("Invalid choice. Please try again.");
        break;
    }
  }

  private void Register(IConnection connection)
  {
    Console.WriteLine("*** REGISTER ***");
    Console.WriteLine("Please enter a username");
    string username = Console.ReadLine() ?? "";
    Console.WriteLine("Please enter a password");
    string password = Console.ReadLine() ?? "";
    connection.Send(new Shared.RegisterUserMessage(username, password));
    Console.WriteLine($"Sending registration request for user \"{username}\" + {password}");
  }

  private void Login(IConnection connection)
  {
    Console.WriteLine("*** LOGIN ***");
    Console.WriteLine("Please enter a username");
    string username = Console.ReadLine() ?? "";
    Console.WriteLine("Please enter a password");
    string password = Console.ReadLine() ?? "";
    connection.Send(new Shared.LoginMessage(username, password));
    Console.WriteLine($"Sending login request for user \"{username}\" + {password}");
    this._username = username;

  }
  private void SendMessage(IConnection connection)
  {
    Console.WriteLine("*** SEND MESSAGE ***");
    Console.WriteLine("Please enter a message");
    string message = Console.ReadLine() ?? "";
    connection.Send(new Shared.SendMessage(_username, message));
    Console.WriteLine($"Sending message \"{message}\"");
    return;
  }
}