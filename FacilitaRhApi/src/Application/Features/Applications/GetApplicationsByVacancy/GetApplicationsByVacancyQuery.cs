using FacilitaRhApi.Application.Abstractions.Messaging;

namespace FacilitaRhApi.Application.Features.Applications.GetApplicationsByVacancy;

public record GetApplicationsByVacancyQuery(int VacancyId) : IQuery<IEnumerable<Domain.Models.Application>>;
