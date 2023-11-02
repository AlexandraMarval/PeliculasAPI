using AutoMapper;
using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;
using PeliculasAPI.Repositorio;

namespace PeliculasAPI.Servicios
{
    public class ActorServicio : IActorServicio
    {       
        private readonly IMapper mapper;
        private readonly IRepositorio<ActorEntity> repositorio;

        public ActorServicio(IMapper mapper,IRepositorio<ActorEntity> repositorio) 
        {          
            this.mapper = mapper;
            this.repositorio = repositorio;
        }

        public async Task<List<ActorModel>> ObtenerActores()
        {
            var actor = await repositorio.ObtenerTodo();
            var actorModel = mapper.Map<List<ActorModel>>(actor);
            return actorModel;
        }      

        public async Task<ActorModel> ObtenerActorPorId(int id)
        {
            var actor = await repositorio.ObtenerPorId(id);
            var actorModel = mapper.Map<ActorModel>(actor);
            return actorModel;
        }
        public async Task<ActorModel> CrearActor(ActorCreacionModel actorCreacionModel)
        {
            var ActorExiste = await repositorio.BuscarPorCondicion(actor => actor.Nombre == actorCreacionModel.Nombre);

            if (!ActorExiste.Any())
            {
                var actorCreacion = mapper.Map<ActorEntity>(actorCreacionModel);
                await repositorio.Crear(actorCreacion);
                var actorModel = mapper.Map<ActorModel>(actorCreacion);
                return actorModel;
            }
            else 
            {
                throw new Exception("Ya existe un autor con el mismo nombre");
            }
        }

    }
}
