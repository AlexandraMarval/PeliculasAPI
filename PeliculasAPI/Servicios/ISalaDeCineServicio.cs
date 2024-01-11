using PeliculasAPI.Modelos;

namespace PeliculasAPI.Servicios
{
    public interface ISalaDeCineServicio
    {
        public Task<List<SalaDeCineModelo>> ObtenerSalaDeCine();
        public Task<SalaDeCineModelo> ObtenerPorId(int id);
        public Task<SalaDeCineModelo> CrearSalaDeCine(CrearSalaDeCineModelo crearSalaDeCineModelo);
        public Task<SalaDeCineModelo> ActualizarSalaDeCine(int id, ActualizarSalaDeCineModelo actualizarSalaDeCineModelo);
        public Task<List<SalaDeCineCercanoModelo>> Cercano(SalaDeCineCercanoFiltroModelo cineCercanoFiltroModelo);
        public Task<SalaDeCineModelo> EliminarSalaDeCine(int id);
    }
}
