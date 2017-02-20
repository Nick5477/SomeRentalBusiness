using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IRentSumCalculate
    {
        decimal Calcilate(DateTime startTime, DateTime endTime, decimal HourCost);
    }
}
