using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Recommendation.API.Interfaces;
using Recommendation.API.Models.DTOs;
using Recommendation.API.Validations;

namespace Recommendation.API.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class OrderItemController : ControllerBase
    {
        public IOrderItem _orderItem;
        public SOrderItemValidator validator { get; set; }
        public OrderItemController(IOrderItem orderItem)
        {
            _orderItem = orderItem;
            validator = new SOrderItemValidator();


        }

        /// <summary>
        /// This is written for adding Order Details to DB.
        /// It is used for ETL Process App
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add(OrderItemModel model)
        {
            _orderItem.CreateOrderItemModel = model;
            validator.ValidateAndThrow(_orderItem);
            _orderItem.Add();
            return NoContent();
        }
    }
}
