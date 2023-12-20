using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Ayudantes;
using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;
using PeliculasAPI.Repositorio;

namespace PeliculasAPI.Servicios
{
    public class ActorServicio : IActorServicio
    {       
        private readonly IMapper mapper;
        private readonly IRepositorio<ActorEntidad> repositorio;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string contenedor = "actores";

        public ActorServicio(IMapper mapper,IRepositorio<ActorEntidad> repositorio, IAlmacenadorArchivos almacenadorArchivos, IHttpContextAccessor httpContextAccessor) 
        {          
            this.mapper = mapper;
            this.repositorio = repositorio;
            this.almacenadorArchivos = almacenadorArchivos;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<ActorModel>> ObtenerActores(PaginacionModel paginacionModel)
        {
            var queryable = await repositorio.AsQueryable();

            await httpContextAccessor.HttpContext.InsertarParametrosPaginacion(queryable, paginacionModel.cantidadRegistrosPorPagina);

            var actor = await queryable.Paginar(paginacionModel).ToListAsync();
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
                var crearActor = mapper.Map<ActorEntidad>(crearActorModel);

                if (crearActor.Foto != null)
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


        public async Task<ActorPatchModelo> ActualizarActorPatchId(int id, JsonPatchDocument<ActorPatchModelo> pathDocument)
        {
            var entidadDb = await repositorio.ObtenerPorId(id);

            if (pathDocument == null)
            {
                throw new Exception("El jsonPatchDocument es nulo");
            }

            if(entidadDb == null)
            {
                throw new Exception("No se encontró ninguna entidad con el id proporcionado");
            }                  
            var actorPatchModel = mapper.Map<ActorPatchModelo>(entidadDb);
            pathDocument.ApplyTo(actorPatchModel);

            entidadDb = mapper.Map<ActorEntidad>(actorPatchModel);

            await repositorio.Actualizar(entidadDb);
            
            return actorPatchModel;
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
