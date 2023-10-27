using PeliculasAPI.Modelos;

namespace PeliculasAPI.Servicios
{
    public interface IActorServicio
    {
        public Task<ActorModelo> ObtenerActorPorId();
    }
}
