

namespace Shared;

public abstract class Message
{
    public abstract string Encode();

    public abstract int Id();
}

public class RegisterUserMessage : Message
{
    public string Username { get; set; }
    public string Password { get; set; }

    public RegisterUserMessage(string username, string password)
    {
        this.Username = username;
        this.Password = password;
    }

    public override string Encode()
    {
        return $"{this.Username}:{this.Password}";
    }

    public static Message Decode(string message)
    {
        string[] split = message.Split(":");
        return new RegisterUserMessage(split[0], split[1]);
    }

    public override int Id()
    {
        return 10;
    }
}

public class LoginMessage : Message
{
    public string Username { get; set; }
    public string Password { get; set; }

    public LoginMessage(string username, string password)
    {
        this.Username = username;
        this.Password = password;
    }

    public override string Encode()
    {
        return $"{this.Username}:{this.Password}";
    }

    public static Message Decode(string message)
    {
        string[] split = message.Split(":");
        return new LoginMessage(split[0], split[1]);
    }

    public override int Id()
    {
        return 11;
    }
}
public class SendMessage : Message
{
    public string Message { get; set; }
    public string Username { get; set; }


    public SendMessage(string username, string message)
    {
        this.Username = username;
        this.Message = message;
    }

    public override string Encode()
    {
        return $"{this.Username}:{this.Message}";
    }

    public static Message Decode(string message)
    {
        string[] split = message.Split(":");
        return new SendMessage(split[0], split[1]);
    }

    public override int Id()
    {
        return 12;
    }
}
