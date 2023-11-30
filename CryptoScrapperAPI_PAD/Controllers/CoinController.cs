using CryptoScrapperAPI_PAD.Features.Coins;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoScrapperAPI_PAD.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CoinController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CoinController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ProducesResponseType(typeof(GetCoinQuery.GetCoinQueryResponse), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<JsonResult> GetCoins([FromQuery] GetCoinQuery.GetCoinsQuery cmd)
        {
            var result = await _mediator.Send(cmd);
            return new JsonResult(result);
        }
    }
}
