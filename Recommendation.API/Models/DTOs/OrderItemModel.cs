namespace Recommendation.API.Models.DTOs
{
    public class OrderItemModel
    {
        public int Id { get; set; }
        public string ProductId { get; set; } = null!;
        public decimal Quantity { get; set; }
        public decimal OrderId { get; set; }
    }
}
