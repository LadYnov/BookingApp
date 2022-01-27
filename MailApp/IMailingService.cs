namespace MailApp;

public interface IMailingService
{
    void SendingMail(string message);
    void ReceivedQueueMessage();

}