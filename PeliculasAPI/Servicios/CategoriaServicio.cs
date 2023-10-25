using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Context;
using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;

namespace PeliculasAPI.Servicios
{
    public class CategoriaServicio : ICategoriaServicio
    {
        private readonly PeliculaDbContext dbContext;
        private readonly IMapper mapper;

        public CategoriaServicio(PeliculaDbContext dbContext, IMapper mapper) 
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<CategoriaModel> ObtenerCategoria()
        {
            var categoria = dbContext.Categorias.ToListAsync();
            var categoriaModel = mapper.Map<CategoriaModel>(categoria);
            return categoriaModel;
        }
    }
}
