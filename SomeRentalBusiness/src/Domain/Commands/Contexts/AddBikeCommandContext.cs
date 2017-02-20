using Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Commands.Contexts
{
    public class AddBikeCommandContext : ICommandContext
    {
        public string Name { get; set; }
        public decimal HourCost { get; set; }
        public decimal Cost { get; set; }
        public RentPoint RentPoint { get; set; }
    }
}
