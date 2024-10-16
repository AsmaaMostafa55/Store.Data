using AutoMapper;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using Store.Data.Entities;
using Store.Data.Entities.OrderEntites;
using Store.Repositry.Basket.Models;
using Store.Repositry.Interfaces;
using Store.Repositry.Specification.OrderSpecs;
using Store.Service.OrderService.Dtos;
using Store.Service.Services.BasketService;
using Store.Service.Services.BasketService.Dtos;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Store.Data.Entities.Product;

namespace Store.Service.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        public PaymentService(IConfiguration  configuration,IUnitOfWork unitOfWork,IBasketService basketService,IMapper mapper)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _basketService = basketService;
           _mapper = mapper;
        }



        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(CustomerBasketDto input)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
            if (input == null)
                throw new Exception("Basket is Empty ");

            var deliveryMethod = await _unitOfWork.Repositry<DeliveryMethod, int>().GetByIdAsync(input.DeliverymethodId.Value);
            if (deliveryMethod == null)
                throw new Exception("Delivery Method not provided");

            decimal ShippingPrice = deliveryMethod.Price;
            foreach (var item in input.BasketItems) {
          var  Product= await _unitOfWork.Repositry<Product, int>().GetByIdAsync(item.ProductId);

                if(item.Price !=Product.Price)
                    item.Price = Product.Price;
            }
            var service=new PaymentIntentService();

            PaymentIntent paymentIntent;

            if (string.IsNullOrEmpty(input.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount=(long)input.BasketItems.Sum(item=>item.Quantity*(item.Price*100))+(long)(ShippingPrice*100)
                    Currency="usd",
                    PaymentMethodTypes=new List<string> { "card" }
                };
                paymentIntent=await service.CreateAsync(options);
                input.PaymentIntentId=paymentIntent.Id;
                input.ClientSecret=paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)input.BasketItems.Sum(item => item.Quantity * (item.Price * 100)) + (long)(ShippingPrice * 100),
                    
                   
                };
                await service.UpdateAsync(input.PaymentIntentId,options
                    );

            }
            await _basketService.UpdateBasketAsync(input);
            return input;
        }

        public async Task<OrderDetailsDto> UpdateOrderPaymentFailed(string PaymentIntentId)
        {
            var specs = new OrderWithPaymentIntentSpecification(PaymentIntentId);
            var order = await _unitOfWork.Repositry<Data.Entities.OrderEntites.Order, Guid>().GetWithSpecificationByIdAsync(specs);
            if (order == null)
                throw new Exception("Order Does not Exist");

            order.OrderPaymentStatus = orderPaymentStatus.Failed;
            _unitOfWork.Repositry<Data.Entities.OrderEntites.Order, Guid>().Update(order);
            await _unitOfWork.CompleteAsync();
            var mappedOrder =_mapper.Map<OrderDetailsDto>(order);
            return mappedOrder;

            
        }

        public async Task<OrderDetailsDto> UpdateOrderPaymentSucceeded(string PaymentIntentId)
        {
            var specs = new OrderWithPaymentIntentSpecification(PaymentIntentId);
            var order = await _unitOfWork.Repositry<Data.Entities.OrderEntites.Order, Guid>().GetWithSpecificationByIdAsync(specs);
            if (order == null)
                throw new Exception("Order Does not Exist");

            order.OrderPaymentStatus = orderPaymentStatus.Received;
            _unitOfWork.Repositry<Data.Entities.OrderEntites.Order, Guid>().Update(order);
            await _unitOfWork.CompleteAsync();
            await _basketService.DeleteBasketAsync(order.BasketId);
            var mappedOrder = _mapper.Map<OrderDetailsDto>(order);
            return mappedOrder;
        }
    }
}
