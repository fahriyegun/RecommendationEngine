using FluentValidation;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Recommendation.API.Controllers;
using Recommendation.API.Interfaces;
using Recommendation.API.Models.DTOs;
using Recommendation.API.Services;
using Recommendation.API.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Recommendation.API.Tests.ControllerTests
{
    public class OrderItemControllerTests
    {
        private readonly Mock<IOrderItem> _mockRepo;
        private readonly OrderItemController _orderItemController;

        private OrderItemModel Model = new OrderItemModel
        {
            Id = 1,
            OrderId = 2,
            ProductId = "product-1",
            Quantity = 5
        };

        public OrderItemControllerTests()
        {
            _mockRepo = new Mock<IOrderItem>();
            _orderItemController = new OrderItemController(_mockRepo.Object);

        }

        [Fact]
        public void Add_ValidOrderItem_ReturnNoContentResult()
        {
            _mockRepo.Setup(x=> x.CreateOrderItemModel).Returns(Model);
            _mockRepo.Setup(x => x.Add());
            var result = _orderItemController.Add(Model);

            Assert.IsType<NoContentResult>(result);
            _mockRepo.Verify(x => x.Add(), Times.Once);
        }

    }
}
