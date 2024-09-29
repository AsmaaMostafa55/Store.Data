using Store.Data.Entities;

namespace Store.Repositry.Specification.ProductSpecs
{
    public class ProductWithSpecifications : Basespecification<Product>
    {
        public ProductWithSpecifications(ProductSpecification specs) 
            : base(Product => 
            (!specs.BrandId.HasValue || Product.BrandId == specs.BrandId.Value)
                          && (!specs.TypeId.HasValue || Product.TypeId == specs.TypeId.Value)
            )
            
        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Type);
            AddOrderBy(x => x.Name);
            ApplyPagination(specs.PageSize * (specs.PageIndex - 1), specs.PageSize);

            if (!string.IsNullOrEmpty(specs.Sort))
            {
                switch (specs.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "Pricedesc":
                        AddOrderByDescending(x => x.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;

                }

            }

        }

    }
}
