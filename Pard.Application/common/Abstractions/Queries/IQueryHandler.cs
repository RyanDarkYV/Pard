using MediatR;

namespace Pard.Application.Common.Abstractions.Queries
{
    public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        
    }
}