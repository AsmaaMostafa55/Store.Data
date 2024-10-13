using Store.Data.Entities.OrderEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repositry.Specification.OrderSpecs
{
    public class OrderWithitemSpecification : Basespecification<Order>
    {
        public OrderWithitemSpecification(string buyerEmail) 
            : base(order=>order.Buyeremail==buyerEmail)
        {
            {
                AddInclude(order=>order.DeliveryMethod);
                AddInclude(order=>order.OrderItems);
                AddOrderByDescending(order=>order.OrderDate);
            }

        }


        public OrderWithitemSpecification(Guid id)
            : base(order => order.Id == id)
        {
            {
                AddInclude(order => order.DeliveryMethod);
                AddInclude(order => order.OrderItems);
               
            }

        }
    }
}
