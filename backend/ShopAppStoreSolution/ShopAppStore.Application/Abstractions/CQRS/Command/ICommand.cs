using MediatR;
using ShopAppStore.Shared;

namespace ShopAppStore.Application.Abstractions.CQRS.Command
{
    public interface ICommand : IRequest<Result>
    {
    }
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result> where TCommand : ICommand
    {
    }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    {
    }
    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>> where TCommand : ICommand<TResponse>
    {
    }
}
