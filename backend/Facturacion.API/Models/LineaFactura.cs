using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Facturacion.API.Models
{
    // Representa una línea de factura
    public class LineaFactura
    {
        [Key]
        public int Id { get; set; }

        // Descripción del producto o servicio
        [Required]
        public string Descripcion { get; set; }

        // Cantidad debe ser positiva
        [Required]
        public int Cantidad { get; set; }

        // Precio unitario
        [Column(TypeName = "decimal(18,2)")]
        [Required]
        public decimal Precio { get; set; }

        // FK a factura
        public int FacturaId { get; set; }
        public Factura Factura { get; set; }
    }
}
