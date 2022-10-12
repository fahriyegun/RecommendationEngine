using Microsoft.EntityFrameworkCore;
using Recommendation.API.Models.Entities;

namespace Recommendation.API.Interfaces
{
    public interface IRecommendationDbContext
    {
        DbSet<Products> Products { get; set; }
        DbSet<Orders> Orders { get; set; }
        DbSet<OrderItems> OrderItems { get; set; }
        DbSet<History> Histories { get; set; }
        int SaveChanges();

    }
}
