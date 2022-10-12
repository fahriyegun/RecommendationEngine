using AutoMapper;
using Recommendation.API.DbContexts;
using Recommendation.API.Interfaces;
using Recommendation.API.Models.DTOs;
using Recommendation.API.Models.Entities;
using System.Collections.Generic;

namespace Recommendation.API.Services
{
    public class SHistory : IHistory
    {
        public IRecommendationDbContext _context;
        private readonly IMapper _mapper;
        public string UserId { get; set; }
        public CreateHistoryModel CreateHistoryModel { get; set; }
        public DeleteHistoryModel DeleteHistoryModel { get; set; }

        public SHistory(IRecommendationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Add()
        {
            var item = _context.Histories.FirstOrDefault(p => p.Messageid.Equals(CreateHistoryModel.messageid));
            var product = _context.Products.FirstOrDefault(p => p.Id.Equals(CreateHistoryModel.properties.productid));

            if (product == null)
                throw new Exception("Product not found");

            if (item == null && product != null)
            {
                History history = _mapper.Map<History>(CreateHistoryModel);
                _context.Histories.Add(history);
                _context.SaveChanges();

            }
        }

        public void Delete()
        {
            var historyDetail = _context.Histories.Where(x => x.Productid.Equals(DeleteHistoryModel.productid) && x.Productid.Equals(x.Productid)).FirstOrDefault();

            if (historyDetail != null)
            {
                _context.Histories.Remove(historyDetail);
                _context.SaveChanges();
            }
            else
                throw new Exception("History not found");
        }

        public HistoryModel GetBrowsingHistory()
        {
            HistoryModel history = new HistoryModel();
            history.UserId = UserId;
            history.Products = new List<string>();

            var historyDetails = _context.Histories.Where(x => x.Userid.Equals(UserId)).OrderByDescending(x=>x.ViewedDate).ToList();


            if (historyDetails != null && historyDetails.Count >= 5)
            {                
                history.RecommendationType = Enums.RecommendationTypes.Personalized.ToString();

                historyDetails.ForEach(detail =>
                {
                    history.Products.Add(detail.Productid);
                });
            }
            else
            {
                history.RecommendationType = Enums.RecommendationTypes.NonPersonalized.ToString();
            }

            return history;
        }
    }
}
