using System.Threading;
using System.Threading.Tasks;
using Sher.Application.Processing;
using Sher.Core.Base;

namespace Sher.Infrastructure.Processing
{
    public class CommandHandlerUnitOfWorkDecorator<TRequest, TResponse> : ICommandHandler<TRequest, TResponse> where TRequest : ICommand<TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommandHandler<TRequest, TResponse> _handler;

        public CommandHandlerUnitOfWorkDecorator(IUnitOfWork unitOfWork, ICommandHandler<TRequest, TResponse> handler)
        {
            _unitOfWork = unitOfWork;
            _handler = handler;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var result = await _handler.Handle(request, cancellationToken);
            await _unitOfWork.CommitChangesAsync();

            return result;
        }
    }
}