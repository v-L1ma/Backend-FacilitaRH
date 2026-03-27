using MediatR;
using FacilitaRhApi.Domain.Abstractions;

namespace FacilitaRhApi.Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
