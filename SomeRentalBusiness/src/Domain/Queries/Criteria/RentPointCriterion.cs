using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Queries.Criteria
{
    public class RentPointCriterion:ICriterion
    {
        public RentPoint RentPoint { get; set; }
    }
}
