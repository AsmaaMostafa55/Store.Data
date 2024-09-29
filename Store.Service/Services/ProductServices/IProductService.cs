using Store.Repositry.Specification.ProductSpecs;
using Store.Service.Helper;
using Store.Service.Services.ProductServices.Dtos;

namespace Store.Service.Services.ProductServices
{
    public interface IProductService
    {
        Task<ProductDetailsDto> GetProductByIdASync(int? ProductId);
        Task<PaginatedResulttDto<ProductDetailsDto>> GetAllProductsAsync(ProductSpecification specs);
        Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandssAsync();
        Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync();


    }
}
