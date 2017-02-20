using Domain.Queries.Criteria;

namespace Infrastructure.Queries
{

    public interface IQueryFactory
    {
        IQuery<TCriterion, TResult> Create<TCriterion, TResult>() where TCriterion : ICriterion;
    }
}
