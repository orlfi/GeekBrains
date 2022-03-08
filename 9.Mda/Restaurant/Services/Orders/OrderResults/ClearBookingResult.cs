using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Request
{
    public class ClearBookingResult : RequestResult
    {
        public int TableNumber { get; init; }

        public ClearBookingResult(int tableNumber) => TableNumber = tableNumber;
    }
}
