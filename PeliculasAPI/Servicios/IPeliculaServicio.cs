using PeliculasAPI.Modelos;

namespace PeliculasAPI.Servicios
{
    public interface IPeliculaServicio
    {
        public Task<List<PeliculaModelo>> ObtenerPelicula();
        public Task<PeliculaModelo> ObtenerPeliculaPorId(int id);
        public Task<PeliculaModelo> Crear(CrearPeliculaModelo crearPeliculaModelo);      
    }
}
