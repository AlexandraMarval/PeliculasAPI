using AutoMapper;
using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;

namespace PeliculasAPI.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CategoriaCreateModel, CategoriaEntity>().ReverseMap();
            CreateMap<CategoriaEntity, CategoriaModel>().ReverseMap();
        }
    }
}
