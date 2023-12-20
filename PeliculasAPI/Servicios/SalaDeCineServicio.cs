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
        public async Task<SalaDeCineModelo> ObtenerPorId(int id)
        {
            var salaDeCine = await repositorio.ObtenerPorId(id);
            var salaDeCineModelo = mapper.Map<SalaDeCineModelo>(salaDeCine);
            return salaDeCineModelo;
        }

        public async Task<SalaDeCineModelo> CrearSalaDeCine(CrearSalaDeCineModelo crearSalaDeCineModelo)
        {
            var salaDeCineDb = await repositorio.BuscarPorCondicion(sala => sala.Nombre == crearSalaDeCineModelo.Nombre);

            if (!salaDeCineDb.Any())
            {
                var crearSalaDeCine = mapper.Map<SalaDeCineEntidad>(crearSalaDeCineModelo);
                await repositorio.Crear(crearSalaDeCine);
                var salaDeCine = mapper.Map<SalaDeCineModelo>(crearSalaDeCine);
                return salaDeCine;
            }
            else 
            {
                throw new Exception("Ya existe un nombre");
            }
        }
    }
}
