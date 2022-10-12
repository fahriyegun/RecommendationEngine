using Recommendation.API.Models.DTOs;
using Recommendation.API.Models.Entities;

namespace Recommendation.API.Interfaces
{
    public interface IProduct
    {
        string Userid { get; set; }
        ProductModel CreateProductModel { get; set; }
        void Add();
        HistoryModel GetBestSellerProducts();
    }
}
