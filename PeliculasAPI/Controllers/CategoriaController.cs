using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Context;
using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;

namespace PeliculasAPI.Controllers
{
    [ApiController]
    [Route("api/controllers")]
    public class CategoriaController : ControllerBase
    {
        private readonly PeliculaDbContext dbContext;

        public CategoriaController(PeliculaDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<List<CategoriaEntity>>> Get()
        {
            return await dbContext.Categorias.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(CategoriaEntity categoriaEntity)
        {
            dbContext.Add(categoriaEntity);
            await dbContext.SaveChangesAsync();
            return Ok();
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
