using AutoMapper;
using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;

namespace PeliculasAPI.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CrearCategoriaModelo, CategoriaEntidad>().ReverseMap();
            CreateMap<CategoriaEntidad, CategoriaModelo>().ReverseMap();
            CreateMap<ActualizarCategoriaModelo, CategoriaEntidad>().ReverseMap();
            CreateMap<CrearActorModel, ActorEntidad>().ReverseMap();
            CreateMap<ActorEntidad, ActorModel>().ReverseMap();           
            CreateMap<ActualizarActorModelo, ActorEntidad>().ReverseMap().ForMember(x => x.Foto, options => options.Ignore());
            CreateMap<ActorPatchModelo, ActorEntidad>().ReverseMap();
            CreateMap<CrearPeliculaModelo, PeliculaEntidad>()
                .ForMember(pelicula => pelicula.Poster, options => options.Ignore())
                .ForMember(pelicula => pelicula.PeliculaCategorias, options => options.MapFrom(MapPeliculasCategorias))
                .ForMember(pelicula => pelicula.PeliculaActores, options => options.MapFrom(MapPeliculasActores));
            CreateMap<PeliculaEntidad, PeliculaModelo>().ReverseMap();
            CreateMap<ActualizarPeliculaModelo, PeliculaEntidad>().ReverseMap();
            CreateMap<PeliculaPatchModelo, PeliculaEntidad>().ReverseMap();
        }

        private List<PeliculasCategoriasEntidad> MapPeliculasCategorias(CrearPeliculaModelo crearPeliculaModelo, PeliculaEntidad peliculaEntidad)
        {
            var resultado = new List<PeliculasCategoriasEntidad>();
            if (crearPeliculaModelo.CategoriasIDs == null)
            {
                return resultado;
            }

            foreach (var categoria in crearPeliculaModelo.CategoriasIDs )
            {
                resultado.Add( new PeliculasCategoriasEntidad(){ CategoriaId = categoria });
            }
            return resultado;
        }

        private List<PeliculasActoresEntidad> MapPeliculasActores(CrearPeliculaModelo crearPeliculaModelo, PeliculaEntidad peliculaEntidad)
        {
            var resultado = new List<PeliculasActoresEntidad>();
            if (crearPeliculaModelo.Actores == null)
            {

            }
            foreach (var actor in crearPeliculaModelo.Actores)
            {
                resultado.Add ( new PeliculasActoresEntidad() { ActorId  = actor.ActorId, Personaje = actor.Personaje});
            }
            return resultado;
        }
    }
}
