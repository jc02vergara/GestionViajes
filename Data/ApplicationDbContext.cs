using GestionViajes.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionViajes.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Destino> Destinos { get; set; } = null!;
        public DbSet<Turista> Turistas { get; set; } = null!;
        public DbSet<Reserva> Reservas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Destino)
                .WithMany(d => d.Reservas)
                .HasForeignKey(r => r.DestinoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Turista)
                .WithMany(t => t.Reservas)
                .HasForeignKey(r => r.TuristaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}