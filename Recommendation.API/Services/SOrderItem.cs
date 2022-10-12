using AutoMapper;
using Recommendation.API.DbContexts;
using Recommendation.API.Interfaces;
using Recommendation.API.Models.DTOs;
using Recommendation.API.Models.Entities;

namespace Recommendation.API.Services
{
    public class SOrderItem : IOrderItem
    {
        public IRecommendationDbContext _context;
        private readonly IMapper _mapper;
        public OrderItemModel CreateOrderItemModel { get; set; }

        public SOrderItem(IRecommendationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Add()
        {
            var item = _context.OrderItems.FirstOrDefault(p => p.Id == CreateOrderItemModel.Id);
            var order = _context.Orders.FirstOrDefault(x => x.Id == CreateOrderItemModel.OrderId);

            if (item == null && order != null)
            {
                OrderItems orderItem = _mapper.Map<OrderItems>(CreateOrderItemModel);
                _context.OrderItems.Add(orderItem); 
                _context.SaveChanges();

            }
        }
    }
}
