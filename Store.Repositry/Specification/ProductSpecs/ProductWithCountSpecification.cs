using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repositry.Specification.ProductSpecs
{
    public class ProductWithCountSpecification:Basespecification<Product>
    {
        public ProductWithSpecifications(ProductSpecification specs)
           : base(Product =>
           (!specs.BrandId.HasValue || Product.BrandId == specs.BrandId.Value)
                         && (!specs.TypeId.HasValue || Product.TypeId == specs.TypeId.Value)
           )
        {

        }
    }
}
