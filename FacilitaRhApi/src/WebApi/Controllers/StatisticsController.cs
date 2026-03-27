using MediatR;
using Microsoft.AspNetCore.Mvc;
using FacilitaRhApi.Application.Features.Statistics.GetStatistics;

namespace FacilitaRhApi.WebApi.Controllers;

[Route("statistics")]
public class StatisticsController : ApiControllerBase
{
    private readonly ISender _sender;

    public StatisticsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _sender.Send(new GetStatisticsQuery());
        if (!result.IsSuccess)
            return HandleFailure(result);

        return Ok(result.Value);
    }
}
