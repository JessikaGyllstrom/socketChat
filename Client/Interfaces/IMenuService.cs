using Shared;

namespace Client.Interfaces;

public interface IMenuService
{
    void ShowMainMenu();
    void ShowLoggedInMenu();
    void HandleUserInput(IConnection connection);
    void HandleLoggedInUserInput(IConnection connection);
}
