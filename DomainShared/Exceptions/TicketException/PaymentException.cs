namespace DomainShared.Exceptions.TicketException;

public class PaymentException : DomainExceptionBase
{
    public override string ErrorCode => ExceptionErrorCodes.PaymentException;
}