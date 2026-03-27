using FluentValidation;

namespace FacilitaRhApi.Application.Features.Vacancies.UpdateVacancy;

public class UpdateVacancyCommandValidator : AbstractValidator<UpdateVacancyCommand>
{
    public UpdateVacancyCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Status).NotEmpty();
        RuleFor(x => x.Titulo).NotEmpty();
        RuleFor(x => x.QtdeVagas).GreaterThan(0);
        RuleFor(x => x.Descricao).NotEmpty();
        RuleFor(x => x.Setor).NotEmpty();
        RuleFor(x => x.Senioridade).NotEmpty();
        RuleFor(x => x.Diversidade).NotEmpty();
        RuleFor(x => x.Pcd).NotEmpty();
        RuleFor(x => x.Salario).NotEmpty();
        RuleFor(x => x.Contrato).NotEmpty();
        RuleFor(x => x.Turno).NotEmpty();
        RuleFor(x => x.Local).NotEmpty();
        RuleFor(x => x.Endereco).NotEmpty();
        RuleFor(x => x.DataAbertura).NotEmpty();
        RuleFor(x => x.DataFechamento).NotEmpty();
    }
}
