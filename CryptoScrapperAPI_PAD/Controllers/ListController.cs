using CryptoScrapperAPI_PAD.Features.Lists;
using CryptoScrapperAPI_PAD.Features.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoScrapperAPI_PAD.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ListController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region List Methods
        [HttpPost]
        [ProducesResponseType(typeof(CreateListCommands.CreateListCommandResponse), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<JsonResult> CreateList([FromBody] CreateListCommands.CreateListCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return new JsonResult(result);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(DeleteListCommands.DeleteListCommandResponse), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<JsonResult> DeleteList([FromQuery] DeleteListCommands.DeleteListCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return new JsonResult(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetListsQuery.GetListQueryResponse), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<JsonResult> GetList([FromQuery] GetListsQuery.GetListQuery cmd)
        {
            var result = await _mediator.Send(cmd);
            return new JsonResult(result);
        }
        #endregion

        #region List Items Methods
        [HttpPut]
        [ProducesResponseType(typeof(AddItemsToListCommands.AddItemsToListCommandResponse), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<JsonResult> AddItemsToList([FromBody] AddItemsToListCommands.AddItemsToListCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return new JsonResult(result);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(RemoveItemsToListCommands.RemoveItemsToListCommandResponse), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<JsonResult> DeleteItemToList([FromQuery] RemoveItemsToListCommands.RemoveItemsToListCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return new JsonResult(result);
        }

        #endregion
    }
}
