using FacilitaRhApi.Application.Abstractions.Messaging;
using FacilitaRhApi.Application.Repositories;
using FacilitaRhApi.Domain.Abstractions;
using FacilitaRhApi.Domain.Models;

namespace FacilitaRhApi.Application.Features.Vacancies.GetVacancyById;

public sealed class GetVacancyByIdQueryHandler : IQueryHandler<GetVacancyByIdQuery, Vacancy>
{
    private readonly IVacancyRepository _vacancyRepository;

    public GetVacancyByIdQueryHandler(IVacancyRepository vacancyRepository)
    {
        _vacancyRepository = vacancyRepository;
    }

    public async Task<Result<Vacancy>> Handle(GetVacancyByIdQuery request, CancellationToken cancellationToken)
    {
        var vacancy = await _vacancyRepository.GetByIdAsync(request.Id);
        if (vacancy is null)
            return VacancyErrors.NotFound;

        return vacancy;
    }
}
