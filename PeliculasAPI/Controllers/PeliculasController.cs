using Microsoft.AspNetCore.Mvc;
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

    }
}
