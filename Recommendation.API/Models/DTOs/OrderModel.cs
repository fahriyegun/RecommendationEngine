namespace Recommendation.API.Models.DTOs
{
    public class OrderModel
    {
        public decimal OrderId { get; set; }
        public string UserId { get; set; } = null!;

    }
}
