using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications;

public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
{
    public ProductWithFiltersForCountSpecification(ProductSpecParams productParams)
        // Count items and populate that in our pagination class
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
    }
}