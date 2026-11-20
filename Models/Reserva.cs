using System.ComponentModel.DataAnnotations;

namespace GestionViajes.Models
{
    public class Reserva
    {
        public int ReservaId { get; set; }

        [Required]
        [Display(Name = "Destino")]
        public int DestinoId { get; set; }
        public Destino? Destino { get; set; }

        [Required]
        [Display(Name = "Turista")]
        public int TuristaId { get; set; }
        public Turista? Turista { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de reserva")]
        public DateTime FechaReserva { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de viaje")]
        public DateTime FechaViaje { get; set; } = DateTime.Now.AddDays(7);

        [Range(1, 20, ErrorMessage = "La cantidad de personas debe ser entre 1 y 20")]
        [Display(Name = "Personas")]
        public int Personas { get; set; } = 1;

        [Display(Name = "Total")]
        public decimal Total { get; set; }

        [StringLength(30)]
        [Display(Name = "Estado")]
        public string Estado { get; set; } = "Pendiente";
    }
}