using System;
using System.Collections.Generic;
using BerlinClock.Dtos;
using BerlinClock.Services;
using BerlinClock.Services.Interfaces;

namespace BerlinClock.Classes
{
    public class TimeConverter : ITimeConverter
    {
        private readonly ITimeParsingService _timeParsingService;

        private readonly ITimeConversionService _timeConversionService;
        
        public TimeConverter(ITimeParsingService timeParsingService, ITimeConversionService timeConversionService)
        {
            _timeParsingService = timeParsingService;

            _timeConversionService = timeConversionService;
        }

        public string convertTime(string aTime)
        {
            var timeDto = _timeParsingService.ParseTime(aTime);

            var clockState = _timeConversionService.GetClockState(timeDto);

            return clockState;
        }
    }
}
