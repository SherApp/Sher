using MediatR;

namespace Sher.Application.Processing
{
    public interface IQuery<out T> : IRequest<T>
    {
        
    }
}