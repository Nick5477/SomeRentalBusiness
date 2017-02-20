using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class RentSumCalculate : IRentSumCalculate
    {
        public decimal Calcilate(DateTime startTime, DateTime endTime, decimal HourCost)
        {
            return (decimal)(Math.Round(Math.Ceiling((endTime - startTime).TotalHours) * (double)HourCost, 2));
        }
    }
}
