using HttpBase.DTO.Common;
using Microsoft.AspNetCore.Mvc;

namespace HttpBase.DTO.Archivos; 
public class FileGetDTO : IErrores
{
    public Stream File { get; set; }

    public string Base64 { get; set; }

    public ListDTO<ProblemDetails> Errores { get; set; }

    public string Nombre { get; set; }
}

public class FilePostDTO : IErrores
{
    public string fileName { get; set; }

    public string contentType { get; set; }

    public long length { get; set; }

    public string etag { get; set; }

    public ListDTO<ProblemDetails> Errores { get; set; }
}

public class File64PostDTO : BaseDTO, IErrores
{
    public string fileName { get; set; }

    public string contentType { get; set; }

    public string file64 { get; set; }

    public string contenedor { get; set; }

    public long length { get; set; }

    public string etag { get; set; }

    public new ListDTO<ProblemDetails> Errores { get; set; } = new();
}
