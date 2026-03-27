using FluentValidation;

namespace FacilitaRhApi.Application.Features.Applications.CreateApplication;

public class CreateApplicationCommandValidator : AbstractValidator<CreateApplicationCommand>
{
    public CreateApplicationCommandValidator()
    {
        RuleFor(x => x.VacancyId).GreaterThan(0);
        RuleFor(x => x.NomeCompleto).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Telefone).NotEmpty();
        RuleFor(x => x.DataNasc).NotEmpty();
        RuleFor(x => x.Cpf).NotEmpty();
        RuleFor(x => x.ResumoProfissional).NotEmpty();
        RuleFor(x => x.Cargo).NotEmpty();
        RuleFor(x => x.Empresa).NotEmpty();
        RuleFor(x => x.DataInicioEmpresa).NotEmpty();
        RuleFor(x => x.DataTerminoEmpresa).NotEmpty();
        RuleFor(x => x.DescricaoATVD).NotEmpty();
        RuleFor(x => x.Situacao).NotEmpty();
        RuleFor(x => x.Escolaridade).NotEmpty();
        RuleFor(x => x.Curso).NotEmpty();
        RuleFor(x => x.Instituicao).NotEmpty();
        RuleFor(x => x.DataInicioEstudo).NotEmpty();
        RuleFor(x => x.DataTerminoEstudos).NotEmpty();
    }
}
