using System.ComponentModel.DataAnnotations;

namespace GestionViajes.Models
{
    public class Destino
    {
        public int DestinoId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El país es obligatorio")]
        [StringLength(60)]
        [Display(Name = "País")]
        public string Pais { get; set; } = string.Empty;

        [StringLength(300)]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El costo es obligatorio")]
        [Range(0, double.MaxValue, ErrorMessage = "El costo debe ser mayor o igual a 0")]
        [Display(Name = "Costo")]
        public decimal Costo { get; set; }

        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}