using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Diagnostics;

namespace PomodoroTimer.Model
{
    class IntervalTimer {
        public int WorkTime { get; private set; } = getMiliseconds(15);
        public int ShortBreak { get; private set; } = getMiliseconds(6);
        public int LongBreak { get; private set; } = getMiliseconds(10);
        public int CurrentTime { get; private set; }
        public Timer Timer { get; private set; }

        public IntervalTimer()
        {
            // Set new timer (interval 1 sec)
            Timer = new Timer(1000);
            // Assign countdown for the interval event and starting time as work time
            Timer.Elapsed += CountDown;
            CurrentTime = WorkTime;
        }

        #region Methods
        // Get miliseconds from minutes
        private static int getMiliseconds(int mins) => mins * 60 * 1000;

        private void CountDown(Object source, ElapsedEventArgs e)
        {
            CurrentTime -= 1;
        }

        public void setTime()
        {
            // CurrentTime = ;
        }
        #endregion
    }
}
