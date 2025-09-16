using Microsoft.EntityFrameworkCore;
using Facturacion.API.Models;

namespace Facturacion.API.Data
{
    // DbContext para el ejercicio usando SQLite embebida
    public class FacturaDbContext : DbContext
    {
        public FacturaDbContext(DbContextOptions<FacturaDbContext> opciones) : base(opciones)
        {
        }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<LineaFactura> LineasFactura { get; set; }

        protected override void OnModelCreating(ModelBuilder modelo)
        {
            base.OnModelCreating(modelo);

            // Configuraciones simples
            modelo.Entity<Persona>()
                .HasIndex(p => p.Identificacion)
                .IsUnique();

            modelo.Entity<Persona>()
                .HasMany(p => p.Facturas)
                .WithOne(f => f.Persona)
                .HasForeignKey(f => f.PersonaId)
                .OnDelete(DeleteBehavior.Cascade); // borrar persona elimina también facturas

            modelo.Entity<Factura>()
                .HasMany(f => f.Lineas)
                .WithOne(l => l.Factura)
                .HasForeignKey(l => l.FacturaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
