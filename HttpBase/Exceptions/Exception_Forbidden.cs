
namespace HttpBase.Exceptions;

public class Exception_Forbidden : Exception
{
    public Exception_Forbidden(int code)
        : base()
    {
        this.code = code;
    }

    public Exception_Forbidden(string message, int code)
        : base(message)
    {
        this.code = code;
    }

    public Exception_Forbidden(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public int code { get; set; }

    public int mensaje { get; set; }
}
