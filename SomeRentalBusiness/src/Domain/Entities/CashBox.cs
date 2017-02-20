using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CashBox
    {
        public decimal Money { get; private set; }

        public CashBox(decimal startMoney)
        {
            if (startMoney < 0)
                throw new ArgumentOutOfRangeException(nameof(startMoney));

            Money = startMoney;
        }

        public void PutMoney(decimal money)
        {
            if (money < 0)
                throw new ArgumentOutOfRangeException(nameof(money));

            Money += money;
        }

        public void TakeMoney(decimal money)
        {
            if (money < 0)
                throw new ArgumentOutOfRangeException(nameof(money));

            if (money > Money)
                throw new InvalidOperationException("Not enough money");

            Money -= money;
        }


    }
}
