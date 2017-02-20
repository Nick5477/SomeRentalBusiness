using Infrastructure.Queries;

namespace Infrastructure.Queries
{
    public interface IQueryBuilder
    {
        IQueryFor<TResult> For<TResult>();
    }
}