using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Recommendation.API.Interfaces;
using Recommendation.API.Models.DTOs;
using Recommendation.API.Validations;
using System.Numerics;

namespace Recommendation.API.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class ProductController : ControllerBase
    {
        public IProduct _product;
        public SProductValidator validator { get; set; }

        public ProductController(IProduct product)
        {
            _product = product;
            validator = new SProductValidator();
        }


        /// <summary>
        /// This is written for adding product to DB.
        /// It is used for ETL Process App
        /// </summary>
        /// <param name="productItem"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add(ProductModel productItem)
        {
            _product.CreateProductModel = productItem;
            validator.ValidateAndThrow(_product);
            _product.Add();
            return NoContent();

        }
        /// <summary>
        /// It can understand the interest of a user using his/her browsing history items 
        /// and recommend best seller products to him/her only from the categories of these items. 
        /// Otherwise, it returns a general best seller product list without any filter. 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("BestSellerProducts")]
        public IActionResult Get(string userId)
        {
            _product.Userid = userId;
            validator.ValidateAndThrow(_product);
            var result = _product.GetBestSellerProducts();
            return Ok(result);
        }
    }
}
