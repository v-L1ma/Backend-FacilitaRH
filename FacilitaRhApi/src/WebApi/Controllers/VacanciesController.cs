using MediatR;
using Microsoft.AspNetCore.Mvc;
using FacilitaRhApi.Application.Features.Vacancies.CreateVacancy;
using FacilitaRhApi.Application.Features.Vacancies.GetVacancies;
using FacilitaRhApi.Application.Features.Vacancies.GetVacancyById;
using FacilitaRhApi.Application.Features.Vacancies.UpdateVacancy;
using FacilitaRhApi.Application.Features.Vacancies.DeleteVacancy;

namespace FacilitaRhApi.WebApi.Controllers;

[Route("vacancies")]
public class VacanciesController : ApiControllerBase
{
    private readonly ISender _sender;

    public VacanciesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVacancyCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return HandleFailure(result);

        return Ok(new { vacancy = new { id = result.Value } });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _sender.Send(new GetVacanciesQuery());
        if (!result.IsSuccess)
            return HandleFailure(result);

        return Ok(new { vacancies = result.Value });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sender.Send(new GetVacancyByIdQuery(id));
        if (!result.IsSuccess)
            return HandleFailure(result);

        return Ok(new { vacancy = result.Value });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateVacancyCommand command)
    {
        if (id != command.Id)
            return BadRequest("Route ID does not match body ID.");

        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return HandleFailure(result);

        return Ok(new { vacancy = command });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sender.Send(new DeleteVacancyCommand(id));
        if (!result.IsSuccess)
            return HandleFailure(result);

        return Ok(new { msg = "Vacancy has been deleted" });
    }
}
