using System;

namespace Domain.Entities
{
    public class Reserve : IEntity
    {
        public readonly Client Client;
        public readonly Bike Bike;
        public DateTime EndTime { get; protected set; }
        public readonly DateTime StartDate;
        public ReserveStatus Status { get; private set; }

        //public. Хочу спать.
        public Reserve(Client client, Bike bike, DateTime endTime)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (bike == null)
                throw new ArgumentNullException(nameof(bike));

            if (endTime == null)
                throw new ArgumentNullException(nameof(endTime));


            Client = client;
            Bike = bike;
            Bike.IsReserved = true;
            EndTime = endTime;
            StartDate = DateTime.UtcNow;
            Status = ReserveStatus.Wait;
        }

        public void EndReserve()
        {
            Status = ReserveStatus.SuccessEnded;
            Bike.IsReserved = false;
            EndTime = DateTime.UtcNow;
        }

        public void ExpireReserve()
        {
            Status = ReserveStatus.Failed;
            Bike.IsReserved = false;
        }

    }
}
