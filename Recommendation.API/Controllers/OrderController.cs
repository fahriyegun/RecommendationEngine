using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Recommendation.API.Interfaces;
using Recommendation.API.Models.DTOs;
using Recommendation.API.Validations;

namespace Recommendation.API.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class OrderController : ControllerBase
    {
        public IOrder _order;
        public SOrderValidator validator { get; set; }

        public OrderController(IOrder order)
        {
            _order = order;
            validator = new SOrderValidator();

        }

        /// <summary>
        /// This is written for adding Order to DB.
        /// It is used for ETL Process App
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add(OrderModel model)
        {
            _order.CreateOrderModel = model;
            validator.ValidateAndThrow(_order);
            _order.Add();
            return NoContent();
        }
    }
}
