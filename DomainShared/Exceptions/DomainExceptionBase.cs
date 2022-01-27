namespace DomainShared.Exceptions;

public class DomainExceptionBase : Exception
{
    public virtual string ErrorCode => ExceptionErrorCodes.DomainExceptionBase;
}