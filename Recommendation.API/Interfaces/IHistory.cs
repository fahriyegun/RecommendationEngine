using Recommendation.API.Models.DTOs;

namespace Recommendation.API.Interfaces
{
    public interface IHistory
    {
        string UserId {get; set;}
        CreateHistoryModel CreateHistoryModel { get; set; }
        DeleteHistoryModel DeleteHistoryModel { get; set; }
        void Add();
        void Delete();
        HistoryModel GetBrowsingHistory();
    }
}
