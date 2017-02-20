using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Domain.Commands;
using Domain.Commands.Contexts;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Infrastructure.Commands
{
    public class AddEmployeeCommand:ICommand<AddEmployeeCommandContext>
    {
        private readonly IEmployeeService _employeeService;
        private readonly IRepository<Employee> _employeeRepository;

        public AddEmployeeCommand(IEmployeeService employeeService,IRepository<Employee> employeeRepository)
        {
            _employeeService = employeeService;
            _employeeRepository = employeeRepository;
        }
        public void Execute(AddEmployeeCommandContext commandContext)
        {
            _employeeService.AddEmployee(commandContext.Surname,commandContext.FirstName,commandContext.Patronymic);
            Employee emp=_employeeRepository.All()
                .SingleOrDefault(
                    x =>
                        x.FullName == 
                        $"{commandContext.Surname} {commandContext.FirstName} {commandContext.Patronymic}");
            commandContext.CreatedEmployee = emp;
        }
    }
}
