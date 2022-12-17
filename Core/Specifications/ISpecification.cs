using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Specifications;

public interface ISpecification<T>
{
    // Search criteria
    // An Expression takes a function, and the function takes a type T and what is returning, in this case a boolean
    Expression<Func<T, bool>> Criteria {get; }
    // Includes let us populate the data in a custom specification class
    List<Expression<Func<T, object>>> Includes {get; }
    Expression<Func<T, object>> OrderBy {get; }
    Expression<Func<T, object>> OrderByDescending {get; }
    // Pagination
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
}