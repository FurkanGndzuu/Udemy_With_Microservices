using OrderService.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get;  set; }

        public AdressDTO Address { get;  set; }

        public string BuyerId { get;  set; }

        public IReadOnlyCollection<OrderItemDTO> OrderItems {get; set;}
    }
}
