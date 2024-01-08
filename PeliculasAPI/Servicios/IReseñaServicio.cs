using PeliculasAPI.Modelos;

namespace PeliculasAPI.Servicios
{
    public interface IReseñaServicio
    {
        Task<List<ReseñaModelo>> ObtenerTodo( int peliculaId, PaginacionModel paginacionModel);
    }
}
