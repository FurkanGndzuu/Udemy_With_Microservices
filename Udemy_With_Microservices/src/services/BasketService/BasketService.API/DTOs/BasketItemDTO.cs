namespace BasketService.API.DTOs
{
    public class BasketItemDTO
    {
        public string courseId { get; set; }
        public string courseName { get; set; }
        public decimal coursePrice { get; set; }
        public int quantity { get; set; }
    }
}
