
namespace HttpBase.Exceptions;

public class Exception_NotFound : Exception
{
    public Exception_NotFound(int code)
        : base()
    {
        this.code = code;
    }

    public Exception_NotFound(string message, int code)
        : base(message)
    {
        this.code = code;
    }

    public Exception_NotFound(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public int code { get; set; }

    public int mensaje { get; set; }
}
