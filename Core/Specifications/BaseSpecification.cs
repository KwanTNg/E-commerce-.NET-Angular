using System;
using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specifications;

//To enable creating specfication without the where clause
//Add ? to make criteria optional
public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
{
    //Create an emptied constructor
    protected BaseSpecification() : this(null) { }

    public Expression<Func<T, bool>>? Criteria => criteria;

    public Expression<Func<T, object>>? OrderBy { get; private set; }

    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    //for projection
    public bool IsDistinct { get; private set; }

    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }
    protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByDescending = orderByDescExpression;
    }

    //for projection
    protected void ApplyDistinct()
    {
        IsDistinct = true;
    }
}
//This class is created for projection(e.g. get the list of brand of products)
public class BaseSpecification<T, TResult>(Expression<Func<T, bool>> criteria)
 : BaseSpecification<T>(criteria), ISpecification<T, TResult>
{
    protected BaseSpecification() : this(null!) { }
    public Expression<Func<T, TResult>>? Select { get; private set; }
    protected void AddSelect(Expression<Func<T, TResult>> selectExpression)
    {
        Select = selectExpression;
    }
}
