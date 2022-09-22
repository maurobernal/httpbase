﻿using Microsoft.AspNetCore.Mvc;

namespace HttpBase.DTO.Common;

public class ListPostDTO<T> : List<T>, IBaseDTO, IErrores
{
    public string Descripcion { get; set; }

    public Guid Id { get; set; }

    public ListDTO<ProblemDetails> Errores { get; set; }
}
