using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Context;
using PeliculasAPI.Entidades;

namespace PeliculasAPI.Repositorio
{
    public class PeliculaRepositorio : Repositorio<PeliculaEntidad>, IPeliculaRepositorio
    {
        public PeliculaRepositorio(PeliculaDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PeliculaEntidad> BuscarPeliculaYRelacionesPorIdAsync(int id)
        {
            var pelicula = await dbContext.Peliculas
                 .Include(x => x.PeliculaActores)
                 .Include(x => x.PeliculaCategorias)
                 .FirstOrDefaultAsync(x => x.Id == id);
            return pelicula;         
        }
    }
}
