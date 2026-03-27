namespace FacilitaRhApi.Application.Repositories;

public interface IApplicationRepository : IRepositoryBase<Domain.Models.Application>
{
    Task<Domain.Models.Application?> GetByEmailOrCpfAsync(string email, string cpf);
    Task<IEnumerable<Domain.Models.Application>> GetByVacancyIdAsync(int vacancyId);
}
