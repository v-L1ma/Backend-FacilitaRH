using FacilitaRhApi.Application.Abstractions.Messaging;

namespace FacilitaRhApi.Application.Features.Applications.GetAllApplications;

public record GetAllApplicationsQuery() : IQuery<IEnumerable<Domain.Models.Application>>;
