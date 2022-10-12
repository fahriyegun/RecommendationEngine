using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using Recommendation.API.DbContexts;
using Recommendation.API.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommendation.API.Tests
{
    public class DummyDbGenerator
    {
        protected DbContextOptions<RecommendationDbContext> _contextOption { get; private set; }

        public void SetContextOptions(DbContextOptions<RecommendationDbContext> contextOption)
        {
            _contextOption = contextOption;
        }

        public void Seed()
        {
            using (var context = new RecommendationDbContext(_contextOption))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Products product1 = new Products { Id = "product-1", CategoryId = "category-1" };
                Products product2 = new Products { Id = "product-2", CategoryId = "category-2" };
                Products product3 = new Products { Id = "product-3", CategoryId = "category-3" };
                Products product4 = new Products { Id = "product-4", CategoryId = "category-4" };
                Products product5 = new Products { Id = "product-5", CategoryId = "category-5" };
                Products product6 = new Products { Id = "product-6", CategoryId = "category-6" };
                Products product7 = new Products { Id = "product-7", CategoryId = "category-7" };
                Products product8 = new Products { Id = "product-8", CategoryId = "category-8" };
                Products product9 = new Products { Id = "product-9", CategoryId = "category-9" };
                Products product10 = new Products { Id = "product-10", CategoryId = "category-10" };
                context.Products.AddRange(product1, product2, product3, product4, product5, product6, product7, product8, product9, product10);


                Orders order1 = new Orders { Id = 1, UserId = "user-1" };
                Orders order2 = new Orders { Id = 2, UserId = "user-1" };
                Orders order3 = new Orders { Id = 3, UserId = "user-1" };
                Orders order4 = new Orders { Id = 4, UserId = "user-1" };
                Orders order5 = new Orders { Id = 5, UserId = "user-1" };
                Orders order6 = new Orders { Id = 6, UserId = "user-2" };
                Orders order7 = new Orders { Id = 7, UserId = "user-2" };
                Orders order8 = new Orders { Id = 8, UserId = "user-2" };
                Orders order9 = new Orders { Id = 9, UserId = "user-2" };
                Orders order10 = new Orders { Id = 10, UserId = "user-3" };
                Orders order11 = new Orders { Id = 11, UserId = "user-3" };
                Orders order12 = new Orders { Id = 12, UserId = "user-3" };
                Orders order13 = new Orders { Id = 13, UserId = "user-3" };
                context.Orders.AddRange(order1, order2, order3, order4, order5, order6, order7, order8, order9, order10, order11, order12, order13);

                /*
                OrderItems orderItem1 = new OrderItems { Id = 1, Order = order1, Product = product1 };
                OrderItems orderItem2 = new OrderItems { Id = 2, Order = order2, Product = product2 };
                OrderItems orderItem3 = new OrderItems { Id = 3, Order = order3, Product = product3 };
                OrderItems orderItem4 = new OrderItems { Id = 4, Order = order4, Product = product4 };
                OrderItems orderItem5 = new OrderItems { Id = 5, Order = order5, Product = product10 };
                OrderItems orderItem6 = new OrderItems { Id = 6, Order = order6, Product = product2 };
                OrderItems orderItem7 = new OrderItems { Id = 7, Order = order7, Product = product3 };
                OrderItems orderItem8 = new OrderItems { Id = 8, Order = order8, Product = product4 };
                OrderItems orderItem9 = new OrderItems { Id = 9, Order = order9, Product = product5 };
                OrderItems orderItem10 = new OrderItems { Id = 10, Order = order10, Product = product6 };
                OrderItems orderItem11 = new OrderItems { Id = 11, Order = order11, Product = product7 };
                OrderItems orderItem12 = new OrderItems { Id = 10, Order = order12, Product = product8 };
                OrderItems orderItem13 = new OrderItems { Id = 11, Order = order13, Product = product9 };
                context.OrderItems.AddRange(orderItem1, orderItem2, orderItem3, orderItem4, orderItem5, orderItem6, orderItem7, orderItem8, orderItem9, orderItem10, orderItem11, orderItem12, orderItem13);
                */

                History history1 = new History { Messageid = "msg1", Product = product1, Userid = "user1", ViewedDate = DateTime.Now };
                History history2 = new History { Messageid = "msg2", Product = product2, Userid = "user1", ViewedDate = DateTime.Now };
                History history3 = new History { Messageid = "msg3", Product = product3, Userid = "user1", ViewedDate = DateTime.Now };
                History history4 = new History { Messageid = "msg4", Product = product4, Userid = "user1", ViewedDate = DateTime.Now };
                History history5= new History { Messageid = "msg5", Product = product5, Userid = "user1", ViewedDate = DateTime.Now };
                History history6 = new History { Messageid = "msg6", Product = product6, Userid = "user1", ViewedDate = DateTime.Now };
                History history7 = new History { Messageid = "msg7", Product = product7, Userid = "user1", ViewedDate = DateTime.Now };
                History history8 = new History { Messageid = "msg8", Product = product8, Userid = "user1", ViewedDate = DateTime.Now };
                History history9 = new History { Messageid = "msg9", Product = product9, Userid = "user1", ViewedDate = DateTime.Now };
                History history10 = new History { Messageid = "msg10", Product = product10, Userid = "user1", ViewedDate = DateTime.Now };
                History history11 = new History { Messageid = "msg11", Product = product1, Userid = "user2", ViewedDate = DateTime.Now };
                History history12 = new History { Messageid = "msg12", Product = product2, Userid = "user2", ViewedDate = DateTime.Now };
                History history13 = new History { Messageid = "msg13", Product = product3, Userid = "user2", ViewedDate = DateTime.Now };
                context.Histories.AddRange(history1, history2, history3, history4, history5, history6, history7, history8, history9, history10, history11, history12, history13);

                context.SaveChanges();
            }
        }
    }
}
