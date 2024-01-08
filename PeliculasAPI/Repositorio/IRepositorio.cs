using PeliculasAPI.Modelos;
using System.Linq.Expressions;

namespace PeliculasAPI.Repositorio
{
    public interface IRepositorio<TEntity> where TEntity : class
    {
        Task<List<TEntity>> ObtenerTodo();
        Task<EntidadPaginadaModelo<TEntity>> ObtenerTodoPaginado(
            Expression<Func<TEntity, bool>> expressionFilter,
            Expression<Func<TEntity, string>> expressionOrder, 
            PaginacionModel paginacionModel);
        Task<IQueryable<TEntity>> AsQueryable();
        Task<TEntity> ObtenerPorId(int id);
        Task Crear(TEntity entity);
        Task<IEnumerable<TEntity>> BuscarPorCondicion(Func<TEntity, bool> condicionBusqueda);
        Task Actualizar(TEntity entity);
        Task Elimimar(TEntity entity);
    }
}
