namespace HttpBase.Models.Common;

public class Respuesta_Get_Single_Models<T>
{
    public _Request_Get_Single request { get; set; }

    public T resultado { get; set; }

    public bool isError { get; set; }

    public string error { get; set; }
}

public class _Request_Get_Single
{
    public string id { get; set; }
}
