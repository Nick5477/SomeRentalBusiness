namespace Domain.Entities.Deposits
{
    using System;

    public class PassportDeposit : Deposit
    {
        public PassportDeposit(string series, string number) 
            : base(DepositTypes.Passport)
        {
            if (string.IsNullOrWhiteSpace(series))
                throw new ArgumentNullException(nameof(series));

            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentNullException(nameof(number));

            // TODO проверки на длину и циферность :)

            Series = series;
            Number = number;
        }



        public string Series { get; protected set; }

        public string Number { get; protected set; }
    }
}
