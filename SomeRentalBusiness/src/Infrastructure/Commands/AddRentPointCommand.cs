using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Commands;
using Domain.Commands.Contexts;
using Domain.Entities;
using Domain.Services;

namespace Infrastructure.Commands
{
    public class AddRentPointCommand:ICommand<AddRentPointCommandContext>
    {
        private readonly IRentPointService _rentPointService;
        public AddRentPointCommand(IRentPointService rentPointService)
        {
            _rentPointService = rentPointService;
        }
        public void Execute(AddRentPointCommandContext commandContext)
        {
            RentPoint rp=_rentPointService.AddRentPoint(commandContext.Name,commandContext.Employee, commandContext.Money);
            commandContext.CreatedRentPoint = rp;
        }
    }
}
