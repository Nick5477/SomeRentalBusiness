using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Commands;
using Domain.Commands.Contexts;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using Domain.Entities;

namespace Infrastructure.Commands
{
    public class AddBikeCommand : ICommand<AddBikeCommandContext>
    {
        private readonly IBikeNameVerifier _bikeNameVerifier;
        private readonly IBikeService _bikeService;
        public AddBikeCommand(IBikeNameVerifier bikeNameVerifier, IBikeService bikeService)
        {
            _bikeNameVerifier = bikeNameVerifier;
            _bikeService = bikeService;
        }
        public void Execute(AddBikeCommandContext commandContext)
        {
            if (!_bikeNameVerifier.IsFree(commandContext.Name))
                throw new InvalidOperationException("Bike with same name already exists");

            _bikeService.AddBike(commandContext.Name,commandContext.HourCost,commandContext.Cost);
        }
    }
}
