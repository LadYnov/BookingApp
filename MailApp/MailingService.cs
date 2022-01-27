using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MailApp;

public class MailingService : IMailingService
{

    private const string Email = "YourEmail";
    private const string PasswordMail = "YourPassword";
    private readonly IModel _channel;
    private readonly SmtpClient _client;
    public MailingService()
    {
        var factory = new ConnectionFactory() {HostName = "localhost"};
        var connection = factory.CreateConnection();
        _channel = connection.CreateModel();
        _channel.QueueDeclare(queue: "Sending Mail",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        _client = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential(Email, PasswordMail),
            EnableSsl = true
        };
    }

    public void SendingMail(string message)
    {
        Console.WriteLine("En attente d'envoi de mail");
        
        var ticket = JsonSerializer.Deserialize<ConcertDto>(message);
        
        var mailMessage = new MailMessage
        {
            From = new MailAddress("bookingApp@gmail.com"),
            Subject = $"Achat du billet numéro {ticket.Ticket.Id}",
            Body = $"<h1>VOICI LE TICKET DU CONCERT</h1><br>" +
                   $"Artist : {ticket.Artist}<br>" +
                   $"Salle : {ticket.ConcertHall}" +
                   $"Numéro du Ticket: {ticket.Ticket.Id}",
            IsBodyHtml = true,
        };
        mailMessage.To.Add("onehuntmandev@gmail.com");
        try
        {
            _client.Send(mailMessage);
            Console.WriteLine("Mail envoyé");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        Console.ReadKey();
    }

    public void ReceivedQueueMessage()
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (_, ea) =>
        {
            var data = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(data);
            Console.WriteLine($"Message à envoyer : {message}" );
            SendingMail(message);
            
        };
        _channel.BasicConsume(consumer, "Sending Mail", true);
    }
}