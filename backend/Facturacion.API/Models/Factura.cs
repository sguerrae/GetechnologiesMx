using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Facturacion.API.Models
{
    // Modelo de dominio que representa una factura
    public class Factura
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        // Total antes de impuestos
        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal { get; set; }

        // Monto de impuestos
        [Column(TypeName = "decimal(18,2)")]
        public decimal Impuesto { get; set; }

        // Total final
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        // FK a persona
        public int PersonaId { get; set; }
        public Persona Persona { get; set; }

        // Líneas de detalle
        public List<LineaFactura> Lineas { get; set; } = new List<LineaFactura>();
    }
}
