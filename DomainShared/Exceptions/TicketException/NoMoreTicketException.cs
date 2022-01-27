namespace DomainShared.Exceptions.TicketException;

public class NoMoreTicket : DomainExceptionBase
{
    public override string ErrorCode => ExceptionErrorCodes.NoMoreTicket;
}