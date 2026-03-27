using FacilitaRhApi.Domain.Models;

namespace FacilitaRhApi.Application.Features.Statistics.GetStatistics;

public record GetStatisticsResponse(
    double TempoMedio,
    IEnumerable<Vacancy> Vacancies,
    IEnumerable<SetorStats> VagasPorSetor,
    IEnumerable<MesStats> VagasPorMes,
    IEnumerable<Domain.Models.Application> Candidates);

public record SetorStats(string Setor, int Vagas, string Fill);
public record MesStats(string Mes, int Vagas);
