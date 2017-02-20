using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Queries.Criteria;
using Domain.Repositories;

namespace Infrastructure.Queries
{
    public class GetAllBikesInRentPointQuery:IQuery<RentPointCriterion,IEnumerable<Bike>>
    {
        private readonly IRepository<Bike> _repository;

        public GetAllBikesInRentPointQuery(IRepository<Bike> repository)
        {
            _repository = repository;
        }
        public IEnumerable<Bike> Ask(RentPointCriterion criterion)
        {
            return _repository.All()
                .Where(x => x.RentPoint == criterion.RentPoint);
        }
    }
}
