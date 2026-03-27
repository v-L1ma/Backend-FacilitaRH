using FacilitaRhApi.Application.Abstractions.Messaging;

namespace FacilitaRhApi.Application.Features.Vacancies.DeleteVacancy;

public record DeleteVacancyCommand(int Id) : ICommand;
