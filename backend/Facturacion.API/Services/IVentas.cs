using System.Collections.Generic;
using System.Threading.Tasks;
using Facturacion.API.Models;

namespace Facturacion.API.Services
{
    // Contrato para el servicio de facturación
    public interface IVentas
    {
        Task<Factura> CrearFacturaAsync(Factura factura, decimal tasaImpuesto);
        Task<IEnumerable<Factura>> ObtenerFacturasPorPersonaAsync(int personaId);
        Task<Factura> ObtenerPorIdAsync(int id);
    }
}
