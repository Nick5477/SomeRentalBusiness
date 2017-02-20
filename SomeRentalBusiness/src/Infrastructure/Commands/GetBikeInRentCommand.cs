using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Commands;
using Domain.Commands.Contexts;
using Domain.Services;

namespace Infrastructure.Commands
{
    public class GetBikeInRentCommand:ICommand<GetBikeInRentCommandContext>
    {
        private readonly IRentService _rentService;

        public GetBikeInRentCommand(IRentService rentService)
        {
            _rentService = rentService;
        }
        public void Execute(GetBikeInRentCommandContext commandContext)
        {
            _rentService.Take(commandContext.Client,commandContext.Bike,commandContext.Deposit);
        }
    }
}
