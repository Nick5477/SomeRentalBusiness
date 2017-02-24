namespace Domain.Services
{
    using Entities;

    public class DepositCalculator : IDepositCalculator
    {
        public decimal Calculate(Bike bike)
        {
            return 10 * bike.HourCost;
        }
    }
}