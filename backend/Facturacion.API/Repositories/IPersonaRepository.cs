using System.Threading.Tasks;
using Facturacion.API.Models;

namespace Facturacion.API.Repositories
{
    // Repositorio específico para Persona según Repository Pattern
    public interface IPersonaRepository : IRepositorioGenerico<Persona>
    {
        Task<Persona> ObtenerPorIdentificacionAsync(string identificacion);
        Task<bool> EliminarPorIdentificacionAsync(string identificacion);
    }
}
