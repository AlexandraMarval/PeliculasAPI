using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Modelos
{
    public class CrearSalaDeCineModelo
    {
        [Required]
        [StringLength(120)]
        public string Nombre {  get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }
}
