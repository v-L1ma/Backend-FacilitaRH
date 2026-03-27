using FacilitaRhApi.Application.Abstractions.Messaging;

namespace FacilitaRhApi.Application.Features.Statistics.GetStatistics;

public record GetStatisticsQuery() : IQuery<GetStatisticsResponse>;
