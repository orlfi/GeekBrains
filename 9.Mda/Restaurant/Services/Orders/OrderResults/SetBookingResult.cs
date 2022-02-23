using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Request
{
    public class SetBookingResult: RequestResult
    {
        public int SeatsCount { get; init; }

        public SetBookingResult(int seatsCount) => SeatsCount = seatsCount;
    }
}
