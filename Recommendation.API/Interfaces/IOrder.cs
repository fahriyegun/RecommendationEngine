using Recommendation.API.Models.DTOs;

namespace Recommendation.API.Interfaces
{
    public interface IOrder
    {
        OrderModel CreateOrderModel { get; set; }
        void Add();
    }
}
