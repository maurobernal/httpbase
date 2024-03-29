﻿namespace HttpBase.Models.Common;
public class Respuesta_Delete_Models<T>
{
    public T request { get; set; }

    public Guid resultado { get; set; }

    public bool isError { get; set; }

    public string error { get; set; } = string.Empty;
}

public class Respuesta_Delete_Full_Models<T>
{
    public T request { get; set; }

    public List<Guid> resultado { get; set; } = new List<Guid>();

    public bool isError { get; set; }

    public string error { get; set; } = string.Empty;
}