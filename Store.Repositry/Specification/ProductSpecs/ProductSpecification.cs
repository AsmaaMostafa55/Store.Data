using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repositry.Specification.ProductSpecs
{
    public class ProductSpecification
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Sort { get; set; }
        public int PageIndex { get; set; }
        private const int MAXPAGESIZE = 50;
        private int _PageSize = 6;
        public int PageSize
            {
            get=> _PageSize;
    set => _PageSize = (value>MAXPAGESIZE)?int.MaxValue:value;

}
    }
}
