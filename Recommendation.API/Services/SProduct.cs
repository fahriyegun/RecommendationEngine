using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Recommendation.API.DbContexts;
using Recommendation.API.Enums;
using Recommendation.API.Interfaces;
using Recommendation.API.Models.DTOs;
using Recommendation.API.Models.Entities;
using System.Linq;

namespace Recommendation.API.Services
{
    public class SProduct : IProduct
    {
        public IRecommendationDbContext _context;
        private readonly IMapper _mapper;
        public ProductModel CreateProductModel { get; set; }
        public string Userid { get; set; }

        public SProduct(IRecommendationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Add()
        {
            var item = _context.Products.FirstOrDefault(p => p.Id == CreateProductModel.productId);

            if (item == null)
            {
                Products product = _mapper.Map<Products>(CreateProductModel);
                _context.Products.Add(product);
                _context.SaveChanges();
            }
        }

        public HistoryModel GetBestSellerProducts()
        {
            HistoryModel historyModel = new HistoryModel();
            List<MostDistinctUsers> bestseller = new List<MostDistinctUsers>();

            //Use the browsing history of the user
            var historyDetails = _context.Histories.Where(x => x.Userid.Equals(Userid)).Include(x => x.Product).ToList();

            var mostDistinctUsers = getMostDistinctUsers();


            if (historyDetails.Any())
            {
                //Extract at most three categories
                var distinctCategories = historyDetails.Select(x => new { x.Product.CategoryId }).GroupBy(x => x.CategoryId).OrderByDescending(g => g.Distinct().Count()).Take(3).ToList();

                //Bestsellers of a category means top ten products of this category bought (last month) by the most distinct users 
                bestseller = mostDistinctUsers.Where(w => distinctCategories.Select(s => s.Key).Contains(w.CategoryId)).OrderByDescending(x => x.UserCount).ToList();

                if (bestseller != null && bestseller.Count >= 5)
                {
                    bestseller = bestseller.Take(10).ToList();
                    historyModel = getHistoryModel(bestseller, Userid, RecommendationTypes.Personalized);
                }
                else
                {
                    historyModel = getHistoryModel(bestseller, Userid, RecommendationTypes.NonPersonalized);
                }
            }
            else
            {
                bestseller = mostDistinctUsers.Take(10).ToList();
                historyModel = getHistoryModel(bestseller, Userid, RecommendationTypes.NonPersonalized);
            }

            return historyModel;
        }

        private List<MostDistinctUsers> getMostDistinctUsers()
        {
            //the most distinct users
            List<MostDistinctUsers> mostDistinctUsers =
                (from o in _context.Orders.ToList()
                 join oi in _context.OrderItems on o.Id equals oi.OrderId
                 join p in _context.Products on oi.ProductId equals p.Id
                 group o by new { oi.ProductId, p.CategoryId } into stdGroup
                 select new MostDistinctUsers
                 {
                     ProductId = stdGroup.Key.ProductId,
                     CategoryId = stdGroup.Key.CategoryId,
                     UserCount = stdGroup.DistinctBy(o => o.UserId).Count()
                 }).ToList();

            return mostDistinctUsers;
        }

        private HistoryModel getHistoryModel(List<MostDistinctUsers> bestsellerProducts, string userId, RecommendationTypes recommendationType)
        {
            HistoryModel historyModel = new HistoryModel();
            historyModel.UserId = userId;
            historyModel.RecommendationType = recommendationType.ToString();
            historyModel.Products = new List<string>();

            if (bestsellerProducts != null && bestsellerProducts.Count >= 5)
            {
                foreach (var item in bestsellerProducts)
                {
                    historyModel.Products.Add(item.ProductId);
                }

            }


            return historyModel;
        }
    }
}
