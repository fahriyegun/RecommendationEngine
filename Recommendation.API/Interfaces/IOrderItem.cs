using Recommendation.API.Models.DTOs;

namespace Recommendation.API.Interfaces
{
    public interface IOrderItem
    {
        OrderItemModel CreateOrderItemModel { get; set; }
        void Add();
    }
}
