using AutoMapper;
using LivrariaApi.Models;
using LivrariaApi.ViewModels;
using LivrariaApi.ViewModels.InputViewModel;
using Microsoft.AspNetCore.Identity;

namespace LivrariaApi.Data.Mapper;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserViewModel>().ReverseMap();
        CreateMap<UserViewModel, InputUserUpdate>().ReverseMap();
        
    }
}


