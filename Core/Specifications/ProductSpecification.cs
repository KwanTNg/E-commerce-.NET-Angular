using System;
using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    //Pass object as argument
    public ProductSpecification(ProductSpecsParams specsParams) : base(x =>
    //Search functionality
    (string.IsNullOrEmpty(specsParams.Search) || x.Name.ToLower().Contains(specsParams.Search)) &&
    (!specsParams.Brands.Any() || specsParams.Brands.Contains(x.Brand)) &&
    (!specsParams.Types.Any() || specsParams.Types.Contains(x.Type))
    )
    {
        // pagination part 2, first page is 6 * 0 = 6
        ApplyPaging(specsParams.PageSize * (specsParams.PageIndex - 1), specsParams.PageSize);

        switch (specsParams.Sort)
        {
            case "priceAsc":
                AddOrderBy(x => x.Price);
                break;
            case "priceDesc":
                AddOrderByDescending(x => x.Price);
                break;
            default:
                AddOrderBy(x => x.Name);
                break;
        }

    }

}
