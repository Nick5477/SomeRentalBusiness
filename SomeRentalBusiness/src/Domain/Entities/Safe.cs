using Domain.Entities.Deposits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Safe
    {
        protected readonly IList<PassportDeposit> _passportDeposits = new List<PassportDeposit>();

        public decimal Money { get; protected set; }

        public Safe()
        {

        }

        public void PutDeposit(Deposit deposit)
        {
            if (deposit == null)
                throw new ArgumentNullException(nameof(deposit));

            switch (deposit.Type)
            {
                case DepositTypes.Money:
                    PutMoney(((MoneyDeposit)deposit).Sum);
                    break;

                case DepositTypes.Passport:
                    _passportDeposits.Add((PassportDeposit)deposit);
                    break;
            }
        }

        public void ReturnDeposit(Deposit deposit)
        {
            switch (deposit.Type)
            {
                case DepositTypes.Money:
                    TakeMoney(((MoneyDeposit)deposit).Sum);
                    break;

                case DepositTypes.Passport:

                    PassportDeposit currentPassportDeposit = (PassportDeposit)deposit;

                    PassportDeposit passportDeposit = _passportDeposits
                        .SingleOrDefault(x =>
                            x.Number == currentPassportDeposit.Number &&
                            x.Series == currentPassportDeposit.Series);

                    if (passportDeposit == null)
                        throw new InvalidOperationException("No such passport");

                    _passportDeposits.Remove(passportDeposit);

                    break;
            }
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

        internal void MoveMoneyToCashBox(CashBox cashBox, decimal money)
        {
            TakeMoney(money);
            cashBox.PutMoney(money);
        }

        public RentPoint CurrentRentPoint { get; protected set; }



    }
}
