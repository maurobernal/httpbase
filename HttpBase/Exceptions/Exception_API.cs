namespace HttpBase.Exceptions;
public class Exception_API : Exception
{
    public Exception_API(int code)
        : base()
    {
        this.code = code;
    }

    public Exception_API(string message, int code)
        : base(message)
    {
        this.code = code;
    }

    public Exception_API(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public int code { get; set; }

    public int mensaje { get; set; }
}
