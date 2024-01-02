using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using PeliculasAPI.Context;
using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;

namespace PeliculasAPI.Repositorio
{
    public class SalaDeCineRepositorio : Repositorio<SalaDeCineEntidad>, ISalaDeCineRepositorio
    {
        public SalaDeCineRepositorio(PeliculaDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<SalaDeCineCercanoModelo>> ObtenerTodo(Point ubicacionUsuario, SalaDeCineCercanoFiltroModelo filtroModelo)
        {
            var salaDeCine = await dbContext.SalasDeCine.OrderBy(sala => sala.Ubicacion.Distance(ubicacionUsuario)).Where(sala => sala.Ubicacion.IsWithinDistance(ubicacionUsuario, filtroModelo.DistanciaEnKms * 1000)).Select(sala => new SalaDeCineCercanoModelo 
                { Id = sala.Id,
                    Nombre = sala.Nombre,
                    Latitud = sala.Ubicacion.Y,
                    Longitud = sala.Ubicacion.X,
                    DistanciaEnMetros = Math.Round(sala.Ubicacion.Distance(ubicacionUsuario)) 
                }).ToListAsync();

            return salaDeCine;
        }
    }
}
