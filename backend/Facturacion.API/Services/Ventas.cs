using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Facturacion.API.Models;
using Facturacion.API.Repositories;
using Microsoft.Extensions.Logging;

namespace Facturacion.API.Services
{
    // Servicio de negocio para facturación
    public class Ventas : IVentas
    {
        private readonly IFacturaRepository _repositorioFactura;
        private readonly IPersonaRepository _repositorioPersona;
        private readonly ILogger<Ventas> _logger;

        public Ventas(IFacturaRepository repositorioFactura, IPersonaRepository repositorioPersona, ILogger<Ventas> logger)
        {
            _repositorioFactura = repositorioFactura;
            _repositorioPersona = repositorioPersona;
            _logger = logger;
        }

        public async Task<Factura> CrearFacturaAsync(Factura factura, decimal tasaImpuesto)
        {
            // Validaciones básicas
            if (factura == null) throw new ArgumentNullException(nameof(factura));
            if (factura.Lineas == null || !factura.Lineas.Any()) throw new ArgumentException("La factura debe tener al menos una línea");

            // Verificar que la persona exista
            var persona = await _repositorioPersona.ObtenerPorIdAsync(factura.PersonaId);
            if (persona == null) throw new InvalidOperationException("La persona indicada no existe");

            // Calcular subtotal
            factura.Subtotal = factura.Lineas.Sum(l => l.Precio * l.Cantidad);

            // Calcular impuesto y total
            factura.Impuesto = Math.Round(factura.Subtotal * tasaImpuesto, 2);
            factura.Total = factura.Subtotal + factura.Impuesto;
            factura.Fecha = DateTime.UtcNow;

            var creada = await _repositorioFactura.AgregarAsync(factura);
            _logger.LogInformation("Factura creada Id={id} PersonaId={personaId} Total={total}", creada.Id, creada.PersonaId, creada.Total);
            return creada;
        }

        public async Task<Factura> ObtenerPorIdAsync(int id)
        {
            return await _repositorioFactura.ObtenerPorIdAsync(id);
        }

        public async Task<IEnumerable<Factura>> ObtenerFacturasPorPersonaAsync(int personaId)
        {
            return await _repositorioFactura.ObtenerPorPersonaIdAsync(personaId);
        }
    }
}
