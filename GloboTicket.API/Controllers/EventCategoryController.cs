using GloboTicket.Application.Features.EventCategory.CreateCategory;
using GloboTicket.Application.Features.EventCategory.GetCategories;
using GloboTicket.Application.Features.EventCategory.UpdateCategories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GloboTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventCategoryController : ControllerBase
    {
        public ISender Sender { get; set; }

        public EventCategoryController(ISender sender)
        {
            this.Sender = sender;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] GetCategoriesQuery query,CancellationToken token)
        {
            var result = (await Sender.Send(query,token)).ToOperationResult();
            return StatusCode(result.StatusCode, result);
        }
        [HttpPut]
        public async Task<IActionResult>UpdateCategory([FromBody] UpdateCategoryCommand command, CancellationToken token)
        {
            var result = (await Sender.Send(command,token)).ToOperationResult();
            return StatusCode(result.StatusCode, result);
        }
        [HttpPost]
        public async Task<IActionResult>CreateCategory([FromBody] CreateCategoryCommand command, CancellationToken token)
        {
            var result = (await Sender.Send(command, token)).ToOperationResult();
            return StatusCode(result.StatusCode, result);
        }
    }
}
