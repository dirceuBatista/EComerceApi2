using AutoMapper;
using LivrariaApi.Models;
using LivrariaApi.ViewModels;

namespace LivrariaApi.Data.Mapper;

public class OrderProfile :Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderViewModel>().ReverseMap();
        CreateMap<OrderItem, OrderItemViewModel>()
            .ForMember(dest => dest.BookId, opt => opt.Ignore())
            .ForMember(dest => dest.OrderId, opt => opt.Ignore());
    }
    
}