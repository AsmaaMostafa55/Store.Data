using Store.Service.Services.Product.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.Product
{
    public interface IProductService
    {
        Task<ProductDetailsDto> GetProductByIdASync(int? ProductId);
        Task<IReadOnlyList<ProductDetailsDto>> GetAllProductsAsync();
        Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandssAsync();
        Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync();


    }
}
