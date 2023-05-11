namespace DiscountService.API.DTOs
{
    public class DiscountDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Code { get; set; }
        public int Rate { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

    }
}
