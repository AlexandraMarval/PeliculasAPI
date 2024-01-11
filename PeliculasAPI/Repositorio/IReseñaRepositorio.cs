using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;

namespace PeliculasAPI.Repositorio
{
    public interface IReseñaRepositorio : IRepositorio<ReseñaEntidad>
    {
        Task<EntidadPaginadaModelo<ReseñaEntidad>> BuscarReseñasDePeliculaAsync(int peliculaId, PaginacionModel paginacionModel);
    }
}
