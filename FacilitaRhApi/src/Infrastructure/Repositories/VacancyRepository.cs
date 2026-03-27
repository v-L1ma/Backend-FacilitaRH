using FacilitaRhApi.Application.Repositories;
using FacilitaRhApi.Domain.Models;

namespace FacilitaRhApi.Infrastructure.Repositories;

public class VacancyRepository : RepositoryBase<Vacancy>, IVacancyRepository
{
    public VacancyRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
