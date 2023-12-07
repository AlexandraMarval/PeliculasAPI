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
        private readonly IAlmacenadorArchivos almacenadorArchivos;

        public PeliculasController(IPeliculaServicio servicio, IAlmacenadorArchivos almacenadorArchivos)
        {
            this.servicio = servicio;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        public async Task<ActionResult<PeliculaModelo>> ObtenerId(int id)
        {
            var pelicula = await servicio.ObtenerPeliculaPorId(id);
            if (pelicula == null)
            {
                return BadRequest("No se encontro resultado");
            }
            return Ok(pelicula);
        }

        [HttpGet]
        public async Task<ActionResult> ObtenerListadoDePelicula()
        {
            var pelicula = await servicio.ObtenerPelicula();
            if (pelicula == null)
            {
                return BadRequest("No se encontro resultado");
            }
            return Ok(pelicula);

        }

    }
}
