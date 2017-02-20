using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IEmployeeService
    {
        void AddEmployee(string surname, string firstname, string patronymic);

        void GiveEmployeeFree(Employee employee);
    }
}
