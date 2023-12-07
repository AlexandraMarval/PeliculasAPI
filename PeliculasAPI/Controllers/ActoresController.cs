using Microsoft.AspNetCore.JsonPatch;
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
        private readonly IAlmacenadorArchivos almacenadorArchivos;

        public ActoresController(IActorServicio servicio)
        {
            this.servicio = servicio;
           
        }

        [HttpGet]
        public async Task<ActionResult> ObtenerListadoDeActor([FromQuery] PaginacionModel paginacion)
        {
            var actor = await servicio.ObtenerActores(paginacion);
            if (actor == null)
            {
                return NotFound("No se encontro resultado");
            }
            return Ok(actor);
        }

        [HttpGet("int:id", Name = "ObtenerActorPorId")]
        public async Task<ActionResult<ActorModel>> ObtenerPorId(int id)
        {
            var actor = await servicio.ObtenerActorPorId(id);
            if (actor == null)
            {
                return NotFound("No se encontro ningun resultado de su busqueda");
            }
            return Ok(actor);
        }

        [HttpPost]
        public async Task<ActionResult> CrearUnActor([FromForm]CrearActorModel crearActorModel)
        {
            var crearActor = await servicio.CrearActor(crearActorModel);
            if (crearActor == null)
            {
                return NotFound("Ya existe un actor con ese nombre");
            }
            return new CreatedAtRouteResult("ObtenerActorPorId", new { id = crearActor.Id}, crearActor);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarActor(int id, [FromForm]ActualizarActorModelo actualizarActorModelo)
        {
            var actualizarActor = await servicio.ActualizarActor(id, actualizarActorModelo);
            return Ok(actualizarActor);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<ActorPatchModelo> patchDocument)
        {
            var actorModel = await servicio.ObtenerActorPatchId(id, patchDocument);            
            return Ok(actorModel);
        }

        [HttpDelete]
        public async Task<ActionResult> EliminarActor(int id)
        {
            var Actor = await servicio.Eliminar(id);
            return Ok(Actor);
        }
    }
}
