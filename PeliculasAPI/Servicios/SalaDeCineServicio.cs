using AutoMapper;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;
using PeliculasAPI.Repositorio;

namespace PeliculasAPI.Servicios
{
    public class SalaDeCineServicio : ISalaDeCineServicio
    {
        private readonly IMapper mapper;
        private readonly ISalaDeCineRepositorio repositorio;
        private readonly GeometryFactory geometryFactory;

        public SalaDeCineServicio(IMapper mapper, ISalaDeCineRepositorio repositorio, GeometryFactory geometryFactory)
        {
            this.mapper = mapper;
            this.repositorio = repositorio;
            this.geometryFactory = geometryFactory;
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

        public async Task<SalaDeCineModelo> ActualizarSalaDeCine(int id, ActualizarSalaDeCineModelo actualizarSalaDeCineModelo)
        {
            var salaDeCine = await repositorio.ObtenerPorId(id);
            if (salaDeCine != null)
            {
                salaDeCine.Nombre = actualizarSalaDeCineModelo.Nombre;
                await repositorio.Actualizar(salaDeCine);
                var salaDeCineModelo = mapper.Map<SalaDeCineModelo>(salaDeCine);
                return salaDeCineModelo;
            }
            else
            {
                throw new Exception("No existe una sala de cine con ese id");
            }
        }

        public async Task<SalaDeCineModelo> EliminarSalaDeCine(int id)
        {
            var salaDeCineDB = await repositorio.ObtenerPorId(id);
            if(salaDeCineDB != null)
            {
                await repositorio.Elimimar(salaDeCineDB);
                var salaDeCine = mapper.Map<SalaDeCineModelo>(salaDeCineDB);
                return salaDeCine;
            }
            else
            {
                throw new Exception("No existe ese id");
            }
        }

        public async Task<List<SalaDeCineCercanoModelo>> Cercano(SalaDeCineCercanoFiltroModelo cineCercanoFiltroModelo)
        {
            var ubicacionUsuario = geometryFactory.CreatePoint(new Coordinate(cineCercanoFiltroModelo.Longitud, cineCercanoFiltroModelo.Latitud));

            var salaDeCine = await repositorio.ObtenerTodo(ubicacionUsuario, cineCercanoFiltroModelo);
            return salaDeCine;
        }
    }
}
