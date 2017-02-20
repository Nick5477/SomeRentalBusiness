using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Queries.Criteria;
using Domain.Repositories;

namespace Infrastructure.Queries
{
    public class GetAllBikesQuery:IQuery<EmptyCriterion,IEnumerable<Bike>>
    {
        private readonly IRepository<Bike> _bikeRepository;
        public GetAllBikesQuery(IRepository<Bike> bikeRepository)
        {
            _bikeRepository = bikeRepository;
        }

        public IEnumerable<Bike> Ask(EmptyCriterion criterion)
        {
            return _bikeRepository.All();
        }
    }
}
