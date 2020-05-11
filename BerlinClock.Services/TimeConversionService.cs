using System.Collections.Generic;
using BerlinClock.Dtos;
using BerlinClock.Services.Interfaces;

namespace BerlinClock.Services
{
    public class TimeConversionService : ITimeConversionService
    {
        private const string YellowLight = "Y";
        private const string RedLight = "R";
        private const string NoLight = "O";
        private const string LineBreak = "\r\n";

        private const int LampWeight = 5;
        private const int FirstRowLampsCount = 4;
        private const int SecondRowLampsCount = 4;
        private const int ThirdRowLampsCount = 11;
        private const int FourthRowLampsCount = 4;
        private const int ThirdRowFirstRedLampDigit = 3;
        private const int ThirdRowSecondRedLampDigit = 6;
        private const int ThirdRowThirdRedLampDigit = 9;

        public string GetClockState(TimeDto timeDto)
        {
            var topLampState = GetTopLampState(timeDto);

            var firstRowLampStates = GetFirstRowLampStates(timeDto);

            var secondRowLampStates = GetSecondRowLampStates(timeDto);

            var thirdRowLampStates = GetThirdRowLampStates(timeDto);

            var forthRowLampStates = GetForthRowLampStates(timeDto);


            var clockState =
                $"{topLampState}{LineBreak}" +
                $"{firstRowLampStates}{LineBreak}" +
                $"{secondRowLampStates}{LineBreak}" +
                $"{thirdRowLampStates}{LineBreak}" +
                $"{forthRowLampStates}";

            return clockState;
        }

        private static string GetTopLampState(TimeDto time)
        {
            var secondsSinceMidnight = time.Seconds;

            var lampState = (secondsSinceMidnight % 2 == 0) ? YellowLight : NoLight;

            return lampState;
        }

        private static string GetFirstRowLampStates(TimeDto time)
        {
            var hoursSinceMidnight = time.Hours;

            int fiveHoursCount = hoursSinceMidnight / LampWeight;

            var lampStates = GetLampStates(fiveHoursCount, FirstRowLampsCount, RedLight);

            return lampStates;
        }

        private static string GetSecondRowLampStates(TimeDto time)
        {
            var hoursSinceMidnight = time.Hours;

            int oneHoursCount = hoursSinceMidnight % LampWeight;

            var lampStates = GetLampStates(oneHoursCount, SecondRowLampsCount, RedLight);

            return lampStates;
        }

        private static string GetThirdRowLampStates(TimeDto time)
        {
            var minutesSinceMidnight = time.Minutes;

            int fiveMinutesCount = minutesSinceMidnight / LampWeight;

            var states = new List<string>();

            for (int index = 1; index <= ThirdRowLampsCount; index++)
            {
                var lampState = index <= fiveMinutesCount ? YellowLight : NoLight;

                if ((index == 3 || index == 6 || index == 9) && lampState == YellowLight)
                {
                    lampState = RedLight;
                }

                states.Add(lampState);
            }

            var lampStates = string.Join(string.Empty, states);

            var lampStatesWithYellowColor = GetLampStates(fiveMinutesCount, ThirdRowLampsCount, YellowLight);

            var lampStatesWithYellowAndRedColor = AddRedColorToYellowLampStates(lampStatesWithYellowColor, ThirdRowLampsCount);

            return lampStatesWithYellowAndRedColor;
        }

        private static string GetForthRowLampStates(TimeDto time)
        {
            var minutesSinceMidnight = time.Minutes;

            int oneMinutesCount = minutesSinceMidnight % LampWeight;

            var lampStates = GetLampStates(oneMinutesCount, FourthRowLampsCount, YellowLight);

            return lampStates;
        }

        private static string GetLampStates(int timeInterval, int lampsCount, string lampLight)
        {
            var states = new List<string>();

            for (int index = 1; index <= lampsCount; index++)
            {
                var lampState = index <= timeInterval ? lampLight : NoLight;

                states.Add(lampState);
            }

            var lampStates = string.Join(string.Empty, states);

            return lampStates;
        }

        private static string AddRedColorToYellowLampStates(string lampStatesWithYellowColor, int thirdRowLampsCount)
        {
            var thirdLampIndex = GetLampIndex(ThirdRowFirstRedLampDigit);
            var sixthLampIndex = GetLampIndex(ThirdRowSecondRedLampDigit);
            var ninthLampIndex = GetLampIndex(ThirdRowThirdRedLampDigit);
            int index = 0;

            var states = new List<string>();

            while (index < thirdRowLampsCount)
            {
                var lampState = lampStatesWithYellowColor[index].ToString();

                if ((index == thirdLampIndex || index == sixthLampIndex || index == ninthLampIndex) && lampState == YellowLight)
                {
                    lampState = RedLight;
                }

                states.Add(lampState);

                index++;
            }

            var lampStates = string.Join(string.Empty, states);

            return lampStates;
        }

        private static int GetLampIndex(int lampIndex)
        {
            var indexCorrectionValue = 1;

            var correctedLampIndex = lampIndex - indexCorrectionValue;

            return correctedLampIndex;
        }
    }
}
