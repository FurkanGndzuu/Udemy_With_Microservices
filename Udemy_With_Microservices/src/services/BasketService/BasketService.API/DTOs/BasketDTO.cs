namespace BasketService.API.DTOs
{
    public class BasketDTO
    {
        public string userId { get; set; }
        public List<BasketItemDTO> basketItems { get; set; }
        public string discountCode { get; set; }
        public decimal price { get => basketItems.Sum(x => x.quantity * x.coursePrice); }
    }
}
