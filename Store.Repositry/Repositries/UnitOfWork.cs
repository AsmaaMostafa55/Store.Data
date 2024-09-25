using Store.Data.Contexts;
using Store.Data.Entities;
using Store.Repositry.Interfaces;
using System.Collections;

namespace Store.Repositry.Repositries
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        private Hashtable _repositries;
        public UnitOfWork(StoreDbContext context)
        {
            _context=context;
        }
        public async Task<int> CompleteAsync()
        => await _context.SaveChangesAsync();   

        public IGenericRepositry<TEntity, TKey> Repositry<TEntity, TKey>() where TEntity : Baseentity<TKey>
        {
           if(_repositries is null)
                _repositries = new Hashtable();
           var entitykey=typeof(TEntity).Name;
            if(_repositries.ContainsKey(entitykey))
            {
                var repositryType=typeof(IGenericRepositry<,>);
                var repositoryInstance=Activator.CreateInstance(repositryType.MakeGenericType(typeof(TEntity),typeof(TKey)),_context);
                _repositries.Add(entitykey, repositoryInstance);

            }
            return (IGenericRepositry<TEntity, TKey>)  _repositries[entitykey];
        }
    }
}
