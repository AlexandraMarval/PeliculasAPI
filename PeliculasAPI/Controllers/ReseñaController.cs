using Microsoft.AspNetCore.Mvc;

namespace PeliculasAPI.Controllers
{

    [ApiController]
    [Route("api/pelicula/{pelicula:int}/reseña")]
    public class ReseñaController : ControllerBase
    {
        public ReseñaController()
        {
            
        }

    }
}
