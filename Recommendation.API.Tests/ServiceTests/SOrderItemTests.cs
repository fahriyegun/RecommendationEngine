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
    public class SOrderItemTests : DummyDbGenerator
    {
        private readonly IMapper mapper;

        public SOrderItemTests()
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
            OrderItemModel model = new OrderItemModel
            {
                Id = 100,
                OrderId = 1,
                ProductId= "product-1",
                Quantity = 5
            };

            using (var context = new RecommendationDbContext(_contextOption))
            {
                var service = new SOrderItem(context, mapper);
                service.CreateOrderItemModel = model;
                service.Add();
            }

            using (var context = new RecommendationDbContext(_contextOption))
            {
                var hasItem = context.OrderItems.Any(p => p.Id == model.Id);
                Assert.True(hasItem);
            }
        }

        [Fact]
        public void Add_InvalidModel_ReturnEmpty()
        {
            OrderItemModel model = new OrderItemModel
            {
                Id = 100,
                OrderId = 15,
                ProductId = "product-1",
                Quantity = 5
            };

            using (var context = new RecommendationDbContext(_contextOption))
            {
                var service = new SOrderItem(context, mapper);
                service.CreateOrderItemModel = model;
                service.Add();
            }

            using (var context = new RecommendationDbContext(_contextOption))
            {
                var hasOrderItem = context.OrderItems.Any(p => p.Id == model.Id);
                Assert.False(hasOrderItem);
            }
        }
    }
}
