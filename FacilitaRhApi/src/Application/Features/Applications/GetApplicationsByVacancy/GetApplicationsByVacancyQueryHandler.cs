using FacilitaRhApi.Application.Abstractions.Messaging;
using FacilitaRhApi.Application.Repositories;
using FacilitaRhApi.Domain.Abstractions;

namespace FacilitaRhApi.Application.Features.Applications.GetApplicationsByVacancy;

public sealed class GetApplicationsByVacancyQueryHandler
    : IQueryHandler<GetApplicationsByVacancyQuery, IEnumerable<Domain.Models.Application>>
{
    private readonly IApplicationRepository _applicationRepository;

    public GetApplicationsByVacancyQueryHandler(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    public async Task<Result<IEnumerable<Domain.Models.Application>>> Handle(
        GetApplicationsByVacancyQuery request,
        CancellationToken cancellationToken)
    {
        var applications = await _applicationRepository.GetByVacancyIdAsync(request.VacancyId);
        return Result<IEnumerable<Domain.Models.Application>>.Success(applications);
    }
}
