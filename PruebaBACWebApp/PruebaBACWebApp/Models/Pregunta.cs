using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PruebaBACWebApp.Models
{
    public class Pregunta
    {
        public int idPregunta { get; set; }
        public string? usuario { get; set; }
        [Display(Name = "Pregunta")]
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string? pregunta1 { get; set; }
        public string? estado { get; set; }
        public DateTime? fCreacion { get; set; }
        public virtual Usuario? UsuarioNavigation { get; set; }
        public Respuesta? registro { get; set; }
        public virtual ICollection<Respuesta>? respuesta { get; set; }
    }
}
