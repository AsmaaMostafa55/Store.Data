﻿using Store.Data.Entities.OrderEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.OrderService.Dtos
{
    public class OrderDetailsDto
    {
        public Guid Id { get; set; }
        public string BuyerEmail {  get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public AddressDto  ShippingAddress { get; set; }
        public string DeliveryMethodName {  get; set; }
        public orderPaymentStatus OrderPaymentStatus { get; set; }

        public OrderStatus OrderStatus { get; set; }
        public IReadOnlyList<OrderItemDto> orderItems { get; set; }
        public decimal SubTotal {  get; set; }
        public decimal Total { get; set; }
        public decimal ShippingPrice {  get; set; }
        public string? BasketId {  get; set; }
        public string? PaymentIntentId { get; set; }
    }
}
