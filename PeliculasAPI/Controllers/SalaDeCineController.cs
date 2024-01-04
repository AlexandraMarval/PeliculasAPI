using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.Modelos;
using PeliculasAPI.Servicios;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/salasDeCine")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SalaDeCineController : ControllerBase
    {
        private readonly ISalaDeCineServicio servicio;

        public SalaDeCineController(ISalaDeCineServicio servicio)
        {
            this.servicio = servicio;
        }

        [HttpGet(Name = "ObtenerListadoSalaDeCine")]
        public async Task<ActionResult<List<SalaDeCineModelo>>> ObtenerListadoDeSalaDeCine()
        {
            var salaDeCine = await servicio.ObtenerSalaDeCine();
            if (salaDeCine == null)
            {
                NotFound("No se encontro resultado de su busqueda");
            }
            return Ok(salaDeCine);
        }

        [HttpGet("{id}", Name = "ObtenerSalaDeCinePorId")]
        public async Task<ActionResult<SalaDeCineModelo>> ObtenerPorId(int id)
        {
            var salaDeCine = await servicio.ObtenerPorId(id);
            if (salaDeCine == null)
            {
                NotFound("No se encontro el id");
            }
            return Ok(salaDeCine);
        }

        [HttpGet("Cercanos")]
        public async Task<ActionResult<List<SalaDeCineCercanoModelo>>> Cercanos([FromQuery] SalaDeCineCercanoFiltroModelo filtroModelo
            )
        {
            var salaDeCine = await servicio.Cercano(filtroModelo);
            if (salaDeCine == null)
            {
                return NotFound("No se encontro resultado");
            }
            return Ok(salaDeCine);
        }

        [HttpPost(Name = "CrearSalaDeCine")]
        public async Task<ActionResult> CrearSalaDeCine([FromBody]CrearSalaDeCineModelo crearSalaDeCineModelo)
        {
            var salaDeCine = await servicio.CrearSalaDeCine(crearSalaDeCineModelo);
            if (salaDeCine == null)
            {
                return NotFound("No se encontro resultado");
            }
            return new CreatedAtRouteResult("ObtenerSalaDeCinePorId", new { id = salaDeCine.Id}, salaDeCine);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarSalaDeCine(int id, [FromBody] ActualizarSalaDeCineModelo actualizarSalaDeCineModelo)
        {
            var salaDeCine = await servicio.ActualizarSalaDeCine(id, actualizarSalaDeCineModelo);
            return Ok(salaDeCine);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarSalaDeCine(int id)
        {
            var salaDeCine = await servicio.EliminarSalaDeCine(id);
            return Ok(salaDeCine);
        }
    }
}
