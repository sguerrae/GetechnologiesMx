using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Facturacion.API.Models;
using Facturacion.API.Repositories;
using Microsoft.Extensions.Logging;

namespace Facturacion.API.Services
{
    // Servicio de negocio para gestionar personas
    public class Directorio : IDirectorio
    {
        private readonly IPersonaRepository _repositorioPersona;
        private readonly ILogger<Directorio> _logger;

        public Directorio(IPersonaRepository repositorioPersona, ILogger<Directorio> logger)
        {
            _repositorioPersona = repositorioPersona;
            _logger = logger;
        }

        public async Task<Persona> CrearPersonaAsync(Persona persona)
        {
            // Validaciones: todos los campos obligatorios excepto ApellidoMaterno
            if (string.IsNullOrWhiteSpace(persona.Nombre) || string.IsNullOrWhiteSpace(persona.ApellidoPaterno) || string.IsNullOrWhiteSpace(persona.Identificacion))
            {
                throw new ArgumentException("Todos los campos obligatorios deben estar presentes");
            }

            // Guardar
            var creada = await _repositorioPersona.AgregarAsync(persona);
            _logger.LogInformation("Persona creada: {identificacion}", creada.Identificacion);
            return creada;
        }

        public async Task<bool> EliminarPorIdentificacionAsync(string identificacion)
        {
            _logger.LogInformation("Eliminando persona con identificacion {id}", identificacion);
            return await _repositorioPersona.EliminarPorIdentificacionAsync(identificacion);
        }

        public async Task<Persona> ObtenerPorIdAsync(int id)
        {
            return await _repositorioPersona.ObtenerPorIdAsync(id);
        }

        public async Task<IEnumerable<Persona>> ObtenerPersonasAsync()
        {
            return await _repositorioPersona.ObtenerTodosAsync();
        }
    }
}
