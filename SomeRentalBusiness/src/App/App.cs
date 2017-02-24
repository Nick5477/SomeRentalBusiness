using Domain.Commands;
using Domain.Commands.Contexts;
using Domain.Queries.Criteria;
using Infrastructure.Queries;

namespace App
{
    using System;
    using System.Collections.Generic;
    using Domain.Entities;
    using Domain.Repositories;
    using Domain.Services;
    using Domain.Entities.Deposits;

    public class App
    {
		//random comment
        private readonly ICommandBuilder _commandBuilder;
        private readonly IQueryBuilder _queryBuilder;

        public App(
            ICommandBuilder commandBuilder,
            IQueryBuilder queryBuilder)
        {
            _commandBuilder = commandBuilder;
            _queryBuilder = queryBuilder;
        }



        public void AddBike(string name, decimal hourCost, decimal cost, RentPoint myRentPoint)
        {
            _commandBuilder.Execute(new AddBikeCommandContext()
            {
                Name = name,
                HourCost = hourCost,
                Cost=cost,
                RentPoint = myRentPoint
            });
        }

        public RentPoint AddRentPoint(string name,Employee myEmployee,decimal money=10000)
        {
            AddRentPointCommandContext context=new AddRentPointCommandContext()
            {
                Name=name,
                Employee = myEmployee,
                Money = money
            };
            _commandBuilder.Execute(context);

            return context.CreatedRentPoint;
        }

        public IEnumerable<Bike> GetBikes()
        {
            return _queryBuilder
                .For<IEnumerable<Bike>>()
                .With(new EmptyCriterion());
        }

        public IEnumerable<RentPoint> GetRentPoints()
        {
            return _queryBuilder
                .For<IEnumerable<RentPoint>>()
                .With(new EmptyCriterion());
        }

        public Employee CreateEmployee(string surname, string firstname, string patronymic)
        {
            AddEmployeeCommandContext context = new AddEmployeeCommandContext()
            {
                FirstName = firstname,
                Surname = surname,
                Patronymic = patronymic
            };
            _commandBuilder.Execute(context);
            return context.CreatedEmployee;
        }

        public Client CreateClient(string surname, string firstname, string patronymic)
        {
            AddClientCommandContext context = new AddClientCommandContext()
            {
                Surname = surname,
                FirstName = firstname,
                Patronymic = patronymic
            };
            _commandBuilder.Execute(context);
            return context.CreatedClient;
        }

        public void GetBikeInRent(Client client, Bike bike, Deposit deposit)
        {
            _commandBuilder.Execute(new GetBikeInRentCommandContext()
            {
                Bike=bike,
                Client = client,
                Deposit = deposit
            });
        }

        public void ReturnBike(Bike bike, RentPoint rentPoint, bool IsBroken)
        {
            _commandBuilder.Execute(new ReturnBikeCommandContext()
            {
                Bike = bike,
                RentPoint = rentPoint,
                IsBroken = IsBroken
            });
        }

        public void ReserveBike(Client client, Bike bike, DateTime endTime)
        {
            _commandBuilder.Execute(new ReserveBikeCommandContext()
            {
                Client = client,
                Bike = bike,
                EndTime=endTime
            });
        }

        public IEnumerable<Bike> GetBikesInRentPoint(RentPoint rentPoint)
        {
            return _queryBuilder
                .For<IEnumerable<Bike>>()
                .With(new RentPointCriterion()
            {
                RentPoint = rentPoint
            });
        }

        public void MoveBike(Bike bike, RentPoint rentPoint)
        {
            _commandBuilder.Execute(new MoveBikeCommandContext()
            {
                Bike = bike,
                RentPoint = rentPoint
            });
        }
    }
}
