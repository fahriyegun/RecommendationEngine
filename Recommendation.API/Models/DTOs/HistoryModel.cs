using Recommendation.API.Enums;

namespace Recommendation.API.Models.DTOs
{
    public class HistoryModel
    {
        public string UserId { get; set; }
        public List<string> Products { get; set; }
        public string RecommendationType { get; set; }
    }


    public class Context
    {
        public string source { get; set; }
    }

    public class Properties
    {
        public string productid { get; set; }
    }

    public class CreateHistoryModel
    {
        public string @event { get; set; }
        public string messageid { get; set; }
        public string userid { get; set; }
        public DateTime ViewedDate { get; set; }
        public Properties properties { get; set; }
        public Context context { get; set; }
    }

    public class DeleteHistoryModel
    {       
        public string productid { get; set; }
        public string userid { get; set; }
    }

}
