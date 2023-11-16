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
        }
    }
}
