using Store.Data.Entities;
using Store.Repositry.Interfaces;
using Store.Service.Services.Product.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductEntity = Store.Data.Entities.Product;

namespace Store.Service.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandssAsync()
        {
         var brands=await _unitOfWork.Repositry<ProductBrand,int>().GetAllAsNoTrackingAsync();

           /* IReadOnlyList<BrandTypeDetailsDto>*/ var  mappedBarands = brands.Select(x => new BrandTypeDetailsDto
            {
                Id= x.Id,
                Name= x.Name,
                CreatedAt= x.CreatedAt,
            }).ToList();
            return mappedBarands;
        }

        public async Task<IReadOnlyList<ProductDetailsDto>> GetAllProductsAsync()
        {
       var PRoducts=await _unitOfWork.Repositry<ProductEntity,int>().GetAllAsNoTrackingAsync();
            var mappedProducts = PRoducts.Select(x => new ProductDetailsDto
            {
Id = x.Id,
Name= x.Name,
Description= x.Description,
PictureUrl= x.PictureUrl,
Price= x.Price,
CreatedAt   = x.CreatedAt,
BrandName   =x.Brand.Name,
TypeName=x.Type.Name
    
            }).ToList();
            return mappedProducts;
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repositry<ProductType, int>().GetAllAsNoTrackingAsync();

            /* IReadOnlyList<BrandTypeDetailsDto>*/
            var mappedtypes = types.Select(x => new BrandTypeDetailsDto
            {
                Id = x.Id,
                Name = x.Name,
                CreatedAt = x.CreatedAt,
            }).ToList();
            return mappedtypes;
        }

        public async Task<ProductDetailsDto> GetProductByIdASync(int? ProductId)
        {
            if (ProductId == null)
                throw new Exception("Id is null");
            var product = await _unitOfWork.Repositry<ProductEntity, int>().GetByIdAsync(ProductId.Value);
            if (product == null)
                throw new Exception("product Not FOund");
            var mappedProduct = new ProductDetailsDto
            {
                Id = product.Id,
                Name = product.Name,
                CreatedAt = product.CreatedAt,
                Description = product.Description,
                Price   = product.Price,
                PictureUrl = product.PictureUrl,
                BrandName = product.Brand.Name,
                TypeName= product.Type.Name,


            };
            return mappedProduct;
        }
    }
}
