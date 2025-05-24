using System;
using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    //Below is a traditional constructor
    //Critera(e.g. brand, type) is property of the base class
    public ProductSpecification(string? brand, string? type) : base(x =>
    (string.IsNullOrWhiteSpace(brand) || x.Brand == brand) &&
    (string.IsNullOrWhiteSpace(type) || x.Type == type)
    )
    {

    }

}
