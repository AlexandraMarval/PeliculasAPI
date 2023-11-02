using PeliculasAPI.Modelos;

namespace PeliculasAPI.Servicios
{
    public interface IActorServicio
    {
        public Task<List<ActorModel>> ObtenerActores();
        public Task<ActorModel> ObtenerActorPorId(int id);
        public Task<ActorModel> CrearActor(ActorCreacionModel actorCreacionModel);
    }
}
