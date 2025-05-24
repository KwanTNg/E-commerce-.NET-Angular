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
}
