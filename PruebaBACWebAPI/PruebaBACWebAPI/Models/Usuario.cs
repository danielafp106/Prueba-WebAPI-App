using System;
using System.Collections.Generic;

namespace PruebaBACWebAPI.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Pregunta = new HashSet<Pregunta>();
            Respuesta = new HashSet<Respuesta>();
        }

        public string Usuario1 { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
        public DateTime? FCreacion { get; set; }
        public DateTime? FModificacion { get; set; }

        public virtual ICollection<Pregunta> Pregunta { get; set; }
        public virtual ICollection<Respuesta> Respuesta { get; set; }
    }
}
