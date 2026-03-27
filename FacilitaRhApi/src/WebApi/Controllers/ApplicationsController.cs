using MediatR;
using Microsoft.AspNetCore.Mvc;
using FacilitaRhApi.Application.Features.Applications.CreateApplication;
using FacilitaRhApi.Application.Features.Applications.GetApplicationsByVacancy;
using FacilitaRhApi.Application.Features.Applications.GetAllApplications;

namespace FacilitaRhApi.WebApi.Controllers;

[Route("applications")]
public class ApplicationsController : ApiControllerBase
{
    private readonly ISender _sender;

    public ApplicationsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _sender.Send(new GetAllApplicationsQuery());
        if (!result.IsSuccess)
            return HandleFailure(result);

        return Ok(new { applications = result.Value });
    }

    [HttpPost("{vacancyId:int}")]
    public async Task<IActionResult> Apply(int vacancyId, [FromBody] CreateApplicationCommand command)
    {
        if (vacancyId != command.VacancyId)
            return BadRequest("Route vacancyId does not match body VacancyId.");

        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return HandleFailure(result);

        return Ok(new { application = new { id = result.Value } });
    }

    [HttpGet("{vacancyId:int}")]
    public async Task<IActionResult> GetByVacancy(int vacancyId)
    {
        var result = await _sender.Send(new GetApplicationsByVacancyQuery(vacancyId));
        if (!result.IsSuccess)
            return HandleFailure(result);

        return Ok(new { applications = result.Value });
    }
}
