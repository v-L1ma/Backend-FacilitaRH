using FacilitaRhApi.Application.Abstractions.Messaging;
using FacilitaRhApi.Application.Repositories;
using FacilitaRhApi.Domain.Abstractions;
using FacilitaRhApi.Domain.Models;

namespace FacilitaRhApi.Application.Features.Vacancies.GetVacancies;

public sealed class GetVacanciesQueryHandler : IQueryHandler<GetVacanciesQuery, IEnumerable<Vacancy>>
{
    private readonly IVacancyRepository _vacancyRepository;

    public GetVacanciesQueryHandler(IVacancyRepository vacancyRepository)
    {
        _vacancyRepository = vacancyRepository;
    }

    public async Task<Result<IEnumerable<Vacancy>>> Handle(
        GetVacanciesQuery request,
        CancellationToken cancellationToken)
    {
        var vacancies = await _vacancyRepository.GetAllAsync();
        return Result<IEnumerable<Vacancy>>.Success(vacancies);
    }
}
