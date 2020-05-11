using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BerlinClock.Dtos;

namespace BerlinClock.Services.Interfaces
{
    public interface ITimeParsingService
    {
        TimeDto ParseTime(string time);
    }
}
