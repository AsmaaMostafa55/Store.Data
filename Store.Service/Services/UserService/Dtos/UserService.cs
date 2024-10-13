using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.IdentityEntities;
using Store.Service.Services.TokenService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.UserService.Dtos
{
    public class UserService : IUserService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public UserService(SignInManager <AppUser> signInManager,UserManager<AppUser> userManager,
            ITokenService tokenService )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }
        public async Task<UserDto> Login(LoginDto input)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);
            if (user == null)
                return null;
            var result = await _signInManager.CheckPasswordSignInAsync(user,input.Password,false);
            if (!result.Succeeded)
                throw new Exception("Login Fail");

            return new UserDto
            { 
            id=Guid.Parse(user.Id),
                Name = user.DisplayName,
            Email=user.Email,
            Token=_tokenService.GenerateToken(user)
            };
        }

        public async Task<UserDto> Register(RegisterDto input)
        {

            var user = await _userManager.FindByEmailAsync(input.Email);
            if (user is not null)
                return null;

            var appuser = new AppUser
            {
                DisplayName = input.DisplayName,
                Email = input.Email,
                UserName = input.DisplayName,

            };

            var result=await _userManager.CreateAsync(appuser,input.Password);
            if (result.Succeeded)
                throw new Exception(result.Errors.Select(x=>x.Description).FirstOrDefault());


            return new UserDto
            {
                id = Guid.Parse(appuser.Id),
                Name = appuser.DisplayName,
                Email = appuser.Email,
                Token = _tokenService.GenerateToken(user)
            };


        }


       

        

    }
}
