
namespace HttpBase.Exceptions;

public class Exception_NotAuthorized : Exception
{
    public Exception_NotAuthorized(int code)
        : base()
    {
        this.code = code;
    }

    public Exception_NotAuthorized(string message, int code)
        : base(message)
    {
        this.code = code;
    }

    public Exception_NotAuthorized(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public int code { get; set; }

    public int mensaje { get; set; }
}
