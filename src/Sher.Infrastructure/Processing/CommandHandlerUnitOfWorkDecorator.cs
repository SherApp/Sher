using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sher.Application.Processing;
using Sher.Core.Base;

namespace Sher.Infrastructure.Processing
{
    public class CommandHandlerUnitOfWorkDecorator<T> : ICommandHandler<T> where T : ICommand, IRequest
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommandHandler<T> _handler;

        public CommandHandlerUnitOfWorkDecorator(IUnitOfWork unitOfWork, ICommandHandler<T> handler)
        {
            _unitOfWork = unitOfWork;
            _handler = handler;
        }

        public async Task<Unit> Handle(T request, CancellationToken cancellationToken)
        {
            await _handler.Handle(request, cancellationToken);
            await _unitOfWork.CommitChangesAsync();

            return Unit.Value;
        }
    }
}