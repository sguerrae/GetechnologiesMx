using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Facturacion.API.Data;
using Facturacion.API.Models;

namespace Facturacion.API.Repositories
{
    // Implementación del repositorio de facturas
    public class FacturaRepository : IFacturaRepository
    {
        private readonly FacturaDbContext _contexto;

        public FacturaRepository(FacturaDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<Factura> AgregarAsync(Factura entidad)
        {
            _contexto.Facturas.Add(entidad);
            await _contexto.SaveChangesAsync();
            return entidad;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var existente = await _contexto.Facturas.FindAsync(id);
            if (existente == null) return false;
            _contexto.Facturas.Remove(existente);
            await _contexto.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Factura>> ObtenerTodosAsync()
        {
            return await _contexto.Facturas.Include(f => f.Lineas).ToListAsync();
        }

        public async Task<Factura> ObtenerPorIdAsync(int id)
        {
            return await _contexto.Facturas.Include(f => f.Lineas).FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<Factura>> ObtenerPorPersonaIdAsync(int personaId)
        {
            return await _contexto.Facturas.Where(f => f.PersonaId == personaId).Include(f => f.Lineas).ToListAsync();
        }

        public async Task<Factura> ActualizarAsync(Factura entidad)
        {
            _contexto.Facturas.Update(entidad);
            await _contexto.SaveChangesAsync();
            return entidad;
        }
    }
}
