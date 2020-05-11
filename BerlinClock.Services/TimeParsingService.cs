using System;
using BerlinClock.Dtos;
using BerlinClock.Services.Interfaces;

namespace BerlinClock.Services
{
    public class TimeParsingService : ITimeParsingService
    {
        private const char FractionsSeparator = ':';

        public TimeDto ParseTime(string time)
        {
            var timeFractions = time.Split(FractionsSeparator);

            var timeDto = new TimeDto
            {
                Hours = ConvertTimeFractionToInt(timeFractions[0]),
                Minutes = ConvertTimeFractionToInt(timeFractions[1]),
                Seconds = ConvertTimeFractionToInt(timeFractions[2])
            };

            return timeDto;
        }

        private static int ConvertTimeFractionToInt(string timeFraction)
        {
            var timeFractionsSecondSymbol = timeFraction[1].ToString();
            var isTimeFractionsFirstSymbolZero = timeFraction.StartsWith("0");

            var normalizedTimeFraction = isTimeFractionsFirstSymbolZero ?
                timeFractionsSecondSymbol :
                timeFraction;

            var convertedTimeFraction = Convert.ToInt32(normalizedTimeFraction);

            return convertedTimeFraction;
        }
    }
}