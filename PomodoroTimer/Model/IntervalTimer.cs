using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Diagnostics;

namespace PomodoroTimer.Model
{
    class IntervalTimer
    {
        public event Action IntervalPassed;

        public int WorkTime { get; private set; } = getSeconds(15);
        public int ShortBreak { get; private set; } = getSeconds(6);
        public int LongBreak { get; private set; } = getSeconds(10);
        public int CurrentTime { get; private set; }
        public Timer Timer { get; private set; }

        public IntervalTimer()
        {
            // Set new timer (interval 1 sec)
            Timer = new Timer(1000);
            // Assign countdown for the interval event 
            Timer.Elapsed += CountDown;
            // Assign starting time as work time
            CurrentTime = WorkTime;
        }

        #region Methods
        // Get miliseconds from minutes
        private static int getSeconds(int mins) => mins * 60;

        // Countdown time and notify viewmodel 
        private void CountDown(Object source, ElapsedEventArgs e)
        {
            CurrentTime -= 1;
            // Notify
            IntervalPassed?.Invoke();
            // Stop when time ended
            if (CurrentTime <= 0)
                ResetTimer();
        }

        private void ResetTimer()
        {
            Timer.Stop();
            // Change to next interval
        }

        public void setTimeMode(int timeMode)
        {
            CurrentTime = timeMode;
        }

        #endregion
    }
}
