using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Commands;
using Domain.Commands.Contexts;
using Domain.Entities;
using Domain.Entities.Deposits;
using Domain.Queries.Criteria;
using Infrastructure.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class BikeController : Controller
    {
        private readonly IQueryBuilder _queryBuilder;
        private readonly ICommandBuilder _commandBuilder;

        public BikeController(IQueryBuilder queryBuilder,ICommandBuilder commandBuilder)
        {
            _queryBuilder = queryBuilder;
            _commandBuilder = commandBuilder;
        }
        // GET: /<controller>/
        public IActionResult View(string name)
        {
            Bike bike = _queryBuilder.For<Bike>().With(new NameCriterion()
            {
                Name = name
            });
            return View(bike);
        }

        public IActionResult Take(string name,string surname,string firstName, string patronymic,decimal money)
        {
            AddClientCommandContext clientCommandContext=new AddClientCommandContext()
            {
                FirstName = firstName,
                Surname = surname,
                Patronymic = patronymic
            };
            _commandBuilder.Execute(clientCommandContext);
            MoneyDeposit deposit=new MoneyDeposit(money);
            Bike bike = _queryBuilder.For<Bike>().With(new NameCriterion()
            {
                Name = name
            });
            _commandBuilder.Execute(new GetBikeInRentCommandContext()
            {
                Bike = bike,
                Client = clientCommandContext.CreatedClient,
                Deposit = deposit
            });
            ViewData["Message"] = string.Format("{0} взял в прокат {1} и отдал в качестве депозита {2} рублей",
                clientCommandContext.CreatedClient.FullName, name, money.ToString());
            return View();
        }

        public IActionResult ReturnBikePage()
        {
            return View();
        }

        public IActionResult Return(string bikeName, string rentPointName)
        {
            RentPoint rentPoint = _queryBuilder.For<RentPoint>().With(new NameCriterion()
            {
                Name = rentPointName
            });
            Bike bike = _queryBuilder.For<Bike>().With(new NameCriterion()
            {
                Name = bikeName
            });
            _commandBuilder.Execute(new ReturnBikeCommandContext()
            {
                Bike = bike,
                RentPoint = rentPoint,
                IsBroken = false
            });
            return View();
        }
    }
}
