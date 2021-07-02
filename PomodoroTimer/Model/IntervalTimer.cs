using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Diagnostics;

namespace PomodoroTimer.Model
{
    public class IntervalTimer : ITimer
    {
        private Timer _timer;
        public int Interval { get; set; } = 1000;
        public event ElapsedEventHandler Elapsed
        {
            add { _timer.Elapsed += value; }
            remove { _timer.Elapsed -= value; }
        }

        public IntervalTimer()
        {
            _timer = new Timer();

        }

        #region Methods
        public void Start()
        {
            _timer.Interval = Interval;
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
        #endregion
    }
}
