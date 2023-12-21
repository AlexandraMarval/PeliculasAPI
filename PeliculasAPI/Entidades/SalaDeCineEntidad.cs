using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Entidades
{
    public class SalaDeCineEntidad
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Nombre { get; set;}
        public Point Ubicacion { get; set; }
        public List<PeliculasSalasDeCineEntidad> PeliculasSalasDeCine { get; set; }
    }
}
