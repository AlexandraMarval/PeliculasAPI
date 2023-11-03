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
        public async Task<ActorModel> CrearActor(CrearActorModel CrearActorModel)
        {
            var ActorExiste = await repositorio.BuscarPorCondicion(actor => actor.Nombre == CrearActorModel.Nombre);

            if (!ActorExiste.Any())
            {
                var crearActor = mapper.Map<ActorEntity>(CrearActorModel);
                await repositorio.Crear(crearActor);
                var actorModel = mapper.Map<ActorModel>(crearActor);
                return actorModel;
            }
            else 
            {
                throw new Exception("Ya existe un autor con el mismo nombre");
            }
        }

        public async Task<ActorModel> ActualizarActor(int id, ActualizarActorModelo actualizarActorModelo)
        {
            var actualizarActor = await repositorio.ObtenerPorId(id);

            if (actualizarActor != null)
            {
                actualizarActor.Nombre = actualizarActorModelo.Nombre;
                await repositorio.Actualizar(actualizarActor);
                var categoriaModelRespuesta = mapper.Map<ActorModel>(actualizarActor);
                return categoriaModelRespuesta;
            }
            else
            {
                throw new Exception("No existe un actor con ese id: ");
            }
        }

        public async Task<ActorModel> Eliminar(int id)
        {
            var eliminarActor = await repositorio.ObtenerPorId(id);
            if (eliminarActor != null)
            {
                await repositorio.Elimimar(eliminarActor);
                var actorModel = mapper.Map<ActorModel>(eliminarActor);
                return actorModel;
            }
            else
            {
                throw new Exception("No existe un actor por el mismo id");
            }
        }
    }
}
