using System.Text;
using RabbitMQ.Client;

namespace WebApp.Services;

public class MailingService : IMailingService
{
    private readonly IModel _channel;

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
    }

    public void SendingMessageQueue(string message)
    {
        var data = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(exchange:"",
            routingKey:"Sending Mail",
            basicProperties:null,
            body:data);
    }
}