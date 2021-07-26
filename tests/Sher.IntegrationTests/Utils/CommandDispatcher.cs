using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sher.Application.Processing;

namespace Sher.IntegrationTests.Utils
{
    public static class CommandDispatcher
    {
        public static async Task DispatchCommand(this IServiceProvider provider, ICommand command)
        {
            var mediator = provider.GetRequiredService<IMediator>();

            await mediator.Send(command);
        }
    }
}