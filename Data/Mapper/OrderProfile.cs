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
        CreateMap<OrderItem, OrderItemViewModel>();
        CreateMap<OrderItemViewModel, OrderItem>();
    }

}