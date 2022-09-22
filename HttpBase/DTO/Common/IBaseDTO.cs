using Microsoft.AspNetCore.Mvc;

namespace HttpBase.DTO.Common;

public interface IBaseDTO
{
    string Descripcion { get; set; }

    Guid Id { get; set; }
}

public interface IErrores
{
    ListDTO<ProblemDetails> Errores { get; set; }
}