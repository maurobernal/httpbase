namespace HttpBase.Models.Common;

public class Respuesta_Post_Models<T>
{
    public T request { get; set; }

    public Guid resultado { get; set; }

    public bool isError { get; set; }

    public string error { get; set; }
}

public class Respuesta_Post_Full_Models<T>
{
    public T request { get; set; }

    public List<Guid> resultado { get; set; } = new List<Guid>();

    public bool isError { get; set; }

    public string error { get; set; }
}

public class Respuesta_Post_File_Models<T>
{
    public T request { get; set; }

    public T resultado { get; set; }

    public bool isError { get; set; }

    public string error { get; set; }
}
