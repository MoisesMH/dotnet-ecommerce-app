using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Specifications;

public interface ISpecification<T>
{
    // An Expression takes a function, and the function takes a type T and what is returning, in this case a boolean
    Expression<Func<T, bool>> Criteria {get; }
    List<Expression<Func<T, object>>> Includes {get; }
}