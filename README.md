# httpbase
Http Client Base for NET 7.0

##Nuget
dotnet add package HttpBase


[![Nuget](https://github.com/maurobernal/httpbase/actions/workflows/dotnet_nuget.yml/badge.svg?branch=main)](https://github.com/maurobernal/httpbase/actions/workflows/dotnet_nuget.yml)


[![BuildAndTesting](https://github.com/maurobernal/httpbase/actions/workflows/dotnet_build_and_testing.yml/badge.svg)](https://github.com/maurobernal/httpbase/actions/workflows/dotnet_build_and_testing.yml)


###Resume
This client required an model explicit for diferents actions

Examples
####Get (Single- one registry)
```
public class Respuesta_Get_Single_Models<T>
{
    public _Request_Get_Single request { get; set; }

    public T resultado { get; set; }

    public bool isError { get; set; }

    public string error { get; set; } = string.Empty;
}

public class _Request_Get_Single
{
    public string id { get; set; } = string.Empty;
}

```
####Get (List- Arrays)
public class Respuesta_Get_Full_Models<T, TR>
{
    public Request_Get_Full request { get; set; }

    public Resultado_Get_Full<T, TR> resultado { get; set; }

    public bool isError { get; set; }

    public string error { get; set; } = string.Empty;

    public DateTime? fechaDesde { get; set; }

    public DateTime? fechaHasta { get; set; }
}

public class Respuesta_Get_Full_Models<T>
{
    public Request_Get_Full request { get; set; }

    public Resultado_Get<T> resultado { get; set; }

    public bool isError { get; set; }

    public string error { get; set; } = string.Empty;

    public DateTime? fechaDesde { get; set; }

    public DateTime? fechaHasta { get; set; }
}

public class Request_Get_Full
{
    public int nroPag { get; set; }

    public int resPorPag { get; set; }

    public string descripcion { get; set; } = string.Empty;

    public string estado { get; set; } = string.Empty;

    public DateTime? fechaDesde { get; set; }

    public DateTime? fechaHasta { get; set; }
}

public class Resultado_Get<T>
{
    public T items { get; set; }

    public int pageIndex { get; set; }

    public int totalPages { get; set; }

    public int totalCount { get; set; }

    public bool hasPreviousPage { get; set; }

    public bool hasNextPage { get; set; }
}

public class Resultado_Get_Full<T, TR>
{
    public T items { get; set; } 

    public DateTime? fechaDesde { get; set; }

    public DateTime? fechaHasta { get; set; }

    public TR header { get; set; }

    public int pageIndex { get; set; }

    public int totalPages { get; set; }

    public int totalCount { get; set; }

    public bool hasPreviousPage { get; set; }

    public bool hasNextPage { get; set; }
}
