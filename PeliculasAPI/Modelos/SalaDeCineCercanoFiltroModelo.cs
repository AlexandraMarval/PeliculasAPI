using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Modelos
{
    public class SalaDeCineCercanoFiltroModelo
    {
        [Range(-90, 90)]
        public double Latitud { get; set; }
        [Range(-90, 90)]
        public double Longitud { get; set; }
    }
}
