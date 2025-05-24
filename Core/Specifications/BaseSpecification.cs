using System;
using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specifications;

public class BaseSpecification<T> : ISpecification<T>
{
    private readonly Expression<Func<T, bool>> criteria;
    //When we create a new instance of base specification, we're going to be able to pass in an expression
    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        this.criteria = criteria;
    }
    public Expression<Func<T, bool>> Criteria => criteria;
}
