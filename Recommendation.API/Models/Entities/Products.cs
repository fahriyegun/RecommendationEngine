using System.ComponentModel.DataAnnotations.Schema;

namespace Recommendation.API.Models.Entities
{
    public class Products
    {
       
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public string CategoryId { get; set; }
    }
}
