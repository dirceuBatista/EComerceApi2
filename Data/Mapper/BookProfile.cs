using AutoMapper;
using LivrariaApi.Models;
using LivrariaApi.ViewModels;

namespace LivrariaApi.Data.Mapper;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookViewModel>().ReverseMap();
    }
    
}