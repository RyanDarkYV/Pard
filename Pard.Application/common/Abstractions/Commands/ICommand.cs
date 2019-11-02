using MediatR;

namespace Pard.Application.Common.Abstractions.Commands
{
    public interface ICommand : IRequest
    {

    }

    public interface ICommand<out TResult> : IRequest<TResult>
    {

    }
}
