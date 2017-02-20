using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Queries.Criteria;
using Domain.Repositories;

namespace Infrastructure.Queries
{
    public class GetAllRentPointsQuery : IQuery<EmptyCriterion,IEnumerable<RentPoint>>
    {
        private readonly IRepository<RentPoint> _repository;
        public GetAllRentPointsQuery(IRepository<RentPoint> repository)
        {
            _repository = repository;
        }
        public IEnumerable<RentPoint> Ask(EmptyCriterion criterion)
        {
            return _repository.All();
        }
    }
}
