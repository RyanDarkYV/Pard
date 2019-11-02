using MediatR;

namespace Pard.Application.Common.Abstractions.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}
