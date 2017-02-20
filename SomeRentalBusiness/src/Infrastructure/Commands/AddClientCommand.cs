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
    public class AddClientCommand:ICommand<AddClientCommandContext>
    {
        private readonly IRepository<Client> _repository;

        public AddClientCommand(IRepository<Client> repository)
        {
            _repository = repository;
        }
        public void Execute(AddClientCommandContext commandContext)
        {
            Client client=new Client(commandContext.Surname,commandContext.FirstName,commandContext.Patronymic);
            _repository.Add(client);
            commandContext.CreatedClient = client;
        }
    }
}
