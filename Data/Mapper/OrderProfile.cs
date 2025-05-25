using AutoMapper;
using LivrariaApi.Models;
using LivrariaApi.ViewModels;
using LivrariaApi.ViewModels.InputOrder;

namespace LivrariaApi.Data.Mapper;

public class OrderProfile :Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderViewModel>().ReverseMap();
        CreateMap<OrderViewModel, InputOrderCreate>().ReverseMap();
        CreateMap<OrderItemViewModel, OrderItem>().ReverseMap();
        CreateMap<OrderItem, InputOrderUpdate>().ReverseMap();
        CreateMap<OrderViewModel, InputOrderUpdate>().ReverseMap();

    }

}