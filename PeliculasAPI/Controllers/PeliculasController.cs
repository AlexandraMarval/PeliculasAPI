using Microsoft.AspNetCore.JsonPatch;
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

        [HttpGet("{id}", Name = "obtenerPorId")]
        public async Task<ActionResult<PeliculaModelo>> ObtenerId(int id)
        {
            var pelicula = await servicio.ObtenerPeliculaPorId(id);
            if (pelicula == null)
            {
                return NotFound("No se encontro resultado");
            }
            return Ok(pelicula);
        }

        [HttpGet(Name = "obtener listado de peliculas")]
        public async Task<ActionResult> ObtenerListadoDePelicula()
        {
            var pelicula = await servicio.ObtenerPelicula();
            if (pelicula == null)
            {
                return NotFound("No se encontro resultado");
            }
            return Ok(pelicula);
        }

        [HttpPost(Name = "Crear pelicula")]
        public async Task<ActionResult> crear([FromForm] CrearPeliculaModelo crearPeliculaModelo)
        {
            var pelicula = await servicio.Crear(crearPeliculaModelo);
            if (pelicula == null)
            { return NotFound("No se encontro resultado"); }
            return new CreatedAtRouteResult("obtenerPorId", new { id = pelicula.Id }, pelicula);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarPelicula(int id, [FromBody] ActualizarPeliculaModelo actualizarPeliculaModelo)
        {
            var actualizarPelicula = await servicio.ActualizarPelicula(id,actualizarPeliculaModelo);
            if(actualizarPelicula == null)
            {
                return NotFound("No se encontro un resultado para actualizar ese id");
            }
            return Ok(actualizarPelicula); 
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> ActualizarPeliculaPacth(int id, [FromBody] JsonPatchDocument<PeliculaPatchModelo> patchDocument)
        {
            var pelicula = await servicio.ActualizarPeliculaPatchId(id,patchDocument);
            return Ok(pelicula);
        }
    }
}
