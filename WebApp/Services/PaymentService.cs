using System;
using System.Threading.Tasks;
using Grpc.Net.Client;

namespace WebApp.Services;

public class PaymentService : IPaymentService
{
    private readonly GrpcChannel _channel;
    public PaymentService()
    {
        _channel = GrpcChannel.ForAddress("https://localhost:7233");
    }
    public async Task<bool> SendPaymentInformation()
    {
        var client = new Greeter.GreeterClient(_channel);
        var reply = await client.SendPaymentInformationAsync(new PaymentRequest
        {
            Id = 1,
            DateCard = "01/23",
            NumberCard = "4569403961014710",
            Name = "Guillaume Ladniak",
            Montant = 17.1f,
            SecurityCode = "519"

        });
        return reply.PaymentSuccessed;
    }
}