using PeliculasAPI.Modelos;

namespace PeliculasAPI.Servicios
{
    public interface IReseñaServicio
    {
        public Task<List<ReseñaModelo>> ObtenerTodasLasReseñas( int peliculaId, PaginacionModel paginacionModel);
        public Task<ReseñaModelo> CrearUnaReseña(int peliculaId, CrearReseñaModelo crearReseñaModelo);
        public Task<ReseñaModelo> ActualizarReseña(int peliculaId, int reseñaId, ActualizarReseñaModelo actualizarReseñaModelo);
        public Task<ReseñaModelo> EliminarReseña(int reseñaId);
    }
}
