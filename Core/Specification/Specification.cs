using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public interface ISpecification<T>
    {
        Expression<Func<T,bool>> Criteria { get; }
        List<Expression<Func<T,object>>> Includes { get; }
        Expression<Func<T, object>> OrderBy { get; }

        Expression<Func<T, object>> OrderByDesceding { get; }

        public int Skip { get; }
        public int Take { get; }
        public bool IsPagingEnable { get; }
    }
}
