using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;

namespace Domain.Services
{
    public class RentPointService : IRentPointService
    {
        private readonly IRepository<RentPoint> _rentPointRepository;

        public RentPointService(
            IRepository<RentPoint> rentPointRepository)
        {

            if (rentPointRepository == null)
                throw new ArgumentNullException(nameof(rentPointRepository));

            _rentPointRepository = rentPointRepository;

        }

        public RentPoint AddRentPoint(string name,Employee employee, decimal money)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            if (money < 0)
                throw new ArgumentOutOfRangeException(nameof(money));

            CashBox cashbox = new CashBox(money);
            Safe safe = new Safe();

            RentPoint rentPoint = new RentPoint(name,employee, safe, cashbox);
            _rentPointRepository.Add(rentPoint);

            return rentPoint;
        }

        public void CloseRentPoint(RentPoint rentPoint)
        {
            throw new NotImplementedException();
        }

        public RentPoint GetRentPoint(string name)
        {
            return _rentPointRepository.All().SingleOrDefault(x => x.Name == name);
        }
    }
}
