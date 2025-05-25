using AutoMapper;
using LivrariaApi.Models;
using LivrariaApi.ViewModels;

namespace LivrariaApi.Data.Mapper;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryViewModel>().ReverseMap();
    }
}