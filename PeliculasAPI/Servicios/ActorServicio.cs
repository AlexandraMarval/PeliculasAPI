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
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "actores";

        public ActorServicio(IMapper mapper,IRepositorio<ActorEntity> repositorio, IAlmacenadorArchivos almacenadorArchivos) 
        {          
            this.mapper = mapper;
            this.repositorio = repositorio;
            this.almacenadorArchivos = almacenadorArchivos;
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
        public async Task<ActorModel> CrearActor(CrearActorModel crearActorModel)
        {
            var actorExiste = await repositorio.BuscarPorCondicion(actor => actor.Nombre == crearActorModel.Nombre);

            if (!actorExiste.Any())
            {
                var crearActor = mapper.Map<ActorEntity>(crearActorModel);

                if (crearActorModel.Foto != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await crearActorModel.Foto.CopyToAsync(memoryStream);
                        var contenido = memoryStream.ToArray();
                        var extension = Path.GetExtension(crearActorModel.Foto.FileName);
                        crearActor.Foto = await almacenadorArchivos.GuardarArchivo(contenido,extension,contenedor, crearActorModel.Foto.ContentType);
                    }
                }
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

                actualizarActor = mapper.Map<ActorEntity>(actualizarActor);

                if (actualizarActorModelo.Foto != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await actualizarActorModelo.Foto.CopyToAsync(memoryStream);
                        var contenido = memoryStream.ToArray();
                        var extension = Path.GetExtension(actualizarActorModelo.Foto.FileName);
                        actualizarActor.Foto = await almacenadorArchivos.EditarArchivo(contenido, extension, contenedor, actualizarActor.Foto, actualizarActorModelo.Foto.ContentType);
                    }
                }
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
