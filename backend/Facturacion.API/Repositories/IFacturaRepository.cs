using System.Collections.Generic;
using System.Threading.Tasks;
using Facturacion.API.Models;

namespace Facturacion.API.Repositories
{
    // Repositorio para Factura
    public interface IFacturaRepository : IRepositorioGenerico<Factura>
    {
        Task<IEnumerable<Factura>> ObtenerPorPersonaIdAsync(int personaId);
    }
}
