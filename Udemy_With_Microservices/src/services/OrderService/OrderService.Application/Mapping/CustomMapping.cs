using AutoMapper;
using OrderService.Application.DTOs;
using OrderService.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Mapping
{
    public class CustomMapping : Profile
    {
        public CustomMapping()
        {
            CreateMap<Order , OrderDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<Adress, AdressDTO>().ReverseMap();
        }
    }
}
