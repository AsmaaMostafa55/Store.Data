
using StackExchange.Redis;
using Store.Repositry.Basket.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Store.Repositry.Basket
{
    public class Basketrepositry : IBasketRepositry
    {
        private readonly IDatabase _database;
        public Basketrepositry(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
      =>await _database.KeyDeleteAsync(basketId);

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var basket = await _database.StringGetAsync(basketId);
            return basket.IsNullOrEmpty?null:JsonSerializer.Deserialize<CustomerBasket>(basket);

        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var isCreated = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
            if (!isCreated) 
            return null;
            return await GetBasketAsync(basket.Id);            
            
            
        }
    }
}
