using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.Modelos;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/controllers")]
    public class CategoriaController : ControllerBase
    {
        [HttpGet]
        public IActionResult Post(CategoriaCreateModel categoriaCreateModel)
        {
            var categoriaModel = new CategoriaModel
            {
               Nombre = categoriaCreateModel.Nombre,
            };
           return Ok(categoriaModel);
        }
    }
}
