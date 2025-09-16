using System.Collections.Generic;
using System.Threading.Tasks;
using Facturacion.API.Models;

namespace Facturacion.API.Services
{
    // Contrato del servicio de Directorio (gestión de personas)
    public interface IDirectorio
    {
        Task<Persona> CrearPersonaAsync(Persona persona);
        Task<IEnumerable<Persona>> ObtenerPersonasAsync();
        Task<Persona> ObtenerPorIdAsync(int id);
        Task<bool> EliminarPorIdentificacionAsync(string identificacion);
    }
}
