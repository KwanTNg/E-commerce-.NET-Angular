using System;
using System.Linq.Expressions;

namespace Core.Interfaces;

//Support generic filtering and sorting
public interface ISpecification<T>
{
    //Filtering
    Expression<Func<T, bool>>? Criteria { get; }
    //Sorting
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }

}
