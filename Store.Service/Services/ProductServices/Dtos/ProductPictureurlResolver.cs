using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.ProductServices.Dtos
{
    public class ProductPictureurlResolver : IValueResolver<Product, ProductDetailsDto, String>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureurlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDetailsDto destination, string destMember, ResolutionContext context)
        {
           if(!string.IsNullOrEmpty(source.PictureUrl))
                return $"{_configuration["BaseUrl"]}/{ source.PictureUrl}";
            return null;
        }
    }
}
