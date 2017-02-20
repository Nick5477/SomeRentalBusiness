using Domain.Queries.Criteria;

namespace Infrastructure.Queries
{

    public interface IQuery<in TCriterion, out TResult>
        where TCriterion : ICriterion
    {
        TResult Ask(TCriterion criterion);
    }
}
