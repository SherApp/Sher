using MediatR;

namespace Sher.Application.Processing
{
    public interface ICommandHandler<in T> : IRequestHandler<T> where T : ICommand
    {
        
    }
}