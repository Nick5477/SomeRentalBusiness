using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Queries.Criteria;
using Domain.Repositories;

namespace Infrastructure.Queries
{
    public class GetBikeByNameQuery:IQuery<NameCriterion,Bike>
    {
        private readonly IRepository<Bike> _repository;

        public GetBikeByNameQuery(IRepository<Bike> repository)
        {
            _repository = repository;
        }
        public Bike Ask(NameCriterion criterion)
        {
            return _repository.All().SingleOrDefault(x => x.Name == criterion.Name);
        }
    }
}
