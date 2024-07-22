using System;
using System.Collections.Generic;

namespace PruebaBACWebAPI.Models
{
    public partial class Pregunta
    {
        public Pregunta()
        {
            Respuesta = new HashSet<Respuesta>();
        }

        public int IdPregunta { get; set; }
        public string? Usuario { get; set; }
        public string? Pregunta1 { get; set; }
        public string? Estado { get; set; }
        public DateTime? FCreacion { get; set; }

        public virtual Usuario? UsuarioNavigation { get; set; }
        public virtual ICollection<Respuesta> Respuesta { get; set; }
    }
}
