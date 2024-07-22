using System;
using System.Collections.Generic;

namespace PruebaBACWebAPI.Models
{
    public partial class Respuesta
    {
        public int IdRespuesta { get; set; }
        public string? Usuario { get; set; }
        public int? IdPregunta { get; set; }
        public string? Respuesta1 { get; set; }
        public DateTime? FCreacion { get; set; }

        public virtual Pregunta? IdPreguntaNavigation { get; set; }
        public virtual Usuario? UsuarioNavigation { get; set; }
    }
}
