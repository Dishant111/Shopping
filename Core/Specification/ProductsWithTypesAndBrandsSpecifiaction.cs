using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class ProductsWithTypesAndBrandsSpecifiaction : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecifiaction(ProductSpecParams specParams)
            : base(
                 (x) =>
                 (string.IsNullOrEmpty(specParams.Search) || x.Name.Contains(specParams.Search)) &&
                 (!specParams.BrandId.HasValue || x.ProductBrandId == specParams.BrandId) &&
                 (!specParams.TypeId.HasValue || x.ProductTypeId == specParams.TypeId)
                 )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);

            if (!string.IsNullOrWhiteSpace(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case nameof(Product.Price):
                        AddOrderBy(x => x.Price);
                        break;
                    case nameof(Product.Price) + "Desc":
                        AddOrderByDesceding(x => x.Price);
                        break;
                    default:
                        break;
                }
            }

            ApplyPaging((specParams.PageIndex - 1) * specParams.PageSize, specParams.PageSize);
        }

        public ProductsWithTypesAndBrandsSpecifiaction(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }

    }
}
