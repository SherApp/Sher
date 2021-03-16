using MediatR;

namespace Sher.Application.Processing
{
    public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
    }

    public interface ICommandHandler<in TRequest> : ICommandHandler<TRequest, Unit>
        where TRequest : ICommand
    {
    }
}