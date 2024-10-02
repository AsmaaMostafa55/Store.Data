using Store.Data.Entities;

namespace Store.Repositry.Specification.ProductSpecs
{
    public class ProductWithCountSpecification:Basespecification<Product>
    {
        public ProductWithCountSpecification(ProductSpecification specs)
           : base(Product =>
           (!specs.BrandId.HasValue || Product.BrandId == specs.BrandId.Value)
             && (!specs.TypeId.HasValue || Product.TypeId == specs.TypeId.Value)&&
             (string.IsNullOrEmpty(specs.search) || Product.Name.Trim().ToLower().Contains(specs.search))

           )
        {

        }
    }
}
