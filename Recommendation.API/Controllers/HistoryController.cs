using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Recommendation.API.Interfaces;
using Recommendation.API.Models.DTOs;
using Recommendation.API.Validations;
using System.ComponentModel.DataAnnotations;

namespace Recommendation.API.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class HistoryController : ControllerBase
    {
        public IHistory _history;
        public SHistoryValidator validator { get; set; }

        public HistoryController(IHistory history)
        {
            _history = history;
            validator = new SHistoryValidator();
        }


        /// <summary>
        /// This returns the last ten products viewed by a given user and sorted by view date
        /// If user has no browsing history, it returns empty product list 
        /// </summary> 
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("BrowsingHistory")]
        public IActionResult Get(string UserId)
        {
            _history.UserId = UserId;
            validator.ValidateAndThrow(_history);
            var historyDetails = _history.GetBrowsingHistory();
            return Ok(historyDetails);
            
        }

        /// <summary>
        /// This is written for adding product viewed by users to DB.
        /// It is used for RabbitMq
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add(CreateHistoryModel model)
        {
            _history.CreateHistoryModel = model;
            validator.ValidateAndThrow(_history);
            _history.Add();
            return NoContent();
        }


        /// <summary>
        /// It can delete a product from specific user's history
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(DeleteHistoryModel model)
        {
            _history.DeleteHistoryModel = model;

            validator.ValidateAndThrow(_history);
            _history.Delete();
            return NoContent();
        }
    }
}
