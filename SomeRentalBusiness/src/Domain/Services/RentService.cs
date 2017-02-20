namespace Domain.Services
{
    using System;
    using System.Linq;
    using Entities;
    using Entities.Deposits;
    using Repositories;

    public class RentService : IRentService
    {
        private readonly IDepositCalculator _depositCalculator;
        private readonly IRentSumCalculate _rentSumCalculate;
        private readonly IRepository<Rent> _rentRepository;
        private readonly IRepository<Reserve> _reserveRepository;


        public RentService(
            IDepositCalculator depositCalculator, 
            IRepository<Rent> rentRepository,
            IRentSumCalculate rentSumCalculate,
            IRepository<Reserve> reserveRepository)
        {
            if (depositCalculator == null)
                throw new ArgumentNullException(nameof(depositCalculator));

            if (rentRepository == null)
                throw new ArgumentNullException(nameof(rentRepository));

            _depositCalculator = depositCalculator;
            _rentRepository = rentRepository;
            _rentSumCalculate = rentSumCalculate;
            _reserveRepository = reserveRepository;
        }



        public void Take(Client client, Bike bike, Deposit deposit)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (bike == null)
                throw new ArgumentNullException(nameof(bike));

            if (deposit == null)
                throw new ArgumentNullException(nameof(deposit));

            if (bike.RentPoint == null)
                throw new InvalidOperationException("Bike is not on rent point");

            if (!bike.IsFree)
                throw new InvalidOperationException("Bike is not free");

            if (bike.IsBroken)
                throw new Exception("Sorry, this bike is broken. Please, choose another.");

            if (bike.IsReserved)
            {
                Reserve reserve = _reserveRepository.All().SingleOrDefault(x => x.Bike == bike);

                if (reserve.EndTime < DateTime.UtcNow)
                {
                    reserve.ExpireReserve();
                }
                else
                {
                    if (client != reserve.Client)
                        throw new InvalidOperationException("Bike in reserved");
                    else
                        reserve.EndReserve();
                }
            }

            if (deposit.Type == DepositTypes.Money)
            {
                decimal depositSum = _depositCalculator.Calculate(bike);

                if (((MoneyDeposit)deposit).Sum < depositSum)
                    throw new InvalidOperationException("Deposit sum is not enough");
            }

            bike.RentPoint.Safe.PutDeposit(deposit);
            
            Rent rent = new Rent(client, bike, deposit);

            bike.RentPoint.RemoveBike(bike);
            bike.Take();
            

            _rentRepository.Add(rent);
        }

        public void Return(Bike bike, RentPoint rentPoint, bool IsBroken)
        {
            if (bike == null)
                throw new ArgumentNullException(nameof(bike));

            if (rentPoint == null)
                throw new ArgumentNullException(nameof(rentPoint));



            Rent rent = _rentRepository
                .All()
                .SingleOrDefault(
                    x => x.Bike == bike && !x.IsEnded);

            if (rent == null)
                throw new InvalidOperationException("Rent not found");

            DateTime endTime = DateTime.UtcNow;
            decimal sum = _rentSumCalculate.Calcilate(rent.StartedAt, endTime, rent.HourCost);

            rent.End(rentPoint, endTime, sum);

            if (IsBroken)
            {
                bike.IsBroken = true;
                rentPoint.CashBox.PutMoney(bike.Cost);
            }
        }
    }
}