namespace Server.Interfaces;
public interface IConnectionHandler
{
    Shared.IConnection? Accept();
    void HandleReads(IUserService userService);
}