using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Facturacion.API.Services;
using Facturacion.API.Models;

namespace Facturacion.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DirectorioRestService : ControllerBase
    {
        private readonly IDirectorio _directorio;

        public DirectorioRestService(IDirectorio directorio)
        {
            _directorio = directorio;
        }

        // POST api/directorio
        [HttpPost]
        public async Task<IActionResult> CrearPersona([FromBody] Persona persona)
        {
            try
            {
                var creada = await _directorio.CrearPersonaAsync(persona);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = creada.Id }, creada);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // GET api/directorio
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var personas = await _directorio.ObtenerPersonasAsync();
            return Ok(personas);
        }

        // GET api/directorio/5
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var persona = await _directorio.ObtenerPorIdAsync(id);
            if (persona == null) return NotFound();
            return Ok(persona);
        }

        // DELETE api/directorio/identificacion/{identificacion}
        [HttpDelete("identificacion/{identificacion}")]
        public async Task<IActionResult> EliminarPorIdentificacion(string identificacion)
        {
            var resultado = await _directorio.EliminarPorIdentificacionAsync(identificacion);
            if (!resultado) return NotFound();
            return NoContent();
        }
    }
}
