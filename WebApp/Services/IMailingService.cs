namespace WebApp.Services;

public interface IMailingService
{
    public void SendingMessageQueue(string message);
}