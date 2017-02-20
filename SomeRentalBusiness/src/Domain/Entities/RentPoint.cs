namespace Domain.Entities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Deposits;

    public class RentPoint : IEntity
    {
        protected readonly IList<Bike> _bikes = new List<Bike>();

        public RentPoint(Employee employee, Safe safe, CashBox cashbox)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            if (safe == null)
                throw new ArgumentNullException(nameof(safe));

            if (cashbox == null)
                throw new ArgumentNullException(nameof(cashbox));

            Employee = employee;
            Safe = safe;
            CashBox = cashbox;
        }


        public readonly Employee Employee;// { get; protected set; }
        public Safe Safe;
        public CashBox CashBox;

        public IEnumerable<Bike> Bikes => _bikes.AsEnumerable();

        protected internal void AddBike(Bike bike)
        {
            if (bike == null)
                throw new ArgumentNullException(nameof(bike));

            _bikes.Add(bike);
        }

        protected internal void RemoveBike(Bike bike)
        {
            if (bike == null)
                throw new ArgumentNullException(nameof(bike));

            _bikes.Remove(bike);
        }

        //protected internal void SetEmployee(Employee employee)
        //{
        //    if (employee == null)
        //        throw new ArgumentNullException(nameof(employee));

        //    if (Employee != null)
        //        throw new InvalidOperationException("Rent point has employee yet");

        //    Employee = employee;
        //}

    }
}