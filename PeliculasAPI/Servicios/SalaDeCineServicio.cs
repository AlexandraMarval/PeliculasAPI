using AutoMapper;
using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;
using PeliculasAPI.Repositorio;

namespace PeliculasAPI.Servicios
{
    public class SalaDeCineServicio : ISalaDeCineServicio
    {
        private readonly IMapper mapper;
        private readonly IRepositorio<SalaDeCineEntidad> repositorio;

        public SalaDeCineServicio(IMapper mapper, IRepositorio<SalaDeCineEntidad> repositorio)
        {
            this.mapper = mapper;
            this.repositorio = repositorio;
        }
        public async Task<List<SalaDeCineModelo>> ObtenerSalaDeCine()
        {
            var salaDeCine = await repositorio.ObtenerTodo();
            var salaDeCineModelo = mapper.Map
                <List<SalaDeCineModelo>>(salaDeCine);
            return salaDeCineModelo;
        }
    }
}
