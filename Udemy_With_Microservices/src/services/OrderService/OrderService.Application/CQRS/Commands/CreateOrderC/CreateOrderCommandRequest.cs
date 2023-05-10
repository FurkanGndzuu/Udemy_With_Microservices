using MediatR;
using OrderService.Application.DTOs;
using OrderService.Domain.OrderAggregate;
using ServicesShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.CQRS.Commands.CreateOrderC
{
    public class CreateOrderCommandRequest : IRequest<Response<CreatedOrderDTO>>
    {
        public string BuyerId { get; set; }

        public List<OrderItemDTO> orderItems { get; set; }

        public AdressDTO adress { get; set; }
    }
}
