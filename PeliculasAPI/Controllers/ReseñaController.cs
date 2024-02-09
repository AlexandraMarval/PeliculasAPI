using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.Ayudantes;
using PeliculasAPI.Modelos;
using PeliculasAPI.Servicios;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/pelicula/{peliculaId:int}/[controller]")]
    [ServiceFilter(typeof(PeliculaExisteAttributo))]
    public class ReseñaController : ControllerBase
    {
        private readonly IReseñaServicio servicio;

        public ReseñaController(IReseñaServicio servicio)
        {
            this.servicio = servicio;
        }

        [HttpGet(Name = "ObtenerTodasLasReseñas")]
        public async Task<ActionResult<List<ReseñaModelo>>> ObtenerTodo(int peliculaId, [FromQuery] PaginacionModel paginacionModel)
        {
            var reseñaModelo = await servicio.ObtenerReseñas(peliculaId, paginacionModel);
            if (reseñaModelo == null)
            {
                return NoContent();
            }
            return Ok(reseñaModelo);
        }

        [HttpPost(Name = "CrearUnaReseña")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> CrearUnaReseña(int peliculaId, [FromBody] CrearReseñaModelo crearReseñaModelo)
        {
            var existeUnaReseña = await servicio.CrearUnaReseña(peliculaId, crearReseñaModelo);

            return new CreatedAtRouteResult("ObtenerTodasLasReseñas", new { id = peliculaId }, existeUnaReseña);
        }

        [HttpPut("{reseñaId:int}", Name = "ActualizarReseñas")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> ActualizarReseña(int peliculaId, int reseñaId, [FromBody]ActualizarReseñaModelo actualizarReseñaModelo)
        {
            try
            {
                var actualizarReseña = await servicio.ActualizarReseña(peliculaId, reseñaId, actualizarReseñaModelo);
                return Ok (actualizarReseña);
            }
            catch (Exception)
            {

                return Forbid();
            }
        }

        [HttpDelete("{reseñaId:int}", Name = "EliminarReseñas")]
        public async Task<ActionResult> Eliminar(int reseñaId)
        {
            try
            {
                var eliminarReseña = await servicio.EliminarReseña(reseñaId);
                return Ok(eliminarReseña);
            }
            catch (Exception)
            {
                return Forbid();
            }
        }
    }
}
