using Store.Repositry.Basket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.BasketService.Dtos
{
    public class CustomerBasketDto
    {
        public string? Id { get; set; }
        public int? DeliverymethodId { get; set; }
        public decimal ShippingPrice { get; set; }

        public List<BasketItemDto> BasketItems { get; set; } = new List<BasketItemDto>();
    }
}
