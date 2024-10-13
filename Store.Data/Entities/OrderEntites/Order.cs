using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Entities.OrderEntites
{
    public class Order:Baseentity<Guid>
    {
        public string Buyeremail {  get; set; }
        public DateTimeOffset? OrderDate { get; set; }=DateTimeOffset.Now;
        public ShippingAddress ShippingAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }

        public DeliveryMethod DeliveryMethodId { get; set; }

        public OrderStatus OrderStatus { get; set; } = OrderStatus.placed;
        public orderPaymentStatus OrderPaymentStatus { get; set; } = orderPaymentStatus.Pending;
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        
        public decimal SubTotal {  get; set; }
        public string? BasketId { get; set; }
        public decimal GetTotal()
            => SubTotal + DeliveryMethod.Price;
        public string? PaymentIntentId { get; set; }
    }
}
