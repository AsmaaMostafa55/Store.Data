using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repositry.Interfaces
{
    public interface IGenericRepositry<TEntity,TKey> where TEntity : Baseentity<TKey>
    {
        Task<TEntity> GetByIdAsync(TKey? id);
        //Task<TEntity> GetByIdAsNoTrackingAsync(TKey? id);
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<IReadOnlyList<TEntity>> GetAllAsNoTrackingAsync();
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

    }
}
