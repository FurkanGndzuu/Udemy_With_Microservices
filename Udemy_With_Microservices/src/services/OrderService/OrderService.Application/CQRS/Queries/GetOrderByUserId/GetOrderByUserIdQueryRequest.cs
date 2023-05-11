using MediatR;
using OrderService.Application.DTOs;
using ServicesShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.CQRS.Queries.GetOrderByUserId
{
    public class GetOrderByUserIdQueryRequest : IRequest<Response<List<OrderDTO>>>    
    {
        public string userId { get; set; }
    }
}
