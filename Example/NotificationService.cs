namespace Example;

public interface INotificationService
{
    string Notify(string message);
}

public class HappyNotificationService : INotificationService
{
    public string Notify(string message) => $"😁 {message}";
}

public class AngryNotificationService : INotificationService
{
    public string Notify(string message) => $"🤬 {message}";
}

public class SillyNotificationService : INotificationService
{
    public string Notify(string message) => $"🤪  {message}";
}