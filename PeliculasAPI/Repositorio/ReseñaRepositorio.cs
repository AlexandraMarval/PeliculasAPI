using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Context;
using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PeliculasAPI.Repositorio
{
    public class ReseñaRepositorio : Repositorio<ReseñaEntidad>, IReseñaRepositorio
    {
        public ReseñaRepositorio(PeliculaDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<EntidadPaginadaModelo<ReseñaEntidad>> BuscarReseñasDePeliculaAsync(int peliculaId, PaginacionModel paginacionModel)
        {
            var query = dbContext.Reseñas.Include(x => x.Usuario)
               .Where(x => x.PeliculaId == peliculaId);
            double cantidadTotalRegistros = await query.CountAsync();
            var skip = paginacionModel.Pagina - 1 * paginacionModel.CantidadRegistrosPorPagina;

            var entities = await query
            .Skip(skip).Take(paginacionModel.cantidadRegistrosPorPagina).ToListAsync();

            var entidadesPaginadas = new EntidadPaginadaModelo<ReseñaEntidad>
            {
                Entidades = entities,
                CantidadRegistros = cantidadTotalRegistros,
                PaginaActual = paginacionModel.Pagina,
                RegistrosPorPagina = paginacionModel.CantidadRegistrosPorPagina,
                CantidadPaginas = Math.Ceiling(cantidadTotalRegistros / paginacionModel.CantidadRegistrosPorPagina)
            };

            return entidadesPaginadas;

        }
    }
}
