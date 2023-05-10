using MediatR;
using OrderService.Application.DTOs;
using OrderService.Domain.OrderAggregate;
using OrderService.Infrastructure.Context;
using ServicesShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.CQRS.Commands.CreateOrderC
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, Response<CreatedOrderDTO>>
    {
        private readonly OrderDbContext _context;

        public CreateOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Response<CreatedOrderDTO>> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var newAddress = new Adress(request.adress.Province, request.adress.District, request.adress.Street, request.adress.ZipCode, request.adress.Line);

            Order newOrder = new Order(request.BuyerId, newAddress);

            request.orderItems.ForEach(x =>
            {
                newOrder.AddOrderItem(x.ProductId, x.ProductName, x.ProductPrice, x.PictureUrl);
            });

            await _context.Orders.AddAsync(newOrder);

            await _context.SaveChangesAsync();

            return Response<CreatedOrderDTO>.Success(new CreatedOrderDTO { OrderId = newOrder.Id }, 200);
        }
    }
}
