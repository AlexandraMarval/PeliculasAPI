﻿using System.ComponentModel.DataAnnotations;

namespace PeliculasAPI.Modelos
{
    public class ActorModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
    }
}
