using Microsoft.EntityFrameworkCore;
using Store.Data.Contexts;
using Store.Data.Entities;
using Store.Repositry.Interfaces;

namespace Store.Repositry.Repositries
{
    public class GenericRepositry<TEntity, TKey> : IGenericRepositry<TEntity, TKey> where TEntity : Baseentity<TKey>
    {
        private readonly StoreDbContext _context;
        public GenericRepositry(StoreDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TEntity entity)
       =>await _context.Set<TEntity>().AddAsync(entity);
        public void Delete(TEntity entity)
         =>  _context.Set<TEntity>().Remove(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllAsNoTrackingAsync()
          => await _context.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
         => await _context.Set<TEntity>().ToListAsync();

        //public async Task<TEntity> GetByIdAsNoTrackingAsync(TKey? id)
        //      => await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x=>x.Id==id);

        public async Task<TEntity> GetByIdAsync(TKey? id)
        => await _context.Set<TEntity>().FindAsync(id);

        public void Update(TEntity entity)
        =>  _context.Set<TEntity>().Update(entity);
    }
}
