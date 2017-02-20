using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Commands;
using Domain.Commands.Contexts;
using Domain.Services;

namespace Infrastructure.Commands
{
    public class MoveBikeCommand:ICommand<MoveBikeCommandContext>
    {
        private readonly IBikeService _bikeService;

        public MoveBikeCommand(IBikeService bikeService)
        {
            _bikeService = bikeService;
        }
        public void Execute(MoveBikeCommandContext commandContext)
        {
            _bikeService.MoveBike(commandContext.Bike,commandContext.RentPoint);
        }
    }
}
