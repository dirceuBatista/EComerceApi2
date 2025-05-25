using AutoMapper;
using LivrariaApi.Models;
using LivrariaApi.ViewModels;
using LivrariaApi.ViewModels.InputViewModel;

namespace LivrariaApi.Data.Mapper;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerViewModel>().ReverseMap();
        CreateMap<CustomerViewModel, InputCustomerUpdate>().ReverseMap();
    }
}


