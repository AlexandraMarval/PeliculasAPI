using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.Ayudantes;
using PeliculasAPI.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Modelos
{
    public class CrearPeliculaModelo
    {       
        public string Titulo { get; set; }
        public bool EnCine { get; set; }
        public DateTime FechaEstreno { get; set; }
        public IFormFile Poster { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder))]
        public List<int> CategoriasIDs { get; set; }
    }
}