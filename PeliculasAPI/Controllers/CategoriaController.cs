using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Context;
using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;
using PeliculasAPI.Servicios;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/categoria")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaServicio servicio;

        public CategoriaController(ICategoriaServicio servicio ) 
        {
            this.servicio = servicio;
        }

        [HttpGet("id:int", Name = "obtenerCategoriaId")]
        public async Task<ActionResult<CategoriaModelo>> ObtenerId(int id)
        {
            var resultadoId = await servicio.ObtenerCategoriaPorId(id);
            if (resultadoId == null)
            {
                return NotFound("No se encontro ningún resultado");
            }
            return Ok(resultadoId);
        }

        [HttpGet(Name = "ObtenerListadoDeCategoria")]
        public async Task<ActionResult> Get()
        {
            var respuestaCategoria = await servicio.ObtenerCategorias();
            if (respuestaCategoria == null)
            {
                return NotFound("No se encontro ningún resultado");
            }
            return Ok(respuestaCategoria);
        }

        [HttpPost]
        public async Task<ActionResult> CrearUnaCategoria([FromBody]CrearCategoriaModelo categoriaCreacionModel)
        {
           var categoria = await servicio.CrearCategoria(categoriaCreacionModel);
            return CreatedAtRoute("obtenerCategoriaId", new { categoria.Id });
        }

        [HttpPut]
        public async Task<ActionResult> ActualizarCategoria(int id, [FromBody]ActualizarCategoriaModelo categoriaActualizarModelo)
        {
            var categoria = await servicio.ActualizarCategoria(id, categoriaActualizarModelo);           
            return Ok(categoria);
        }

        [HttpDelete("id:int")]
        public async Task<ActionResult> EliminarCategoria(int id)
        {
            var categoria = await servicio.EliminarCategoria(id);
            return Ok(categoria);
        }

        //[HttpPost]
        //public IActionResult Post(CategoriaCreateModel categoriaCreateModel)
        //{
        //    var categoriaModel = new CategoriaModel
        //    {
        //       Nombre = categoriaCreateModel.Nombre,
        //    };
        //   return Ok(categoriaModel);
        //}
    }
}
