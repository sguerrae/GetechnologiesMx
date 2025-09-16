using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Facturacion.API.Data;
using Facturacion.API.Models;

namespace Facturacion.API.Repositories
{
    // Implementación del repositorio de persona usando EF Core
    public class PersonaRepository : IPersonaRepository
    {
        private readonly FacturaDbContext _contexto;

        public PersonaRepository(FacturaDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<Persona> AgregarAsync(Persona entidad)
        {
            _contexto.Personas.Add(entidad);
            await _contexto.SaveChangesAsync();
            return entidad;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var existente = await _contexto.Personas.FindAsync(id);
            if (existente == null) return false;
            _contexto.Personas.Remove(existente);
            await _contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarPorIdentificacionAsync(string identificacion)
        {
            var persona = await _contexto.Personas.FirstOrDefaultAsync(p => p.Identificacion == identificacion);
            if (persona == null) return false;
            _contexto.Personas.Remove(persona);
            await _contexto.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Persona>> ObtenerTodosAsync()
        {
            return await _contexto.Personas.Include(p => p.Facturas).ToListAsync();
        }

        public async Task<Persona> ObtenerPorIdAsync(int id)
        {
            return await _contexto.Personas.Include(p => p.Facturas).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Persona> ObtenerPorIdentificacionAsync(string identificacion)
        {
            return await _contexto.Personas.Include(p => p.Facturas).FirstOrDefaultAsync(p => p.Identificacion == identificacion);
        }

        public async Task<Persona> ActualizarAsync(Persona entidad)
        {
            _contexto.Personas.Update(entidad);
            await _contexto.SaveChangesAsync();
            return entidad;
        }
    }
}
