
namespace HttpBase.Models.Common;

public class Respuesta_Get_Full_Models<T, TR>
{
    public Request_Get_Full request { get; set; }

    public Resultado_Get_Full<T, TR> resultado { get; set; }

    public bool isError { get; set; }

    public string error { get; set; }

    public DateTime? fechaDesde { get; set; }

    public DateTime? fechaHasta { get; set; }
}

public class Respuesta_Get_Full_Models<T>
{
    public Request_Get_Full request { get; set; }

    public Resultado_Get<T> resultado { get; set; }

    public bool isError { get; set; }

    public string error { get; set; }

    public DateTime? fechaDesde { get; set; }

    public DateTime? fechaHasta { get; set; }
}

public class Request_Get_Full
{
    public int nroPag { get; set; }

    public int resPorPag { get; set; }

    public string descripcion { get; set; }

    public string estado { get; set; }

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
