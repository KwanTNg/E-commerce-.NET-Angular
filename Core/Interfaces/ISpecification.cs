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

    //for projection
    bool IsDistinct { get; }

    //for pagination part 1
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
    //for pagination part 3
    IQueryable<T> ApplyCriteria(IQueryable<T> query);
}

//This class is created as no product or list of product will return
//For projection (e.g. Get the list of brand for all products)
public interface ISpecification<T, TResult> : ISpecification<T>
{
    Expression<Func<T, TResult>>? Select { get; }
}
