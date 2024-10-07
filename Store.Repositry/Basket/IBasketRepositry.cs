using Store.Repositry.Basket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repositry.Basket
{
    public interface IBasketRepositry
    {
        Task<CustomerBasket> GetBasketAsync(string basketId);

        Task<CustomerBasket>UpdateBasketAsync(CustomerBasket basket);
        Task <bool> DeleteBasketAsync(string basketId);








    }
}
