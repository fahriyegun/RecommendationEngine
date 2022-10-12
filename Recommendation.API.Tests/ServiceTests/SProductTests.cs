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
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Recommendation.API.Tests.ServiceTests
{
    public class SProductTests : DummyDbGenerator
    {
        private readonly IMapper mapper;

        public SProductTests()
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
            int totalCount = 0;
            ProductModel model = new ProductModel
            {
               productId = "product-1000",
               categoryId = "category-1"
            };

            using (var context = new RecommendationDbContext(_contextOption))
            {
                totalCount = context.Products.Count();
                var service = new SProduct(context, mapper);
                service.CreateProductModel = model;
                service.Add();
            }

            using (var context = new RecommendationDbContext(_contextOption))
            {
                int currentCount = context.Products.Count();
                var hasProduct = context.Products.Any(p => p.Id == model.productId);
                Assert.True(hasProduct);
                Assert.Equal(currentCount, totalCount+1);
            }
        }

        [Fact]
        public void Add_InvalidModel_ReturnEmpty()
        {
            int totalCount = 0;
            ProductModel model = new ProductModel
            {
                productId = "product-1",
                categoryId = "category-1"
            };


            using (var context = new RecommendationDbContext(_contextOption))
            {
                totalCount = context.Products.Count();
                var service = new SProduct(context, mapper);
                service.CreateProductModel = model;
                service.Add();
            }

            using (var context = new RecommendationDbContext(_contextOption))
            {
                int currentCount = context.Products.Count();    
                Assert.Equal(currentCount, totalCount);
            }
        }

        [Fact]
        public void GetBestSellerProducts_NotHaveBrowsingHistory_ReturnNonpersonalizedList()
        {
            

            using (var context = new RecommendationDbContext(_contextOption))
            {
                var service = new SProduct(context, mapper);
                service.Userid = "user1000";
                var result = service.GetBestSellerProducts();

                Assert.NotNull(result);
                Assert.Equal(RecommendationTypes.NonPersonalized.ToString(), result.RecommendationType);
            }

          
        }

        //[Fact]
        //public void GetBestSellerProducts_HaveBrowsingHistory_ReturnPersonalizedList()
        //{


        //    using (var context = new RecommendationDbContext(_contextOption))
        //    {
        //        var service = new SProduct(context, mapper);
        //        service.Userid = "user1";
        //        var result = service.GetBestSellerProducts();

        //        Assert.NotNull(result);
        //        Assert.Equal(RecommendationTypes.Personalized.ToString(), result.RecommendationType);
        //    }


        //}

    }
}
