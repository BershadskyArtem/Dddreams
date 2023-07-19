namespace Dddreams.Application.Common.Exceptions;

public class BadRequestException : ApplicationException
{
    public string[] Errors { get; set; } 
    
    public BadRequestException() : base()
    {
    }

    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(string message, Exception innerException) : base(message, innerException)
    {
    }
    
    public BadRequestException(string[] message) : base("Multiple errors occured while processing your request.")
    {
        Errors = message;
    }
}