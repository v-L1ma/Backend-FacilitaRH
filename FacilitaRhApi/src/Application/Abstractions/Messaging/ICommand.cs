using MediatR;
using FacilitaRhApi.Domain.Abstractions;

namespace FacilitaRhApi.Application.Abstractions.Messaging
{
    public interface ICommand : IRequest<Result>
    {
    }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
