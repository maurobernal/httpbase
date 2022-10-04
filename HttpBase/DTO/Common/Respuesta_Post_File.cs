namespace HttpBase.DTO.Common;

public class Respuesta_Post_File<T>
{
    public T resultado { get; set; }

    public bool isError { get; set; }

    public string error { get; set; } = string.Empty;
}
