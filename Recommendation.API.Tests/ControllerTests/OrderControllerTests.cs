using Microsoft.AspNetCore.Mvc;
using Moq;
using Recommendation.API.Controllers;
using Recommendation.API.Interfaces;
using Recommendation.API.Models.DTOs;
using Recommendation.API.Validations;
using Xunit;

namespace Recommendation.API.Tests.ControllerTests
{
    public class OrderControllerTests
    {

        private readonly Mock<IOrder> _mockRepo;
        private readonly OrderController _orderController;

        public OrderModel CreateOrderModel = new OrderModel
        {
            OrderId = 1,
            UserId = "user-1"
        };

        public OrderControllerTests()
        {
            _mockRepo = new Mock<IOrder>();
            _orderController = new OrderController(_mockRepo.Object);
        }

        [Fact]
        public void Add_ValidOrder_ReturnNoContentResult()
        {
            _mockRepo.Setup(x=> x.CreateOrderModel).Returns(CreateOrderModel);
            _mockRepo.Setup(x => x.Add());
            var result = _orderController.Add(CreateOrderModel);

            Assert.IsType<NoContentResult>(result);
            _mockRepo.Verify(x => x.Add(), Times.Once);
        }
    }


}
