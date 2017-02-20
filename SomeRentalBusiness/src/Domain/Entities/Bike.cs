namespace Domain.Entities
{
    using System;

    public class Bike : IEntity
    {
        protected internal Bike(string name, decimal hourCost, decimal cost)
        {
            if (cost <= 0)
                throw new ArgumentOutOfRangeException("Cannot create Bike with zero or negative cost");
            Rename(name);
            ChangeHourCost(hourCost);
            Cost = cost;
        }
//RANDOM COMMENT

        public string Name { get; protected set; }

        public decimal HourCost { get; protected set; }

        public bool IsReserved = false;

        public bool IsFree
        {
            get
            {
                return (RentPoint == null) ? false : true;
            }
        }

        public bool IsBroken = false;

        public decimal Cost { get; private set; }

        public RentPoint RentPoint { get; protected set; }

        protected internal void Rename(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
        }

        public void ChangeHourCost(decimal hourCost)
        {
            if (hourCost <= 0)
                throw new ArgumentOutOfRangeException(nameof(hourCost), "Hour cost must be more than 0");

            HourCost = hourCost;
        }

        public void MoveTo(RentPoint rentPoint)
        {

            if (rentPoint == null)
                throw new ArgumentNullException(nameof(rentPoint));

            if (rentPoint == RentPoint)
                return;

            RentPoint?.RemoveBike(this);

            rentPoint.AddBike(this);

            RentPoint = rentPoint;
        }



        protected internal void Take()
        {
            if (!IsFree)
                throw new InvalidOperationException("Bike is not free");

            RentPoint = null;

        }

        protected internal void Return(RentPoint rentPoint)
        {
            if (IsFree)
                throw new InvalidOperationException("Bike is free");

            RentPoint = rentPoint;
        }
    }
}
