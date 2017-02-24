using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Commands;
using Domain.Commands.Contexts;
using Infrastructure.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IQueryBuilder _queryBuilder;
        private readonly ICommandBuilder _commandBuilder;

        public EmployeeController(IQueryBuilder queryBuilder,ICommandBuilder commandBuilder)
        {
            _queryBuilder = queryBuilder;
            _commandBuilder = commandBuilder;
        }
        // GET: /<controller>/
        [HttpPost]
        public IActionResult Add(string surname, string firstname, string patronymic)
        {
            _commandBuilder
                .Execute(new AddEmployeeCommandContext()
                {
                    FirstName = firstname,
                    Surname = surname,
                    Patronymic = patronymic
                });
            ViewBag.Message = string.Format("Работник {0} {1} {2} успешно добавлен.",surname,firstname,patronymic);
            return View();
        }
        
        public IActionResult AddEmployeeIndex()
        {
            return View();
        }
    }
}
