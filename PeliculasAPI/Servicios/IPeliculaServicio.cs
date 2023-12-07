using PeliculasAPI.Modelos;

namespace PeliculasAPI.Servicios
{
    public interface IPeliculaServicio
    {
        public Task<PeliculaModelo> ObtenerPeliculaPorId(int id);
    }
}
