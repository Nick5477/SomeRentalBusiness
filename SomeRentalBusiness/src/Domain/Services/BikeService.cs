namespace Domain.Services
{
    using System;
    using Entities;
    using Repositories;
    using System.Linq;

    public class BikeService : IBikeService
    {
        private readonly IRepository<Bike> _repository;
        private readonly IBikeNameVerifier _bikeNameVerifier;
        private readonly IRepository<Rent> _rentRepository;
        private readonly IRepository<Reserve> _reserveRepository;

        public BikeService(IRepository<Bike> repository, 
            IBikeNameVerifier bikeNameVerifier,
            IRepository<Rent> rentRepository,
            IRepository<Reserve> reserverepository)
        {
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));

            if (bikeNameVerifier == null)
                throw new ArgumentNullException(nameof(bikeNameVerifier));

            if (rentRepository == null)
                throw new ArgumentNullException(nameof(rentRepository));

            _repository = repository;
            _bikeNameVerifier = bikeNameVerifier;
            _rentRepository = rentRepository;
            _reserveRepository = reserverepository;
        }



        public void AddBike(string name, decimal hourCost, decimal cost)
        {
            if (!_bikeNameVerifier.IsFree(name))
                throw new InvalidOperationException("Bike with same name already exists");

            Bike newBike = new Bike(name, hourCost, cost);

            _repository.Add(newBike);
        }

        public void Rename(Bike bike, string name)
        {
            if (bike == null)
                throw new ArgumentNullException(nameof(bike));

            if (bike.Name == name)
                return;

            if (!_bikeNameVerifier.IsFree(name))
                throw new InvalidOperationException("Bike with same name already exists");

            bike.Rename(name);
        }

        public void MoveBike(Bike bike, RentPoint rentPoint)
        {
            Rent rent = _rentRepository.All().SingleOrDefault(x => x.Bike == bike);
            if (rent != null)
                throw new InvalidOperationException("Bike is not free");

            Reserve reserve = _reserveRepository.All().SingleOrDefault( (x) => (x.Bike == bike && x.Status == ReserveStatus.Wait));
            if (reserve != null)
                throw new InvalidOperationException("Bike reserved");

            bike.MoveTo(rentPoint);
        }
    }
}
