using AutoMapper;
using LivrariaApi.Models;
using LivrariaApi.ViewModels;

namespace LivrariaApi.Data.Mapper;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerViewModel>().ReverseMap();
    }
}


