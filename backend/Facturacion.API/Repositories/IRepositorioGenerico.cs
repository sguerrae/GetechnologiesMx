using System.Collections.Generic;
using System.Threading.Tasks;

namespace Facturacion.API.Repositories
{
    // Interfaz genérica para repositorios (operaciones básicas)
    public interface IRepositorioGenerico<T> where T : class
    {
        Task<T> ObtenerPorIdAsync(int id);
        Task<IEnumerable<T>> ObtenerTodosAsync();
        Task<T> AgregarAsync(T entidad);
        Task<T> ActualizarAsync(T entidad);
        Task<bool> EliminarAsync(int id);
    }
}
