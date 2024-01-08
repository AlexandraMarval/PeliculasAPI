using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.Modelos;
using PeliculasAPI.Servicios;

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
    }
}
