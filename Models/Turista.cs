using System.ComponentModel.DataAnnotations;

namespace GestionViajes.Models
{
    public class Turista
    {
        public int TuristaId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(60)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(60)]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo no válido")]
        [StringLength(100)]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; } = string.Empty;

        [StringLength(20)]
        [Display(Name = "Teléfono")]
        public string? Telefono { get; set; }

        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}