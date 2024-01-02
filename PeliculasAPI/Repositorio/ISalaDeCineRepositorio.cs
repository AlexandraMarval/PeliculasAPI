using NetTopologySuite.Geometries;
using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;
using System.Drawing;
using Point = NetTopologySuite.Geometries.Point;

namespace PeliculasAPI.Repositorio
{
    public interface ISalaDeCineRepositorio : IRepositorio<SalaDeCineEntidad>
    {
        Task<List<SalaDeCineCercanoModelo>> ObtenerTodo(Point point, SalaDeCineCercanoFiltroModelo cineCercanoFiltroModelo);
    }
}
