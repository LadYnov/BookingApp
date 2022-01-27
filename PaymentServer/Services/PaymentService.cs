using System.Text.RegularExpressions;
using Grpc.Core;

namespace PaymentServer.Services;

public class PaymentService : Greeter.GreeterBase, IPaymentService
{
    private readonly ILogger<PaymentService> _logger;

    private const string CardNumber =
        @"(^4[0-9]{12}(?:[0-9]{3})?$)|(^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$)|(3[47][0-9]{13})|(^3(?:0[0-5]|[68][0-9])[0-9]{11}$)|(^6(?:011|5[0-9]{2})[0-9]{12}$)|(^(?:2131|1800|35\d{3})\d{11}$)";

    private readonly PaymentResponse _paymentSuccess = new()
    {
        PaymentSuccessed = false
    };

    

    public PaymentService(ILogger<PaymentService> logger)
    {
        _logger = logger;
    }

    public override Task<PaymentResponse> SendPaymentInformation(PaymentRequest request, ServerCallContext context)
    {
        return IsPaymentRequestCorrect(request);
    }

    private Task<PaymentResponse> IsPaymentRequestCorrect(PaymentRequest request)
    {
        if (IsStringEmpty(request.Name))
        {
            return PaymentFailed();
        }

        if (IsStringEmpty(request.NumberCard))
        {
            return PaymentFailed();
        }

        if (IsStringEmpty(request.DateCard))
        {
            return PaymentFailed();
        }

        if (IsStringEmpty(request.SecurityCode))
        {
            return PaymentFailed();
        }

        return PaymentSuccess();
    }

    private Task<PaymentResponse> PaymentSuccess()
    {
        _paymentSuccess.PaymentSuccessed = true;
        return Task.FromResult(_paymentSuccess);
    }

    private Task<PaymentResponse> PaymentFailed()
    {
        return Task.FromResult(_paymentSuccess);
    }

    private static bool IsStringEmpty(string name)
    {
        return name == "";
    }
    
    public bool IsNumberCardCorrect(string numberCard)
    {
        return IsStringDoesMatchRegex(CardNumber, numberCard);
    }
    
    private static bool IsStringDoesMatchRegex(string pattern,string parametersToTest)
    {
        var rx = new Regex(pattern);
        var match = rx.Match(parametersToTest);
        return match.Success;
    }
}