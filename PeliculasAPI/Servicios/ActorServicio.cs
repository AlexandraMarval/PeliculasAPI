using AutoMapper;
using PeliculasAPI.Context;
using PeliculasAPI.Modelos;

namespace PeliculasAPI.Servicios
{
    public class ActorServicio : IActorServicio
    {
        private readonly PeliculaDbContext dbContext;
        private readonly IMapper mapper;

        public ActorServicio(PeliculaDbContext dbContext, IMapper mapper) 
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public Task<ActorModelo> ObtenerActorPorId()
        {
            throw new NotImplementedException();
        }
    }
}
