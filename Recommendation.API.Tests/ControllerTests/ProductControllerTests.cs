using Microsoft.AspNetCore.Mvc;
using Moq;
using Recommendation.API.Controllers;
using Recommendation.API.Interfaces;
using Recommendation.API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Recommendation.API.Tests.ControllerTests
{
    public class ProductControllerTests
    {
        private readonly Mock<IProduct> _mockRepo;
        private readonly ProductController _productController;

        public ProductModel model = new ProductModel
        {
            productId = "product-1",
            categoryId = "category-1"
        };

        public ProductControllerTests()
        {
            _mockRepo = new Mock<IProduct>();
            _productController = new ProductController(_mockRepo.Object);
        }

        [Fact]
        public void Add_ValidProduct_ReturnNoContentResult()
        {
            _mockRepo.Setup(x => x.CreateProductModel).Returns(model);
            _mockRepo.Setup(x => x.Add());
            var result = _productController.Add(model);

            Assert.IsType<NoContentResult>(result);
            _mockRepo.Verify(x => x.Add(), Times.Once);
        }
    }
}
