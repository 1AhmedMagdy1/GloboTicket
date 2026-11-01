using GloboTicket.Application.Contracts.Identity;
using GloboTicket.Application.DTOS.Categories;
using GloboTicket.Application.Features.EventCategory.CreateCategory;
using GloboTicket.Application.Features.EventCategory.DeleteCategory;
using GloboTicket.Application.Features.EventCategory.GetCategories;
using GloboTicket.Application.Features.EventCategory.UpdateCategories;
using GloboTicket.Infrastructure.Persistence.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [HttpPut("{id:int}")]
        [Authorize(Roles =Roles.SystemAdmin+","+Roles.TenantAdmin)]
        public async Task<IActionResult>UpdateCategory([FromRoute]int ID,[FromBody] UpdateCategoryDTO updateCategoryDTO, CancellationToken token)
        {
            var command = new UpdateCategoryCommand
            {
                ID = ID,
                Name = updateCategoryDTO.Name,
                Description = updateCategoryDTO.Description,
                LogoID = updateCategoryDTO.LogoID
            };

            var result = (await Sender.Send(command,token)).ToOperationResult();

            return StatusCode(result.StatusCode, result);
        }
        [HttpPost]
        [Authorize(Roles =Roles.SystemAdmin+","+Roles.TenantAdmin)]
        public async Task<IActionResult>CreateCategory([FromBody] CreateCategoryCommand command, CancellationToken token)
        {
            var result = (await Sender.Send(command, token)).ToOperationResult();
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles =Roles.SystemAdmin+","+Roles.TenantAdmin)]
        public async Task<IActionResult> DeleteCategory([FromRoute] int ID, CancellationToken token)
        {
            var command = new DeleteCategoryCommand { ID = ID };
            var result = (await Sender.Send(command, token)).ToOperationResult();
            return StatusCode(result.StatusCode, result);
        }
    }
}
