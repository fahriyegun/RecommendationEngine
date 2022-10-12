using AutoMapper;
using Recommendation.API.DbContexts;
using Recommendation.API.Interfaces;
using Recommendation.API.Models.DTOs;
using Recommendation.API.Models.Entities;

namespace Recommendation.API.Services
{
    public class SOrder : IOrder
    {
        public IRecommendationDbContext _context;
        private readonly IMapper _mapper;
        public OrderModel CreateOrderModel { get; set; }

        public SOrder(IRecommendationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Add()
        {
            var item = _context.Orders.FirstOrDefault(p => p.Id == CreateOrderModel.OrderId);

            if (item == null)
            {
                Orders order = _mapper.Map<Orders>(CreateOrderModel);
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
        }
    }
}
