﻿using Microsoft.AspNetCore.Mvc;
using Store.Repositry.Specification.ProductSpecs;
using Store.Service.Services.ProductServices;
using Store.Service.Services.ProductServices.Dtos;
using Store.Web.Helper;

namespace Store.Web.Controllers
{
    //[Route("api/[controller]/[action]")]
    //[ApiController]
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        
        public async Task<ActionResult< IReadOnlyList<BrandTypeDetailsDto>>>GetAllBrands()
            =>Ok(   await _productService.GetAllBrandssAsync());


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BrandTypeDetailsDto>>> GetAllTypes()
           => Ok(await _productService.GetAllTypesAsync());


        [HttpGet]
        [Cache(30)]
        public async Task<ActionResult<IReadOnlyList<ProductDetailsDto>>> GetAllProducts([FromQuery] ProductSpecification input)
            => Ok(await _productService.GetAllProductsAsync(input));
        

        [HttpGet]
        public async Task<ActionResult<ProductDetailsDto>> GetProductById(int? id)
          => Ok(await _productService.GetProductByIdASync(id));

    }
}
