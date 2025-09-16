using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Facturacion.API.Models
{
    // Modelo de dominio que representa a una persona
    public class Persona
    {
        [Key]
        public int Id { get; set; }

        // Nombre es obligatorio
        [Required]
        public string Nombre { get; set; }

        // Apellido paterno obligatorio
        [Required]
        public string ApellidoPaterno { get; set; }

        // Apellido materno opcional
        public string ApellidoMaterno { get; set; }

        // Identificación obligatoria
        [Required]
        public string Identificacion { get; set; }

        // Relación a facturas de la persona
        public List<Factura> Facturas { get; set; } = new List<Factura>();
    }
}
