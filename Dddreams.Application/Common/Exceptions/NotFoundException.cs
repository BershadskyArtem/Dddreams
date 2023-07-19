namespace Dddreams.Application.Common.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException() : base()
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public NotFoundException(string name,string key) : base($"Entity \"{name}\" cannot be found by key: {key}")
    {
    }
}