using System;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using BerlinClock.Classes;
using BerlinClock.Services;

namespace BerlinClock
{
    [Binding]
    public class TheBerlinClockSteps
    {   
        //
        //We should use dependency injection instead of instantiation
        //
        private ITimeConverter berlinClock = new TimeConverter(new TimeParsingService(), new TimeConversionService());
        private String theTime;

        
        [When(@"the time is ""(.*)""")]
        public void WhenTheTimeIs(string time)
        {
            theTime = time;
        }
        
        [Then(@"the clock should look like")]
        public void ThenTheClockShouldLookLike(string theExpectedBerlinClockOutput)
        {
            Assert.AreEqual(berlinClock.convertTime(theTime), theExpectedBerlinClockOutput);
        }

    }
}
