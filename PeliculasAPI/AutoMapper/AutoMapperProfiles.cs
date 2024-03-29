﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using PeliculasAPI.Entidades;
using PeliculasAPI.Modelos;

namespace PeliculasAPI.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles(GeometryFactory geometryFactory)
        {
            // Mapper de Categoria
            CreateMap<CrearCategoriaModelo, CategoriaEntidad>().ReverseMap();
            CreateMap<CategoriaEntidad, CategoriaModelo>().ReverseMap();
            CreateMap<ActualizarCategoriaModelo, CategoriaEntidad>().ReverseMap();

            // Mapper de Actores
            CreateMap<CrearActorModel, ActorEntidad>().ReverseMap();
            CreateMap<ActorEntidad, ActorModel>().ReverseMap();           
            CreateMap<ActualizarActorModelo, ActorEntidad>().ReverseMap().ForMember(x => x.Foto, options => options.Ignore());
            CreateMap<ActorPatchModelo, ActorEntidad>().ReverseMap();

            // Mappper de Pelicula
            CreateMap<CrearPeliculaModelo, PeliculaEntidad>()
                .ForMember(pelicula => pelicula.Poster, options => options.Ignore())
                .ForMember(pelicula => pelicula.PeliculaCategorias, options => options.MapFrom(MapPeliculasCategorias))
                .ForMember(pelicula => pelicula.PeliculaActores, options => options.MapFrom(MapPeliculasActores));
            CreateMap<PeliculaEntidad, PeliculaModelo>().ReverseMap();
            CreateMap<ActualizarPeliculaModelo, PeliculaEntidad>().ReverseMap();
            CreateMap<PeliculaPatchModelo, PeliculaEntidad>().ReverseMap();

            // Mapper de Sala de Cine
            CreateMap<CrearSalaDeCineModelo, SalaDeCineEntidad>()
                  .ForMember(x => x.Ubicacion, x => x.MapFrom(y => geometryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));

            CreateMap<SalaDeCineEntidad, SalaDeCineModelo>().ForMember(x => x.Latitud, x => x.MapFrom(y => y.Ubicacion.Y))
                .ForMember(x => x.Longitud, x => x.MapFrom(y => y.Ubicacion.X));

            CreateMap<SalaDeCineModelo, SalaDeCineEntidad>()
                .ForMember(x => x.Ubicacion, x => x.MapFrom(y => geometryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));
            
            CreateMap<ActualizarSalaDeCineModelo, SalaDeCineEntidad>().ReverseMap();

            // Mapper de Reseña

            CreateMap<ReseñaEntidad, ReseñaModelo>().ForMember(x => x.NombreUsuario, x => x.MapFrom(y => y.Usuario.UserName));

            CreateMap<CrearReseñaModelo, ReseñaEntidad>();
            CreateMap<ReseñaModelo, ReseñaEntidad>().ReverseMap();

            CreateMap<ActualizarReseñaModelo, ReseñaEntidad>().ReverseMap();
            CreateMap<IdentityUser, UsuarioModelo>();
        }

        private List<PeliculasCategoriasEntidad> MapPeliculasCategorias(CrearPeliculaModelo crearPeliculaModelo, PeliculaEntidad peliculaEntidad)
        {
            var resultado = new List<PeliculasCategoriasEntidad>();
            if (crearPeliculaModelo.CategoriasIDs == null)
            {
                return resultado;
            }

            foreach (var categoria in crearPeliculaModelo.CategoriasIDs )
            {
                resultado.Add( new PeliculasCategoriasEntidad(){ CategoriaId = categoria });
            }
            return resultado;
        }

        private List<PeliculasActoresEntidad> MapPeliculasActores(CrearPeliculaModelo crearPeliculaModelo, PeliculaEntidad peliculaEntidad)
        {
            var resultado = new List<PeliculasActoresEntidad>();
            if (crearPeliculaModelo.Actores == null)
            {

            }
            foreach (var actor in crearPeliculaModelo.Actores)
            {
                resultado.Add ( new PeliculasActoresEntidad() { ActorId  = actor.ActorId, Personaje = actor.Personaje});
            }
            return resultado;
        }
    }
}
