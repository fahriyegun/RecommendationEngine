using System.ComponentModel.DataAnnotations;

namespace Recommendation.API.Models.Entities
{

    public class History
    {
        [Key]
        public string Messageid { get; set; }
        public string Userid { get; set; }
        public DateTime ViewedDate { get; set; }
        public string Productid { get; set; }
        public Products Product { get; set; }

    }
}
