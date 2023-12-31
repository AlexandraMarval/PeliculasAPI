﻿using Microsoft.AspNetCore.JsonPatch;
using PeliculasAPI.Modelos;


namespace PeliculasAPI.Servicios
{
    public interface IActorServicio
    {
        public Task<List<ActorModel>> ObtenerActores(PaginacionModel paginacionModel);
        public Task<ActorModel> ObtenerActorPorId(int id);
        public Task<ActorPatchModelo> ActualizarActorPatchId(int id, JsonPatchDocument<ActorPatchModelo> patchDocument);
        public Task<ActorModel> CrearActor(CrearActorModel crearActorModel);
        public Task<ActorModel> ActualizarActor(int id, ActualizarActorModelo actualizarActorModelo);
        public Task<ActorModel> Eliminar(int id);
    }
}
