namespace HttpBase.DTO.Common;
public class Mensajes_Models
{
    public Mensajes_Models()
    {
        Fecha = DateTime.Now;
    }

    public DateTime Fecha { get; set; }

    public string Mensaje { get; set; }

    public string Excepcion { get; set; }
}
