using Store.Data.Entities.OrderEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repositry.Specification.OrderSpecs
{
    public class OrderWithPaymentIntentSpecification : Basespecification<Order>
    {
        public OrderWithPaymentIntentSpecification(string ? PaymentIntentId) : base(order =>order.PaymentIntentId== PaymentIntentId)
        {

        }
    }
}
