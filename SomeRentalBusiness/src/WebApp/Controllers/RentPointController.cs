using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Commands;
using Domain.Commands.Contexts;
using Domain.Entities;
using Domain.Queries.Criteria;
using Infrastructure.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class RentPointController : Controller
    {
        private readonly IQueryBuilder _queryBuilder;
        private readonly ICommandBuilder _commandBuilder;
        // GET: /<controller>/
        public RentPointController(IQueryBuilder queryBuilder, ICommandBuilder commandBuilder)
        {
            _queryBuilder = queryBuilder;
            _commandBuilder = commandBuilder;
            if (!_queryBuilder.For<IEnumerable<RentPoint>>().With(new EmptyCriterion()).Any())
            {
                AddEmployeeCommandContext employeeContext = new AddEmployeeCommandContext()
                {
                    Surname = "Ivanov",
                    FirstName = "Ivan",
                    Patronymic = "Ivanovich"
                };
                _commandBuilder.Execute(employeeContext);
                AddRentPointCommandContext komprosRentPointContext = new AddRentPointCommandContext()
                {
                    Name = "Компрос",
                    Employee = employeeContext.CreatedEmployee,
                    Money = 10000
                };
                _commandBuilder.Execute(komprosRentPointContext);
                AddRentPointCommandContext leninaRentPointContext = new AddRentPointCommandContext()
                {
                    Name = "Ленина",
                    Employee = employeeContext.CreatedEmployee,
                    Money = 10000
                };
                _commandBuilder.Execute(leninaRentPointContext);
                _commandBuilder.Execute(new AddBikeCommandContext()
                {
                    Name = "Kama",
                    Cost = 1000,
                    HourCost = 100,
                    RentPoint = komprosRentPointContext.CreatedRentPoint
                });
                _commandBuilder.Execute(new AddBikeCommandContext()
                {
                    Name = "Stels",
                    Cost = 1000,
                    HourCost = 100,
                    RentPoint = komprosRentPointContext.CreatedRentPoint
                });
                _commandBuilder.Execute(new AddBikeCommandContext()
                {
                    Name = "Forward",
                    Cost = 1000,
                    HourCost = 100,
                    RentPoint = komprosRentPointContext.CreatedRentPoint
                });
                _commandBuilder.Execute(new AddBikeCommandContext()
                {
                    Name = "Lexus134",
                    Cost = 1000,
                    HourCost = 100,
                    RentPoint = leninaRentPointContext.CreatedRentPoint
                });
            }
        }
        public IActionResult Add(string name,string surname,string firstname,string patronymic,decimal money)
        {
            AddEmployeeCommandContext employeeCommandContext=new AddEmployeeCommandContext()
            {
                Surname = surname,
                FirstName = firstname,
                Patronymic = patronymic
            };
            _commandBuilder.Execute(employeeCommandContext);
            _commandBuilder.Execute(new AddRentPointCommandContext()
            {
                Name=name,
                Employee = employeeCommandContext.CreatedEmployee,
                Money = money
            });
            ViewBag.Message = string.Format("Точка проката {0} успешно добавлена", name);
            return View();
        }

        public IActionResult List()
        {
            IEnumerable<RentPoint> listRentPoints = 
                _queryBuilder
                .For<IEnumerable<RentPoint>>()
                .With(new EmptyCriterion());
            return View(listRentPoints);
        }

        public IActionResult View(string name)
        {
            RentPoint rentPoint = _queryBuilder.For<RentPoint>().With(new NameCriterion()
            {
                Name = name
            });
            return View(rentPoint);
        }

        public IActionResult AddRentPointIndex()
        {
            return View();
        }

        public IActionResult GetAllBikesOnRentPoint(string name)
        {
            RentPoint rentPoint = _queryBuilder.For<RentPoint>().With(new NameCriterion()
            {
                Name = name
            });
            IEnumerable<Bike> listBikes = _queryBuilder.For<IEnumerable<Bike>>().With(new RentPointCriterion()
            {
                RentPoint = rentPoint
            });
            ViewData["Message"] = string.Format("Байки с точки проката {0}", name);
            return View(listBikes);
        }
    }
}
