using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repositry.Specification
{
    public class Basespecification<T> : ISpecification<T>
    {
        public Basespecification(Expression<Func<T, bool>> Criteria)
        {
            Criteria = Criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; }=new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }


        public int Skip{ get; private set; }

        public bool IsPaginated { get; private set; }


        //public Expression<Func<T, bool>> Predicate => throw new NotImplementedException();
        protected void AddInclude(Expression<Func<T, object>> IncludeExpression)
            => Includes.Add(IncludeExpression);
        protected void AddOrderBy(Expression<Func<T,Object>> orderByExpression)
            =>OrderBy=orderByExpression;
        protected void AddOrderByDescending(Expression<Func<T, Object>> orderByDescendingExpression)
           => OrderByDescending = orderByDescendingExpression;
        protected void ApplyPagination(int Skip, int Take)
        {
            Take = Take;
            Skip = Skip;
            IsPaginated = true;
        }
    }
}
