using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.DTOs;
using OrderService.Application.Mapping;
using OrderService.Domain.OrderAggregate;
using OrderService.Infrastructure.Context;
using ServicesShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.CQRS.Queries.GetOrderByUserId
{
    public class GetOrderByUseIdQueryHandler : IRequestHandler<GetOrderByUserIdQueryRequest, Response<List<OrderDTO>>>
    {
        readonly OrderDbContext _context;

        public GetOrderByUseIdQueryHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<OrderDTO>>> Handle(GetOrderByUserIdQueryRequest request, CancellationToken cancellationToken)
        {
            var response = await _context.Orders.Include(x => x.OrderItems).Include(x => x.Address)
                  .Where(x => x.BuyerId == request.userId).ToListAsync();

            if (!response.Any())
                return Response<List<OrderDTO>>.Success(new List<OrderDTO>(), StatusCodes.Status200OK);

            var ordersDto = ObjectMapper.mapper.Map<List<OrderDTO>>(response);

            return Response<List<OrderDTO>>.Success(ordersDto, StatusCodes.Status200OK);


        }
    }
}
