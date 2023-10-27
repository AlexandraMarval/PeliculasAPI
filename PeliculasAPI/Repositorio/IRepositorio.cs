using PeliculasAPI.Entidades;

namespace PeliculasAPI.Repositorio
{
    public interface IRepositorio<TEntity> where TEntity : class
    {
        Task<List<TEntity>>ObtenerTodo();
        Task<TEntity> ObtenerPorId(int id);
        Task Crear(TEntity entity);
        Task<IEnumerable<TEntity>> BuscarPorCondicion(Func<TEntity, bool> condicionBusqueda);
        Task Actualizar(TEntity entity);
        Task Elimimar(TEntity entity);
    }
}
