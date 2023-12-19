using AutoMapper;
using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;

namespace PeliculasAPI.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CrearCategoriaModelo, CategoriaEntity>().ReverseMap();
            CreateMap<CategoriaEntity, CategoriaModelo>().ReverseMap();
            CreateMap<ActualizarCategoriaModelo, CategoriaEntity>().ReverseMap();
            CreateMap<CrearActorModel, ActorEntity>().ReverseMap();
            CreateMap<ActorEntity, ActorModel>().ReverseMap();           
            CreateMap<ActualizarActorModelo, ActorEntity>().ReverseMap().ForMember(x => x.Foto, options => options.Ignore());
            CreateMap<ActorPatchModelo, ActorEntity>().ReverseMap();
            CreateMap<CrearPeliculaModelo, PeliculaEntidad>()
                .ForMember(pelicula => pelicula.Poster, options => options.Ignore())
                .ForMember(pelicula => pelicula.PeliculaCategorias, options => options.MapFrom(MapPeliculasCategorias))
                .ForMember(pelicula => pelicula.PeliculaActores, options => options.MapFrom(MapPeliculasActores));
            CreateMap<PeliculaEntidad, PeliculaModelo>().ReverseMap();
            CreateMap<ActualizarPeliculaModelo, PeliculaEntidad>().ReverseMap();
            CreateMap<PeliculaPatchModelo, PeliculaEntidad>().ReverseMap();
        }

        private List<PeliculasCategorias> MapPeliculasCategorias(CrearPeliculaModelo crearPeliculaModelo, PeliculaEntidad peliculaEntidad)
        {
            var resultado = new List<PeliculasCategorias>();
            if (crearPeliculaModelo.CategoriasIDs == null)
            {
                return resultado;
            }

            foreach (var categoria in crearPeliculaModelo.CategoriasIDs )
            {
                resultado.Add( new PeliculasCategorias(){ CategoriaId = categoria });
            }
            return resultado;
        }

        private List<PeliculasActores> MapPeliculasActores(CrearPeliculaModelo crearPeliculaModelo, PeliculaEntidad peliculaEntidad)
        {
            var resultado = new List<PeliculasActores>();
            if (crearPeliculaModelo.Actores == null)
            {

            }
            foreach (var actor in crearPeliculaModelo.Actores)
            {
                resultado.Add ( new PeliculasActores() { ActorId  = actor.ActorId, Personaje = actor.Personaje});
            }
            return resultado;
        }
    }
}
