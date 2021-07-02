using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Diagnostics;

namespace PomodoroTimer.Model
{
    public class PomodoroOperator
    {
        public event Action IntervalPassed;
        public int WorkTime { get; private set; } = getSeconds(15);
        public int ShortBreak { get; private set; } = getSeconds(6);
        public int LongBreak { get; private set; } = getSeconds(10);
        public int CurrentTime { get; private set; }
        public ITimer Timer { get; }

        public PomodoroOperator(ITimer timer)
        {
            Timer = timer;
            Timer.Elapsed += CountDown;
            CurrentTime = WorkTime;
        }

        #region Methods 
        private static int getSeconds(int mins) => mins * 60;
        public void CountDown(Object source, ElapsedEventArgs e)
        {
            Debug.WriteLine("countdown:");
            CurrentTime -= 1;
            IntervalPassed?.Invoke();
            if (CurrentTime <= 0)
                ResetTimer();
        }
        private void ResetTimer()
        {
            Timer.Stop();
            // Change to next interval
        }
        public void SetTimeMode(int timeMode)
        {
            CurrentTime = timeMode;
        }
        #endregion
    }
}
