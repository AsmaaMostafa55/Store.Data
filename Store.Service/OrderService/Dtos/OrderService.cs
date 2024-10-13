using AutoMapper;
using Store.Data.Entities;
using Store.Data.Entities.OrderEntites;
using Store.Repositry.Interfaces;
using Store.Repositry.Specification.OrderSpecs;
using Store.Service.Services.BasketService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.OrderService.Dtos
{
    public class OrderService : IOrderService
    {
        private readonly IBasketService _basketService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IBasketService basketService,IUnitOfWork unitOfWork,IMapper mapper
            )
        {
            _basketService = basketService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderDetailsDto> CreateOrderAsyn(orderDto input)
        {
            //Get basket
            var basket=await _basketService.GetBasketAsync(input.BasketId);
            if (basket == null) 
                throw new Exception("Basket not exsit");
            //

            #region fill Order Item List with Items In The basket


            var OrderItems = new List<OrderItemDto>();
            foreach (var basketItem in basket.BasketItems)
            {

                var ProductItem = await _unitOfWork.Repositry<Product, int>().GetByIdAsync(basketItem.ProductId);
                if (ProductItem == null)
                    throw new Exception($"Product with Id:{basketItem.ProductId}Not Exist");

                var itemOrderd = new ProductItem
                {
                    ProductId = ProductItem.Id,
                    ProductName = ProductItem.Name,
                    PictureUrl = ProductItem.PictureUrl

                };

                var orderitem = new OrderItem
                {
                    Price = ProductItem.Price,
                    Quantity = basketItem.Quantity,
                    productItem = itemOrderd
                };

                var mappedOrderItem = _mapper.Map<OrderItemDto>(orderitem);

                OrderItems.Add(mappedOrderItem);

            }

            #endregion

            #region Get Delivery Method
            var deliveryMethod=await _unitOfWork.Repositry<DeliveryMethod,int>().GetByIdAsync(input.DeliveryMethodId);
            if (deliveryMethod == null)
                throw new Exception("Delivery Method not provided");

            #endregion

            #region calculate subtotal
            var subtotal=OrderItems.Sum(item=>item .Quantity*item.Price);

            #endregion

            #region to do =>Payment

            #endregion
            #region create order
            var mappedShippingAddress=_mapper.Map<ShippingAddress>(input.ShippingAddress);
            var mappedOrderitems = _mapper.Map<List<OrderItem>>(OrderItems);
            var order = new Order
            {
                DeliveryMethodId=deliveryMethod.Id,
                ShippingAddress=mappedShippingAddress,
                Buyeremail=input.BuerEmail,
                BasketId=input.BasketId,
                OrderItems=mappedOrderitems,
                SubTotal=subtotal,


            };
            await _unitOfWork.Repositry<Order,Guid>().AddAsync(order);
            await _unitOfWork.CompleteAsync();
            var mappedOrder=_mapper.Map<OrderDetailsDto>(order);
            return mappedOrder;

            #endregion

        }




        public async Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodAsync()
        => await _unitOfWork.Repositry<DeliveryMethod, int>().GetAllAsync();

        public async Task<IReadOnlyList<OrderDetailsDto>> GetAllOrdersForUserAsync(string buyerEmail)
        {
            var specs = new OrderWithitemSpecification(buyerEmail);

            var orders=await _unitOfWork.Repositry<Order,Guid>().GetAllWithSpecificationAsync(specs);

            if (!orders.Any())
                throw new Exception("You Do not have any Orders Yet!");
            var mappedOrders = _mapper.Map<List<OrderDetailsDto>>(orders);
            return mappedOrders;
        }

        public async Task<OrderDetailsDto> GetOrderByIdAsync(Guid id)
        {
            var specs = new OrderWithitemSpecification(id);

            var order = await _unitOfWork.Repositry<Order, Guid>().GetWithSpecificationByIdAsync(specs);

            if (order is null)
                throw new Exception("there is no Order with id { id}");
            var mappedOrder = _mapper.Map<OrderDetailsDto>(order);
            return mappedOrder;
        }
    }
}
