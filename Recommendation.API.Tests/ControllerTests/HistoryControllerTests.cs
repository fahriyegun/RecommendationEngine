using Microsoft.AspNetCore.Mvc;
using Moq;
using Recommendation.API.Controllers;
using Recommendation.API.Enums;
using Recommendation.API.Interfaces;
using Recommendation.API.Models.DTOs;
using Recommendation.API.Validations;
using Xunit;

namespace Recommendation.API.Tests.ControllerTests
{
    public class HistoryControllerTests
    {
        private readonly Mock<IHistory> _mockRepo;
        private readonly HistoryController _historyController;
        private HistoryModel historyModel_HaveBrowsingHistory;
        private HistoryModel historyModel_NotHaveBrowsingHistory;

        public CreateHistoryModel createModel = new CreateHistoryModel
        {
            messageid = "messageid1",
            userid = "userId1",
            ViewedDate = DateTime.Now,
            properties = new Properties
            {
                productid = "productId1"
            }
        };

        public HistoryControllerTests()
        {
            _mockRepo = new Mock<IHistory>();
            _historyController = new HistoryController(_mockRepo.Object);
            historyModel_HaveBrowsingHistory = DummyDataGenerator.historyModel_HaveBrowsingHistory;
            historyModel_NotHaveBrowsingHistory = DummyDataGenerator.historyModel_NotHaveBrowsingHistory;
        }


        [Theory]
        [InlineData("user-1")]
        public void Get_ValidUserHasHistory_ReturnOkResultWithLastTenHistory(string userId)
        {
            _mockRepo.Setup(x => x.GetBrowsingHistory()).Returns(historyModel_HaveBrowsingHistory);
            _mockRepo.Setup(x => x.UserId).Returns(historyModel_HaveBrowsingHistory.UserId);
            var result = _historyController.Get(userId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var historyResult = Assert.IsAssignableFrom<HistoryModel>(okResult.Value);
            Assert.True(historyResult.Products.Count >= 5);
            Assert.True(historyResult.Products.Count <= 10);
            Assert.Equal(historyModel_HaveBrowsingHistory.UserId, historyResult.UserId);
            Assert.Equal(historyResult.RecommendationType, RecommendationTypes.Personalized.ToString());

        }

        [Theory]
        [InlineData("user-2")]
        public void Get_ValidUserHasHistory_ReturnOkResultWithEmptyHistory(string userId)
        {
            _mockRepo.Setup(x => x.GetBrowsingHistory()).Returns(historyModel_NotHaveBrowsingHistory);
            _mockRepo.Setup(x => x.UserId).Returns(historyModel_NotHaveBrowsingHistory.UserId);
            var result = _historyController.Get(userId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var historyResult = Assert.IsAssignableFrom<HistoryModel>(okResult.Value);
            Assert.True(historyResult.Products.Count == 0);
            Assert.Equal(userId, historyResult.UserId);
            Assert.Equal(historyResult.RecommendationType, RecommendationTypes.NonPersonalized.ToString());

        }

        [Fact]
        public void Add_ValidHistory_ReturnNoContentResult()
        {
            _mockRepo.Setup(x => x.CreateHistoryModel).Returns(createModel);
            _mockRepo.Setup(x => x.Add());
            var result = _historyController.Add(createModel);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete_ActionExecute_ReturnNoContentResult()
        {
            DeleteHistoryModel model = new DeleteHistoryModel { userid = "user-1", productid = "product-1" };
            _mockRepo.Setup(x => x.DeleteHistoryModel).Returns(model);
            _mockRepo.Setup(x => x.Delete());

            var noContentResult = _historyController.Delete(model);

            _mockRepo.Verify(x => x.Delete(), Times.Once);

            Assert.IsType<NoContentResult>(noContentResult);
        }

    }
}
