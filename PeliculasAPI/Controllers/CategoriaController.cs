using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Context;
using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;
using PeliculasAPI.Servicios;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/controllers")]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaServicio service;

        public CategoriaController(CategoriaServicio service) 
        {
            this.service = service;
        }
        [HttpGet]
        public async Task<ActionResult<List<CategoriaModel>>> Get()
        {
            var respuestaCategoria = await service.ObtenerCategoria();
            if (respuestaCategoria == null)
            {
                return NotFound("No se encontro ningun resultado en su busqueda");
            }
            return Ok(respuestaCategoria);
        }

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
