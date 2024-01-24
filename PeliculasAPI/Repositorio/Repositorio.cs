using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Context;
using PeliculasAPI.Modelos;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace PeliculasAPI.Repositorio
{
    public class Repositorio<TEntity> : IRepositorio<TEntity> where TEntity : class
    {
        protected readonly PeliculaDbContext dbContext;
        private readonly DbSet<TEntity> dbSet;

        public Repositorio(PeliculaDbContext dbContext) 
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }     
        public Task<IEnumerable<TEntity>> BuscarPorCondicion(Func<TEntity, bool> expression)
        {
            return Task.FromResult(dbSet.Where(expression));
        }
        public async Task<List<TEntity>> ObtenerTodo()
        {            
            var entities = await dbSet.ToListAsync();
            return entities;
        }
        public async Task<TEntity> ObtenerPorId(int id)
        {
            var entity = await dbSet.FindAsync(id);
            return entity;
        }       

        public async Task Crear(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task Actualizar(TEntity entity)
        {
            dbSet.Update(entity);           
            await dbContext.SaveChangesAsync();          
        }

        public async Task Elimimar(TEntity entity)
        {
            dbSet.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public Task<IQueryable<TEntity>> AsQueryable()
        {
            return Task.FromResult(dbSet.AsQueryable());
        }

        public async Task<EntidadPaginadaModelo<TEntity>> ObtenerTodoPaginado(Expression<Func<TEntity, bool>> expressionFilter, Expression<Func<TEntity, string>> expressionOrder, PaginacionModel paginacionModel)
        {
            var query = dbSet.Where(expressionFilter);
            double cantidadTotalRegistros = await query.CountAsync();
            var skip = (paginacionModel.Pagina - 1) * paginacionModel.CantidadRegistrosPorPagina;

            var entities = await query
                .OrderBy(expressionOrder)
                .Skip(skip)
                .Take(paginacionModel.CantidadRegistrosPorPagina).ToListAsync();

            var entidadesPaginadas = new EntidadPaginadaModelo<TEntity>
            {
                Entidades = entities,
                CantidadRegistros = cantidadTotalRegistros,
                PaginaActual = paginacionModel.Pagina,
                RegistrosPorPagina = paginacionModel.CantidadRegistrosPorPagina,
                CantidadPaginas = Math.Ceiling(cantidadTotalRegistros / paginacionModel.CantidadRegistrosPorPagina)
            };

            return entidadesPaginadas;
        }
    }
}
