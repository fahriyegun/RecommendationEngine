namespace Recommendation.API.Models.DTOs
{
    public class MostDistinctUsers
    {
        public string ProductId { get; set; }
        public string CategoryId { get; set; }
        public int UserCount { get; set; }
    }
}
