using FacilitaRhApi.Application.Abstractions.Messaging;
using FacilitaRhApi.Application.Repositories;
using FacilitaRhApi.Domain.Abstractions;

namespace FacilitaRhApi.Application.Features.Vacancies.DeleteVacancy;

public sealed class DeleteVacancyCommandHandler : ICommandHandler<DeleteVacancyCommand>
{
    private readonly IVacancyRepository _vacancyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteVacancyCommandHandler(IVacancyRepository vacancyRepository, IUnitOfWork unitOfWork)
    {
        _vacancyRepository = vacancyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteVacancyCommand request, CancellationToken cancellationToken)
    {
        var vacancy = await _vacancyRepository.GetByIdAsync(request.Id);
        if (vacancy is null)
            return VacancyErrors.NotFound;

        await _vacancyRepository.DeleteAsync(vacancy);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
