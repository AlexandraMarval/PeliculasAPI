using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;

namespace PeliculasAPI.Repositorio
{
    public interface IReseñaRepositorio : IRepositorio<ReseñaEntidad>
    {
        Task<ReseñaEntidad> BuscarUsuarioYRelacionesPorIdAsync(int id, PaginacionModel paginacionModel);
    }
}
