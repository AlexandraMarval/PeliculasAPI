using PeliculasAPI.Modelos;

namespace PeliculasAPI.Ayudantes
{
    public static class QueryableExtensiones
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, PaginacionModel paginacionModel) where T : class
        {
            return queryable
                .Skip((paginacionModel.Pagina - 1) * paginacionModel.CantidadRegistrosPorPagina)
                .Take(paginacionModel.CantidadRegistrosPorPagina);
        }
    }
}
