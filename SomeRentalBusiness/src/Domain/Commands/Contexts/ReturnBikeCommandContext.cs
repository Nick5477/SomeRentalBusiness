using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Commands.Contexts
{
    public class ReturnBikeCommandContext:ICommandContext
    {
        public Bike Bike { get; set; }
        public RentPoint RentPoint { get; set; }
        public bool IsBroken { get;set; }
    }
}
