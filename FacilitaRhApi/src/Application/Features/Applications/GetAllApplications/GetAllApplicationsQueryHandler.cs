using FacilitaRhApi.Application.Abstractions.Messaging;
using FacilitaRhApi.Application.Repositories;
using FacilitaRhApi.Domain.Abstractions;

namespace FacilitaRhApi.Application.Features.Applications.GetAllApplications;

public sealed class GetAllApplicationsQueryHandler
    : IQueryHandler<GetAllApplicationsQuery, IEnumerable<Domain.Models.Application>>
{
    private readonly IApplicationRepository _applicationRepository;

    public GetAllApplicationsQueryHandler(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    public async Task<Result<IEnumerable<Domain.Models.Application>>> Handle(
        GetAllApplicationsQuery request,
        CancellationToken cancellationToken)
    {
        var applications = await _applicationRepository.GetAllAsync();
        if (!applications.Any())
            return ApplicationErrors.NoneFound;

        return Result<IEnumerable<Domain.Models.Application>>.Success(applications);
    }
}
