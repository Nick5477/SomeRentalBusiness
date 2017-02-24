using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Queries.Criteria;
using Domain.Services;
using Infrastructure.Queries;

namespace Infrastructure.Queries
{
    public class GetRentPointByNameQuery:IQuery<NameCriterion,RentPoint>
    {
        private readonly IRentPointService _rentPointService;

        public GetRentPointByNameQuery(IRentPointService rentPointService)
        {
            _rentPointService = rentPointService;
        }
        public RentPoint Ask(NameCriterion criterion)
        {
            return _rentPointService.GetRentPoint(criterion.Name);
        }
    }
}
