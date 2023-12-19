using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Context;
using PeliculasAPI.Entidades;
using System.Collections.Generic;

namespace PeliculasAPI.Repositorio
{
    public class PeliculaRepositorio : Repositorio<PeliculaEntidad>, IPeliculaRepositorio
    {
        public PeliculaRepositorio(PeliculaDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PeliculaEntidad> BuscarPorId(int id)
        {
            var pelicula = await dbContext.peliculas
                 .Include(x => x.PeliculaActores)
                 .Include(x => x.PeliculaCategorias)
                 .FirstOrDefaultAsync(x => x.Id == id);
            return pelicula;         
        }
    }
}
