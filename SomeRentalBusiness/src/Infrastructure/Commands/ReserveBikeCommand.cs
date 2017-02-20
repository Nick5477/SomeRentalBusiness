using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Commands;
using Domain.Commands.Contexts;
using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Commands
{
    public class ReserveBikeCommand:ICommand<ReserveBikeCommandContext>
    {
        private readonly IRepository<Reserve> _repository;

        public ReserveBikeCommand(IRepository<Reserve> repository)
        {
            _repository = repository;
        }
        public void Execute(ReserveBikeCommandContext commandContext)
        {
            Reserve reserve = new Reserve(commandContext.Client, commandContext.Bike, commandContext.EndTime);
            _repository.Add(reserve);
        }
    }
}
