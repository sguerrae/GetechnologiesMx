using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Facturacion.API.Services;
using Facturacion.API.Models;
using System;

namespace Facturacion.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturaRestService : ControllerBase
    {
        private readonly IVentas _ventas;

        public FacturaRestService(IVentas ventas)
        {
            _ventas = ventas;
        }

        // POST api/factura?tasaImpuesto=0.16
        [HttpPost]
        public async Task<IActionResult> CrearFactura([FromBody] Factura factura, [FromQuery] decimal tasaImpuesto = 0.16m)
        {
            try
            {
                var creada = await _ventas.CrearFacturaAsync(factura, tasaImpuesto);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = creada.Id }, creada);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // GET api/factura/persona/5
        [HttpGet("persona/{personaId}")]
        public async Task<IActionResult> ObtenerPorPersona(int personaId)
        {
            var facturas = await _ventas.ObtenerFacturasPorPersonaAsync(personaId);
            return Ok(facturas);
        }

        // GET api/factura/5
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var factura = await _ventas.ObtenerPorIdAsync(id);
            if (factura == null) return NotFound();
            return Ok(factura);
        }
    }
}
