using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repositry.Interfaces
{
    public interface IUnitOfWork 
    {
        IGenericRepositry<TEntity, TKey> Repositry<TEntity, TKey>() where TEntity : Baseentity<TKey>;
        Task<int> CompleteAsync();

    }
}
