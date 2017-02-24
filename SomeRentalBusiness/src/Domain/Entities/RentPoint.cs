﻿namespace Domain.Entities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Deposits;

    public class RentPoint : IEntity
    {
        protected readonly IList<Bike> _bikes = new List<Bike>();

        public RentPoint(string name, Employee employee, Safe safe, CashBox cashbox)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            if (safe == null)
                throw new ArgumentNullException(nameof(safe));

            if (cashbox == null)
                throw new ArgumentNullException(nameof(cashbox));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            Name = name;
            Employee = employee;
            Safe = safe;
            CashBox = cashbox;
        }

        public string Name { get; protected set; }
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

        public override string ToString()
        {
            return string.Format("Имя точки проката: {0}", Name);
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