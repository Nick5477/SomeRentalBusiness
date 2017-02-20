using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Commands.Contexts
{
    public class ReserveBikeCommandContext:ICommandContext
    {
        public Client Client { get; set; }
        public Bike Bike { get; set; }
        public DateTime EndTime { get; set; }
    }
}
