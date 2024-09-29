using System.Linq.Expressions;

namespace Store.Repositry.Specification
{
    public interface ISpecification<T>
    {
        //criteria.where(x=>x.id==id)
        //includes
        Expression<Func<T, bool>> Criteria { get; }

        List<Expression<Func<T, object>>>Includes { get; }
        Expression<Func<T,Object>>OrderBy { get; }
        Expression<Func<T, Object>> OrderByDescending { get; }

        int Take { get; }
        int Skip { get; }
        bool IsPaginated { get; }

    }
}
