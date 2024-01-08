using PeliculasAPI.Entidades;

namespace PeliculasAPI.Repositorio
{
    public interface IPeliculaRepositorio : IRepositorio<PeliculaEntidad>
    {
        Task<PeliculaEntidad> BuscarPeliculaYRelacionesPorIdAsync(int id);
    }
}
