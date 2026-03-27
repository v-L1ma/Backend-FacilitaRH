using FacilitaRhApi.Application.Abstractions.Messaging;

namespace FacilitaRhApi.Application.Features.Applications.CreateApplication;

public record CreateApplicationCommand(
    int VacancyId,
    string NomeCompleto,
    string Email,
    string Telefone,
    string DataNasc,
    string Cpf,
    string ResumoProfissional,
    string Cargo,
    string Empresa,
    string DataInicioEmpresa,
    string DataTerminoEmpresa,
    string DescricaoATVD,
    string Situacao,
    string Escolaridade,
    string Curso,
    string Instituicao,
    string DataInicioEstudo,
    string DataTerminoEstudos) : ICommand<int>;
