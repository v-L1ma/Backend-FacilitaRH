using MediatR;
using FacilitaRhApi.Domain.Abstractions;

namespace FacilitaRhApi.Application.Abstractions.Messaging
{
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery : IQuery<TResponse>
    {
    }
}
