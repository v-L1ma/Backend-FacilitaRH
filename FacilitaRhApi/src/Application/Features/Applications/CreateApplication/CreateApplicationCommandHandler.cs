using FacilitaRhApi.Application.Abstractions.Messaging;
using FacilitaRhApi.Application.Repositories;
using FacilitaRhApi.Domain.Abstractions;

namespace FacilitaRhApi.Application.Features.Applications.CreateApplication;

public sealed class CreateApplicationCommandHandler : ICommandHandler<CreateApplicationCommand, int>
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateApplicationCommandHandler(
        IApplicationRepository applicationRepository,
        IUnitOfWork unitOfWork)
    {
        _applicationRepository = applicationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
    {
        var existing = await _applicationRepository.GetByEmailOrCpfAsync(request.Email, request.Cpf);
        if (existing is not null)
            return ApplicationErrors.AlreadyApplied;

        var application = new Domain.Models.Application
        {
            VacancyId = request.VacancyId,
            NomeCompleto = request.NomeCompleto,
            Email = request.Email,
            Telefone = request.Telefone,
            DataNasc = request.DataNasc,
            Cpf = request.Cpf,
            ResumoProfissional = request.ResumoProfissional,
            Cargo = request.Cargo,
            Empresa = request.Empresa,
            DataInicioEmpresa = request.DataInicioEmpresa,
            DataTerminoEmpresa = request.DataTerminoEmpresa,
            DescricaoATVD = request.DescricaoATVD,
            Situacao = request.Situacao,
            Escolaridade = request.Escolaridade,
            Curso = request.Curso,
            Instituicao = request.Instituicao,
            DataInicioEstudo = request.DataInicioEstudo,
            DataTerminoEstudos = request.DataTerminoEstudos
        };

        await _applicationRepository.AddAsync(application);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return application.Id;
    }
}
