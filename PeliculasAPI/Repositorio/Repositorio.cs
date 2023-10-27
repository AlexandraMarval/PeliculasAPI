using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PeliculasAPI.Context;
using PeliculasAPI.Entidades;

namespace PeliculasAPI.Repositorio
{
    public class Repositorio<TEntity> : IRepositorio<TEntity> where TEntity : class
    {
        private readonly PeliculaDbContext dbContext;
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
    }
}
