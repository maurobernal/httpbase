using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace HttpBase.DTO.Common;

public class BaseDTO : IBaseDTO, IErrores
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "El campo es requerido")]
    [StringLength(50, ErrorMessage = "El campo debe tener entre 2 y 50 caracteres.", MinimumLength = 2)]
    public string Descripcion { get; set; }

    public ListDTO<ProblemDetails> Errores { get; set; }
}
