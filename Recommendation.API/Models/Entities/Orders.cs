using System.ComponentModel.DataAnnotations.Schema;

namespace Recommendation.API.Models.Entities
{
    public class Orders
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public decimal Id { get; set; }
        public string UserId { get; set; } = null!;

        public List<OrderItems> OrderItems { get; set; }
    }
}
