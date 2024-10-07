using AutoMapper;
using Store.Repositry.Basket;
using Store.Repositry.Basket.Models;
using Store.Service.Services.BasketService.Dtos;

namespace Store.Service.Services.BasketService
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepositry _basketRepositry;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepositry basketRepositry,IMapper mapper)
        {
           _basketRepositry = basketRepositry;
            _mapper = mapper;
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        =>await _basketRepositry.DeleteBasketAsync(basketId);

        public async Task<CustomerBasketDto> GetBasketAsync(string basketId)
        {
            var basket= await _basketRepositry.GetBasketAsync(basketId);
            if (basket == null)
                return new CustomerBasketDto();

            var mappedBasket=_mapper.Map<CustomerBasketDto>(basket);
            return mappedBasket;
        }

        public async Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto input)
        {
            if (input.Id == null)
                input.Id= GenerateRandomBasketId();


                    var customerBasket=_mapper.Map<CustomerBasket>(input);
            var updateBasket = await _basketRepositry.UpdateBasketAsync(customerBasket);
            var mappedUpdatedBasket = _mapper.Map<CustomerBasketDto>(updateBasket);
            return mappedUpdatedBasket;
        }
        private string GenerateRandomBasketId()
        {
            Random random=new Random();
            int randomDigits = random.Next(1000, 10000);
            return $"Bs-{randomDigits}";
        }
    }
}
