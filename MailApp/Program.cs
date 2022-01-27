// See https://aka.ms/new-console-template for more information
using MailApp;




Console.WriteLine("Hello, World!");

var mailing = new MailingService();
while (true)
{
    mailing.ReceivedQueueMessage();
}



