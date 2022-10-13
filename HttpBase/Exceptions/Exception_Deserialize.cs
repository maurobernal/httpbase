namespace HttpBase.Exceptions;
public class Exception_Deserialize : Exception
{
    public Exception_Deserialize(int code)
        : base()
    {
        this.code = code;
    }

    public Exception_Deserialize(string message, int code)
        : base(message)
    {
        this.code = code;
    }

    public Exception_Deserialize(string message)
       : base(message)
    {
    }

    public Exception_Deserialize(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public int code { get; set; }

    public int mensaje { get; set; }
}
