using Core.Entities;

namespace Core.Specification
{
    public class ProductsWithFilterForCountsSpecifiaction : BaseSpecification<Product>
    {
        public ProductsWithFilterForCountsSpecifiaction(ProductSpecParams specParams)
                        : base(
                              (x) =>
                              (string.IsNullOrEmpty(specParams.Search) || x.Name.Contains(specParams.Search)) &&
                              (!specParams.BrandId.HasValue || x.ProductBrandId == specParams.BrandId) &&
                              (!specParams.TypeId.HasValue || x.ProductTypeId == specParams.TypeId)
                              )
        {

        }

    }
}
