using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Recommendation.API.DbContexts;
using Recommendation.API.Enums;
using Recommendation.API.Mappings;
using Recommendation.API.Models.DTOs;
using Recommendation.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Recommendation.API.Tests.ServiceTests
{
    public class SHistoryTests: DummyDbGenerator
    {
        private readonly IMapper mapper;

        public SHistoryTests()
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
            CreateHistoryModel model = new CreateHistoryModel
            {
               messageid = "msg1000",
               userid = "user-1",
               properties = new Properties { productid = "product-1" }
            };

            using (var context = new RecommendationDbContext(_contextOption))
            {
                var service = new SHistory(context, mapper);
                service.CreateHistoryModel = model;
                service.Add();
            }

            using (var context = new RecommendationDbContext(_contextOption))
            {
                var hasHistory = context.Histories.Any(p => p.Messageid.Equals(model.messageid));
                Assert.True(hasHistory);
            }
        }

        [Fact]
        public void Delete_ValidModel_ReturnSuccess()
        {
            int totalCount = 0;
            DeleteHistoryModel model = new DeleteHistoryModel
            {
                productid = "product-1",
                userid = "user1"
            };

            using (var context = new RecommendationDbContext(_contextOption))
            {
                totalCount = context.Histories.Count();
                var service = new SHistory(context, mapper);
                service.DeleteHistoryModel = model;
                service.Delete();
            }

            using (var context = new RecommendationDbContext(_contextOption))
            {
                int currentCount = context.Histories.Count();
                var hasHistory = context.Histories.Any(p => p.Productid.Equals(model.productid) && p.Userid.Equals(model.userid));
                Assert.False(hasHistory);
                Assert.NotEqual(totalCount, currentCount);
            }
        }

        [Fact]
        public void Delete_InvalidModel_ReturnEmpty()
        {
            int totalCount = 0;
            DeleteHistoryModel model = new DeleteHistoryModel
            {
                productid = "prd111",
                userid = "user1"
            };

            using (var context = new RecommendationDbContext(_contextOption))
            {
                totalCount = context.Histories.Count();
                var service = new SHistory(context, mapper);
                service.DeleteHistoryModel = model;

                try
                {
                    service.Delete();
                }
                catch(Exception ex)
                {
                    Assert.Equal("History not found", ex.Message);
                }
               
            }

            using (var context = new RecommendationDbContext(_contextOption))
            {
                int currentCount = context.Histories.Count();
                Assert.Equal(totalCount, currentCount); 
            }
        }


        [Fact]
        public void GetBrowsinHistory_UserHasBrowsingHistoryLessThan5Record_ReturnEmptyList()
        {
           

            using (var context = new RecommendationDbContext(_contextOption))
            {
                var service = new SHistory(context, mapper);
                service.UserId = "user2";
                var result = service.GetBrowsingHistory();

                Assert.Equal(RecommendationTypes.NonPersonalized.ToString(), result.RecommendationType);

                Assert.True(result.Products.Count == 0);
            }

        }


        //[Fact]
        //public void GetBrowsinHistory_UserHasBrowsingHistoryMoreThan5Record_ReturnFullList()
        //{


        //    using (var context = new RecommendationDbContext(_contextOption))
        //    {
        //        var service = new SHistory(context, mapper);
        //        service.UserId = "user1";
        //        var result = service.GetBrowsingHistory();

        //        Assert.Equal(RecommendationTypes.Personalized.ToString(), result.RecommendationType);
        //        Assert.True(result.Products.Count >= 5);
        //        Assert.True(result.Products.Count <= 10);
        //    }

        //}
    }
}
