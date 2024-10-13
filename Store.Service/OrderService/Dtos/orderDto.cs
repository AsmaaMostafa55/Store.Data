using Store.Data.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.OrderService.Dtos
{
    public class orderDto
    {
        public string BasketId {  get; set; }
        public string BuerEmail {  get; set; }
        [Required]
        public int DeliveryMethodId {  get; set; }
        public AddressDto ShippingAddress { get; set; }
    }
}
