using Client.Interfaces;
using Client.Services;
public class Program
{
  static void Main(string[] args)
  {

    Shared.IConnection connection = Shared.SocketConnection.Connect(
        new byte[] { 127, 0, 0, 1 },
        27800
    );

    IMenuService menuService = new MenuService(connection);
    bool isAuthenticated = false;

    while (!isAuthenticated)
    {
      // login menu
      menuService.ShowMainMenu();
      // receive input and send to server
      menuService.HandleUserInput(connection);
      // receive messages from server
      while (true)
      {
        string message = connection.ReceiveMessage();

        if (message != null)
        {
          if (message.Contains("REGISTRATION_FAILED"))
          {
            Console.WriteLine($"Registration Failed - Username already taken");
            break;
          }
          if (message.Contains("REGISTRATION_SUCCESSFUL"))
          {
            Console.WriteLine($"Registration Successful!");
            break;
          }
          if (message.Contains("LOGIN_FAILED"))
          {
            Console.WriteLine($"Login failed!");
            break;
          }
          if (message.Contains("LOGIN_SUCCESSFUL"))
          {
            Console.WriteLine($"Login Successful!");
            isAuthenticated = true;
            break;
          }
        }
      }
    }     
    while (isAuthenticated)
    {
      menuService.ShowLoggedInMenu();
      menuService.HandleLoggedInUserInput(connection);

      string received = string.Empty;

      try
      {
        received = connection.ReceiveMessage();
      }
      catch (Exception exception)
      {
        Console.WriteLine(exception);
      }
      if (string.IsNullOrEmpty(received))
        return;
      
      Console.WriteLine("Received from server: " + received.Remove(received.Length-1));
    }
  }
}
            

          

        
    