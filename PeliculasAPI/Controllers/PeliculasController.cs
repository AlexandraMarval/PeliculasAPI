using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.Modelos;
using PeliculasAPI.Servicios;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/pelicula")]
    public class PeliculasController : ControllerBase
    {
        private readonly IPeliculaServicio servicio;

        public PeliculasController(IPeliculaServicio servicio)
        {
            this.servicio = servicio;
        }

        [HttpGet]
        public async Task<ActionResult<PeliculaModelo>> ObtenerId(int id)
        {
            var pelicula = servicio.ObtenerPeliculaPorId(id);
            if (pelicula == null)
            {
                return BadRequest("No se encontro resultado");
            }
            return Ok(pelicula);
        }

    }
}
