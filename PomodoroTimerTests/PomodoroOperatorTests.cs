using System;
using Xunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FakeItEasy;
using System.Diagnostics;
using System.Timers;
using PomodoroTimer.Model;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace PomodoroTimerTests
{
    public class PomodoroOperatorTests
    {
        [Fact]
        public void CountDown_CurrentTimeGreaterThanZero_SubtractIntervalFromCurrentTime()
        {
            // Arange
            const double MILISECONDS_DIVIDER = 1000.0;
            var fakeTimer = new PomodoroOperator(new IntervalTimer());
            var starterTime = fakeTimer.CurrentTime;
            double interval = fakeTimer.Timer.Interval / MILISECONDS_DIVIDER;
            // Act
            fakeTimer.CountDown(this, null);
            // Assert
            Assert.AreEqual(starterTime - interval, fakeTimer.CurrentTime);
        }
    }
}
