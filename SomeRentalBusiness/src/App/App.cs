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
        private readonly IRentService _rentService;
        private readonly IEmployeeService _employeeService;
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Reserve> _reserveRepository;
        private readonly ICommandBuilder _commandBuilder;
        private readonly IQueryBuilder _queryBuilder;

        public App(
            IRepository<Client> clientRepository,
            IRepository<Employee> employeeRepository,
            IRepository<Reserve> reserveRepository,
            IEmployeeService employeeService,
            IRentService rentService,
            ICommandBuilder commandBuilder,
            IQueryBuilder queryBuilder)
        {
            _clientRepository = clientRepository;
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
            _rentService = rentService;
            _reserveRepository = reserveRepository;
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

        public void AddRentPoint(Employee myEmployee,decimal money=10000)
        {
            _commandBuilder.Execute(new AddRentPointCommandContext()
            {
                Employee = myEmployee,
                Money = money
            });

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
            Employee employee = new Employee(surname, firstname, patronymic);
            _employeeRepository.Add(employee);
            return employee;
            //_employeeService.AddEmployee(surname, firstname, patronymic);
        }

        public Client CreateClient(string surname, string firstname, string patronymic)
        {
            Client client = new Client(surname, firstname, patronymic);
            _clientRepository.Add(client);
            return client;
        }

        public void GetBikeInRent(Client client, Bike bike, Deposit deposit)
        {
            _rentService.Take(client, bike, deposit);
        }

        public void ReturnBike(Bike bike, RentPoint rentPoint, bool IsBroken)
        {
            _rentService.Return(bike, rentPoint, IsBroken);
        }

        public void ReserveBike(Client client, Bike bike, DateTime endTime)
        {
            Reserve reserve = new Reserve(client, bike, endTime);
            _reserveRepository.Add(reserve);
        }
    }
}
