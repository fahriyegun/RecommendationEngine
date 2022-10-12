using System.ComponentModel.DataAnnotations.Schema;

namespace Recommendation.API.Models.Entities
{
    public class OrderItems
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string ProductId { get; set; } = null!;
        public decimal Quantity { get; set; }
        public decimal OrderId { get; set; }

        public Orders Order { get; set; } = null!;
        public Products Product { get; set; } = null!;
    }
}
