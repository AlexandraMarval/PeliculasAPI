using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PeliculasAPI.Repositorio;

namespace PeliculasAPI.Ayudantes
{
    public class PeliculaExisteAttributo : Attribute, IAsyncResourceFilter
    {
        private readonly IPeliculaRepositorio peliculaRepositorio;

        public PeliculaExisteAttributo(IPeliculaRepositorio peliculaRepositorio)
        {
            this.peliculaRepositorio = peliculaRepositorio;
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var peliculaIdObjeto = context.HttpContext.Request.RouteValues["peliculaId"];

            if (peliculaIdObjeto == null)
            {
                return;
            }

            var peliculaId = int.Parse(peliculaIdObjeto.ToString());

            var existePelicula = await peliculaRepositorio.BuscarPorId( peliculaId);

            if (existePelicula is null)
            {
                context.Result = new NotFoundResult();
            }
            else
            {
                await next();
            }
        }
    }
}
