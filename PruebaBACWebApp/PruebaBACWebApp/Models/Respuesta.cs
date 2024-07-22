namespace PruebaBACWebApp.Models
{
    public class Respuesta
    {
        public int idRespuesta { get; set; }
        public string? usuario { get; set; }
        public int? idPregunta { get; set; }
        public string respuesta1 { get; set; }
        public DateTime? fCreacion { get; set; }

    }
}
