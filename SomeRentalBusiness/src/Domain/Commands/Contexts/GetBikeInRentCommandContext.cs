using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.Deposits;

namespace Domain.Commands.Contexts
{
    public class GetBikeInRentCommandContext:ICommandContext
    {
        public Client Client { get; set; }
        public Bike Bike { get; set; }
        public Deposit Deposit { get; set; }
    }
}
