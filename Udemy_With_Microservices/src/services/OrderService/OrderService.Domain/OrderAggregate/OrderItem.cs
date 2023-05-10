using OrderService.Domain.Core;
using System.Diagnostics;

namespace OrderService.Domain.OrderAggregate
{
    public class OrderItem : Entity
    {
        public string ProductId { get;private set; }
        public string ProductName { get; private set; }
        public decimal ProductPrice { get; private set; }
        public string PictureUrl { get; private set; }

        public OrderItem()
        {
            
        }
        public OrderItem(string productId, string productName, decimal productPrice, string pictureUrl)
        {
            ProductId = productId;
            ProductName = productName;
            ProductPrice = productPrice;
            PictureUrl = pictureUrl;
        }

        public void UpdateOrderItem(string productName, string pictureUrl, decimal price)
        {
            ProductName = productName;
            ProductPrice = price;
            PictureUrl = pictureUrl;
        }
    }
}
