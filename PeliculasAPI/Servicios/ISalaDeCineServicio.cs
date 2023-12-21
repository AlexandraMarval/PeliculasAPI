using PeliculasAPI.Modelos;

namespace PeliculasAPI.Servicios
{
    public interface ISalaDeCineServicio
    {
        Task<List<SalaDeCineModelo>> ObtenerSalaDeCine();
        Task<SalaDeCineModelo> ObtenerPorId(int id);
        Task<SalaDeCineModelo> CrearSalaDeCine(CrearSalaDeCineModelo crearSalaDeCineModelo);
        Task<SalaDeCineModelo> ActualizarSalaDeCine(int id, ActualizarSalaDeCineModelo actualizarSalaDeCineModelo);
        Task<SalaDeCineModelo> EliminarSalaDeCine(int id);
    }
}
