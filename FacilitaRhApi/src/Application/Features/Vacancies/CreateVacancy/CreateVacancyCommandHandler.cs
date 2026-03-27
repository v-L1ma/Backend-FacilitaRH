using FacilitaRhApi.Application.Abstractions.Messaging;
using FacilitaRhApi.Application.Repositories;
using FacilitaRhApi.Domain.Abstractions;
using FacilitaRhApi.Domain.Models;

namespace FacilitaRhApi.Application.Features.Vacancies.CreateVacancy;

public sealed class CreateVacancyCommandHandler : ICommandHandler<CreateVacancyCommand, int>
{
    private readonly IVacancyRepository _vacancyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateVacancyCommandHandler(IVacancyRepository vacancyRepository, IUnitOfWork unitOfWork)
    {
        _vacancyRepository = vacancyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateVacancyCommand request, CancellationToken cancellationToken)
    {
        var vacancy = new Vacancy
        {
            Status = request.Status,
            Titulo = request.Titulo,
            QtdeVagas = request.QtdeVagas,
            Descricao = request.Descricao,
            Setor = request.Setor,
            Senioridade = request.Senioridade,
            Diversidade = request.Diversidade,
            Pcd = request.Pcd,
            Salario = request.Salario,
            Contrato = request.Contrato,
            Turno = request.Turno,
            Local = request.Local,
            Endereco = request.Endereco,
            DataAbertura = request.DataAbertura,
            DataFechamento = request.DataFechamento
        };

        await _vacancyRepository.AddAsync(vacancy);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return vacancy.Id;
    }
}
