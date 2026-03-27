using FacilitaRhApi.Application.Abstractions.Messaging;

namespace FacilitaRhApi.Application.Features.Vacancies.UpdateVacancy;

public record UpdateVacancyCommand(
    int Id,
    string Status,
    string Titulo,
    int QtdeVagas,
    string Descricao,
    string Setor,
    string Senioridade,
    string Diversidade,
    string Pcd,
    string Salario,
    string Contrato,
    string Turno,
    string Local,
    string Endereco,
    string DataAbertura,
    string DataFechamento) : ICommand;
