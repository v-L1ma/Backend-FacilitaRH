using FacilitaRhApi.Application.Abstractions.Messaging;
using FacilitaRhApi.Application.Repositories;
using FacilitaRhApi.Domain.Abstractions;

namespace FacilitaRhApi.Application.Features.Vacancies.UpdateVacancy;

public sealed class UpdateVacancyCommandHandler : ICommandHandler<UpdateVacancyCommand>
{
    private readonly IVacancyRepository _vacancyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateVacancyCommandHandler(IVacancyRepository vacancyRepository, IUnitOfWork unitOfWork)
    {
        _vacancyRepository = vacancyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateVacancyCommand request, CancellationToken cancellationToken)
    {
        var vacancy = await _vacancyRepository.GetByIdAsync(request.Id);
        if (vacancy is null)
            return VacancyErrors.NotFound;

        vacancy.Status = request.Status;
        vacancy.Titulo = request.Titulo;
        vacancy.QtdeVagas = request.QtdeVagas;
        vacancy.Descricao = request.Descricao;
        vacancy.Setor = request.Setor;
        vacancy.Senioridade = request.Senioridade;
        vacancy.Diversidade = request.Diversidade;
        vacancy.Pcd = request.Pcd;
        vacancy.Salario = request.Salario;
        vacancy.Contrato = request.Contrato;
        vacancy.Turno = request.Turno;
        vacancy.Local = request.Local;
        vacancy.Endereco = request.Endereco;
        vacancy.DataAbertura = request.DataAbertura;
        vacancy.DataFechamento = request.DataFechamento;

        await _vacancyRepository.UpdateAsync(vacancy);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
