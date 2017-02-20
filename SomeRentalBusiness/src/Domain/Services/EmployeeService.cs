using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeeService(
            IRepository<Employee> empRepository)
        {

            if (empRepository == null)
                throw new ArgumentNullException(nameof(empRepository));

            _employeeRepository = empRepository;

        }

        public void AddEmployee(string surname, string firstname, string patronymic)
        {
            if (surname == null)
                throw new ArgumentNullException(nameof(surname));

            if (firstname == null)
                throw new ArgumentNullException(nameof(firstname));

            if (patronymic == null)
                throw new ArgumentNullException(nameof(patronymic));

            Employee emp = new Employee(surname, firstname, patronymic);

            _employeeRepository.Add(emp);
        }

        public void GiveEmployeeFree(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
