using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.Modelos;
using PeliculasAPI.Servicios;
using System.Security.Claims;

namespace PeliculasAPI.Controllers
{

    [ApiController]
    [Route("api/pelicula/{pelicula:int}/reseña")]
    public class ReseñaController : ControllerBase
    {
        private readonly IReseñaServicio servicio;

        public ReseñaController(IReseñaServicio servicio)
        {
            this.servicio = servicio;
        }

        [HttpGet(Name = "ObtenerTodasLasReseñas")]
        public async Task<ActionResult<List<ReseñaModelo>>> ObtenerTodo(int peliculaId,[FromQuery] PaginacionModel paginacionModel)
        {
            var reseñaModelo = await servicio.ObtenerTodasLasReseñas(peliculaId, paginacionModel);
            if (reseñaModelo == null)
            {
                NotFound();
            }
            return reseñaModelo;
        }

        [HttpPost(Name = "CrearUnaReseña")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> CrearUnaReseña(int peliculaId, [FromBody] CrearReseñaModelo crearReseñaModelo)
        {
            var existeUnaReseña = await servicio.CrearUnaReseña(peliculaId, crearReseñaModelo);

            return new CreatedAtRouteResult("ObtenerTodasLasReseñas", new { id = peliculaId }, existeUnaReseña);
        }
    }
}
