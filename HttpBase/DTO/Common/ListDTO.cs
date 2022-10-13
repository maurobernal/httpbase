using Microsoft.AspNetCore.Mvc;

namespace HttpBase.DTO.Common;

public class ListDTO<T> : List<T>, IBaseDTO, IErrores
{
    public string Descripcion { get; set; } = string.Empty;

    public Guid Id { get; set; }

    public ListDTO<ProblemDetails> Errores { get; set; } 
}
