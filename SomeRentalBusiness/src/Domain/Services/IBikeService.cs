namespace Domain.Services
{
    using Entities;

    public interface IBikeService
    {
        void AddBike(string name, decimal hourCost, decimal cost);

        void Rename(Bike bike, string name);
        void MoveBike(Bike bike, RentPoint myRentPoint);
    }
}
