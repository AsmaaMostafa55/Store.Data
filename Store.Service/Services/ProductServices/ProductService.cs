using AutoMapper;
using Store.Data.Entities;
using Store.Repositry.Interfaces;
using Store.Repositry.Specification.ProductSpecs;
using Store.Service.Helper;
using Store.Service.Services.ProductServices.Dtos;
using System.Collections.Generic;
using ProductEntity = Store.Data.Entities.Product;

namespace Store.Service.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork ,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandssAsync()
        {
            var brands = await _unitOfWork.Repositry<ProductBrand, int>().GetAllAsNoTrackingAsync();

            /* IReadOnlyList<BrandTypeDetailsDto>*/
            //var mappedBarands = brands.Select(x => new BrandTypeDetailsDto
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    CreatedAt = x.CreatedAt,
            //}).ToList();
            var mappedBarands = _mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(brands);
            return mappedBarands;
        }

        public async Task<PaginatedResulttDto<ProductDetailsDto>> GetAllProductsAsync(ProductSpecification input)
        {
            var specs = new ProductWithSpecifications(input);
            var PRoducts = await _unitOfWork.Repositry<ProductEntity, int>().GetAllWithSpecificationAsync(specs);
            var mappedProducts = _mapper.Map<IReadOnlyList<ProductDetailsDto>>(PRoducts);
            //    PRoducts.Select(x => new ProductDetailsDto
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    Description = x.Description,
            //    PictureUrl = x.PictureUrl,
            //    Price = x.Price,
            //    CreatedAt = x.CreatedAt,
            //    BrandName = x.Brand.Name,
            //    TypeName = x.Type.Name

            //}).ToList();
            var CountSpecs = new ProductWithCountSpecification(input);
            var Count = await _unitOfWork.Repositry<ProductEntity, int>().GetCountSpecificationAsync(CountSpecs);
                return new PaginatedResulttDto<ProductDetailsDto>(input.PageSize,input.PageIndex,PRoducts.Count,mappedProducts);
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repositry<ProductType, int>().GetAllAsNoTrackingAsync();

            /* IReadOnlyList<BrandTypeDetailsDto>*/
            var mappedtypes = _mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(types);
            //    = types.Select(x => new BrandTypeDetailsDto
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    CreatedAt = x.CreatedAt,
            //}).ToList();
            return mappedtypes;
        }

        public async Task<ProductDetailsDto> GetProductByIdASync(int? ProductId)
        {
            if (ProductId == null)
                throw new Exception("Id is null");

            var specs = new ProductWithSpecifications(ProductId);


            var product = await _unitOfWork.Repositry<ProductEntity, int>().GetWithSpecificationByIdAsync(specs);
            if (product == null)
                throw new Exception("product Not FOund");
            var mappedProduct = _mapper.Map<ProductDetailsDto>(product);
            //    new ProductDetailsDto
            //{
            //    Id = product.Id,
            //    Name = product.Name,
            //    CreatedAt = product.CreatedAt,
            //    Description = product.Description,
            //    Price = product.Price,
            //    PictureUrl = product.PictureUrl,
            //    BrandName = product.Brand.Name,
            //    TypeName = product.Type.Name,


            //};
            return mappedProduct;
        }
    }
}
