using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PruebaBACWebApp.Models
{
    public class Usuario
    {
        [Display(Name = "Usuario")]      
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string usuario1 { get; set; } = null!; 
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string contrasena { get; set; } = null!;
        public DateTime? fCreacion { get; set; }
        public DateTime? fModificacion { get; set; }

    }
}
