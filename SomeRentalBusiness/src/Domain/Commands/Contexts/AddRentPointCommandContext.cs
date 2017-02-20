using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Commands.Contexts
{
    public class AddRentPointCommandContext:ICommandContext
    {
        public Employee Employee { get; set; }
        public decimal Money { get; set; }
        public RentPoint CreatedRentPoint { get; set; }
    }
}
