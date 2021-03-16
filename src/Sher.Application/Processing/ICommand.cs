using MediatR;

namespace Sher.Application.Processing
{
    public interface ICommand<out T> : IRequest<T>
    {
        
    }

    public interface ICommand : ICommand<Unit>
    {
        
    }
}