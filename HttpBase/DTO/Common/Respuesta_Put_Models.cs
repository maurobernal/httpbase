
namespace HttpBase.Models.Common;

public class Respuesta_Put_Models<T>
{
    public T request { get; set; }

    public Guid resultado { get; set; }

    public bool isError { get; set; }

    public string error { get; set; }
}
