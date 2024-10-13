using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Entities.OrderEntites
{
    public class OrderItem :Baseentity<Guid>
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public ProductItem productItem { get; set; }
        public ProductItemOrdered itemOrdered { get; set; }
        public Guid OrderId { get; set; }
    }
}
