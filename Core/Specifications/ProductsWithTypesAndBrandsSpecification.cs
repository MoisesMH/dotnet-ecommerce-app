using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications;

public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
{
    // public ProductsWithTypesAndBrandsSpecification(string sort, int? brandId, int? typeId)
    public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams)
        // Add criteria which detects if there's a brandId and typeId on the query
        : base(x =>
            // To let the search compare adequately with the input from params
            // Test search functionality
            (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
            // (!brandId.HasValue || x.ProductBrandId == brandId) &&
            // (!typeId.HasValue || x.ProductTypeId == typeId)
            (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
            (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
        )
    {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
        AddOrderBy(x => x.Name);
        // Paging starts from index 0
        // Takes 6 pages by default
        ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

        // Sorting
        // if (!string.IsNullOrEmpty(sort))
        if (!string.IsNullOrEmpty(productParams.Sort))
        {
            // switch (sort)
            switch (productParams.Sort)
            {
                case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break;

                case "priceDesc":
                    AddOrderByDescending(p => p.Price);
                    break;

                default:
                    AddOrderBy(n => n.Name);
                    break;
            }
        }
    }

    public ProductsWithTypesAndBrandsSpecification(int id) : base(
        x => x.Id == id
    )
    {
        AddInclude(x => x.ProductType);
        AddInclude(x => x.ProductBrand);
    }
}