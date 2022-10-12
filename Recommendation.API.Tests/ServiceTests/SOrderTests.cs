using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Recommendation.API.DbContexts;
using Recommendation.API.Mappings;
using Recommendation.API.Models.DTOs;
using Recommendation.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Recommendation.API.Tests.ServiceTests
{
    public class SOrderTests : DummyDbGenerator
    {
        private readonly IMapper mapper;

        public SOrderTests()
        {
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ObjectMapperProfile());
            });
            mapper = mockMapper.CreateMapper();

            //sql lite connection configuration
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            SetContextOptions(new DbContextOptionsBuilder<RecommendationDbContext>().UseSqlite(connection).Options);
            Seed();
        }

        [Fact]
        public void Add_ValidModel_ReturnCreated()
        {
            OrderModel model = new OrderModel
            {
                OrderId = 100,
                UserId = "user-1"
            };

            using (var context = new RecommendationDbContext(_contextOption))
            {
                var service = new SOrder(context, mapper);
                service.CreateOrderModel = model;
                service.Add();
            }

            using (var context = new RecommendationDbContext(_contextOption))
            {
                var hasOrder = context.Orders.Any(p => p.Id == model.OrderId);
                Assert.True(hasOrder);
            }
        }

        [Fact]
        public void Add_InvalidModel_ReturnEmpty()
        {
            OrderModel model = new OrderModel
            {
                OrderId = 1,
                UserId = "user-1"
            };

            using (var context = new RecommendationDbContext(_contextOption))
            {
                var service = new SOrder(context, mapper);
                service.CreateOrderModel = model;
                service.Add();
            }

            using (var context = new RecommendationDbContext(_contextOption))
            {
                var hasOrderItem = context.OrderItems.Any(p => p.Id == model.OrderId);
                Assert.False(hasOrderItem);
            }
        }

    }
}
