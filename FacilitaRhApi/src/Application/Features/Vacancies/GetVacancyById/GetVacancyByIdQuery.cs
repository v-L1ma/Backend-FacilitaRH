using FacilitaRhApi.Application.Abstractions.Messaging;
using FacilitaRhApi.Domain.Models;

namespace FacilitaRhApi.Application.Features.Vacancies.GetVacancyById;

public record GetVacancyByIdQuery(int Id) : IQuery<Vacancy>;
