using Recommendation.API.Enums;
using Recommendation.API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommendation.API.Tests
{
    public class DummyDataGenerator
    {
        public static HistoryModel historyModel_HaveBrowsingHistory = new HistoryModel
        {
            UserId = "user-1",
            Products = new List<string> { "product-1", "product-2", "product-3", "product-4", "product-5",
                "product-6", "product-7", "product-8", "product-9", "product-10" },
            RecommendationType = RecommendationTypes.Personalized.ToString()
        };

        public static HistoryModel historyModel_NotHaveBrowsingHistory = new HistoryModel
        {
            UserId = "user-2",
            Products = new List<string> (),
            RecommendationType = RecommendationTypes.NonPersonalized.ToString()
        };
    }
}
