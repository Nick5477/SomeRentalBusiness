using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IRentPointService
    {
        RentPoint AddRentPoint(Employee employee, decimal money = 10000);

        void CloseRentPoint(RentPoint rentPoint);
    }
}
