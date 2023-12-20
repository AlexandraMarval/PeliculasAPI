using PeliculasAPI.Modelos;

namespace PeliculasAPI.Servicios
{
    public interface ISalaDeCineServicio
    {
        Task<List<SalaDeCineModelo>> ObtenerSalaDeCine();
    }
}
