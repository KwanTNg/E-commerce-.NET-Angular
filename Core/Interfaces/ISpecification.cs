using System;
using System.Linq.Expressions;

namespace Core.Interfaces;

//Support generic filtering and sorting
public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }

}
