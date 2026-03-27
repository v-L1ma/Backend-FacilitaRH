using Microsoft.EntityFrameworkCore;
using FacilitaRhApi.Application.Repositories;

namespace FacilitaRhApi.Infrastructure.Repositories;

public class ApplicationRepository : RepositoryBase<Domain.Models.Application>, IApplicationRepository
{
    private readonly AppDbContext _dbContext;

    public ApplicationRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Domain.Models.Application?> GetByEmailOrCpfAsync(string email, string cpf)
    {
        return await _dbContext.Applications
            .FirstOrDefaultAsync(a => a.Email == email || a.Cpf == cpf);
    }

    public async Task<IEnumerable<Domain.Models.Application>> GetByVacancyIdAsync(int vacancyId)
    {
        return await _dbContext.Applications
            .Where(a => a.VacancyId == vacancyId)
            .ToListAsync();
    }
}
