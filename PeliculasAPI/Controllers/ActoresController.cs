using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.Modelos;
using PeliculasAPI.Servicios;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/actores")]
    public class ActoresController : ControllerBase
    {
        private readonly IActorServicio servicio;

        public ActoresController(IActorServicio servicio)
        {
            this.servicio = servicio;
        }

        [HttpGet]
        public async Task<ActionResult> ObtenerListadoDeActor()
        {
            var actor = await servicio.ObtenerActores();
            if (actor == null)
            {
                return NotFound("No se encontro resultado");
            }
            return Ok(actor);
        }

        [HttpGet("int:id")]
        public async Task<ActionResult<ActorModel>> ObtenerPorId(int id)
        {
            var actor = await servicio.ObtenerActorPorId(id);
            if (actor == null)
            {
                return NotFound("No se encontro ningun resultado de su busqueda");
            }
            return Ok(actor);
        }
    }

}
