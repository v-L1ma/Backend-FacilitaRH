using FacilitaRhApi.Application.Abstractions.Messaging;
using FacilitaRhApi.Domain.Models;

namespace FacilitaRhApi.Application.Features.Vacancies.GetVacancies;

public record GetVacanciesQuery() : IQuery<IEnumerable<Vacancy>>;
