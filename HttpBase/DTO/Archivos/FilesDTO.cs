using HttpBase.DTO.Common;
using Microsoft.AspNetCore.Mvc;

namespace HttpBase.DTO.Archivos;

public class FileGetDTO : IErrores
{
    public Stream File { get; set; }

    public string Base64 { get; set; } = string.Empty;

    public ListDTO<ProblemDetails> Errores { get; set; } = new();

    public string Nombre { get; set; } = string.Empty;
}

public class FilePostDTO : IErrores
{
    public string fileName { get; set; } = string.Empty;

    public string contentType { get; set; } = string.Empty;

    public long length { get; set; }

    public string etag { get; set; } = string.Empty;

    public ListDTO<ProblemDetails> Errores { get; set; } = new();
}

public class File64PostDTO : BaseDTO, IErrores
{
    public string fileName { get; set; } = string.Empty;

    public string contentType { get; set; } = string.Empty;

    public string file64 { get; set; } = string.Empty;

    public string contenedor { get; set; } = string.Empty;

    public long length { get; set; }

    public string etag { get; set; } = string.Empty;

    public new ListDTO<ProblemDetails> Errores { get; set; } = new();
}
