using FacilitaRhApi.Application.Abstractions.Messaging;
using FacilitaRhApi.Application.Repositories;
using FacilitaRhApi.Domain.Abstractions;

namespace FacilitaRhApi.Application.Features.Statistics.GetStatistics;

public sealed class GetStatisticsQueryHandler : IQueryHandler<GetStatisticsQuery, GetStatisticsResponse>
{
    private readonly IVacancyRepository _vacancyRepository;
    private readonly IApplicationRepository _applicationRepository;

    private static readonly (string Setor, string Fill)[] Setores =
    {
        ("Administrativo", "var(--color-Administrativo)"),
        ("Financeiro", "var(--color-Financeiro)"),
        ("Comercial", "var(--color-Comercial)"),
        ("Vendas", "var(--color-Vendas)"),
        ("Marketing", "var(--color-Marketing)"),
        ("Tecnologia da Informação", "var(--color-TI)"),
        ("Atendimento ao Cliente", "var(--color-Atendimento)"),
        ("Logística", "var(--color-Logistica)"),
        ("Jurídico", "var(--color-Juridico)"),
        ("Produção / Manufatura", "var(--color-Producao)"),
        ("Compras / Suprimentos", "var(--color-Compras)"),
        ("Almoxarifado", "var(--color-Almoxarifado)"),
        ("Qualidade", "var(--color-Qualidade)"),
        ("Segurança do Trabalho", "var(--color-Seguranca)")
    };

    private static readonly string[] Meses =
    {
        "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho",
        "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"
    };

    public GetStatisticsQueryHandler(
        IVacancyRepository vacancyRepository,
        IApplicationRepository applicationRepository)
    {
        _vacancyRepository = vacancyRepository;
        _applicationRepository = applicationRepository;
    }

    public async Task<Result<GetStatisticsResponse>> Handle(
        GetStatisticsQuery request,
        CancellationToken cancellationToken)
    {
        var vacancies = (await _vacancyRepository.GetAllAsync()).ToList();
        var candidates = (await _applicationRepository.GetAllAsync()).ToList();

        // Tempo médio (em dias) — bug #5 corrigido: guard contra divisão por zero
        double tempoMedio = 0;
        if (vacancies.Count > 0)
        {
            var totalDias = vacancies.Sum(v =>
            {
                if (DateTime.TryParse(v.DataAbertura, out var abertura) &&
                    DateTime.TryParse(v.DataFechamento, out var fechamento))
                {
                    return (fechamento - abertura).TotalDays;
                }
                return 0;
            });
            tempoMedio = totalDias / vacancies.Count;
        }

        // Vagas por mês de abertura
        var vagasPorMes = Meses.Select((mes, index) => new MesStats(
            mes,
            vacancies.Count(v =>
                DateTime.TryParse(v.DataAbertura, out var dt) && dt.Month == index + 1)
        )).ToList();

        // Vagas por setor
        var vagasPorSetor = Setores.Select(s => new SetorStats(
            s.Setor,
            vacancies.Count(v => v.Setor.Contains(s.Setor, StringComparison.OrdinalIgnoreCase)),
            s.Fill
        )).ToList();

        var response = new GetStatisticsResponse(
            tempoMedio,
            vacancies,
            vagasPorSetor,
            vagasPorMes,
            candidates);

        return response;
    }
}
