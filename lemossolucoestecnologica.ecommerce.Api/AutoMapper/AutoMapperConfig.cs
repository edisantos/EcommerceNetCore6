using AutoMapper;
using lemossolucoestecnologia.ecommerce.Domain.Entities;
using lemossolucoestecnologia.ecommerce.Domain.Entities.Account;
using lemossolucoestecnologica.ecommerce.Api.ViewModels;

namespace lemossolucoestecnologica.ecommerce.Api.AutoMapper
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Users, UsersViewModels>().ReverseMap();
            CreateMap<Users, LoginViewModel>().ReverseMap();
            CreateMap<Products, ProductsViewModel>().ReverseMap();
        }
    }
}
