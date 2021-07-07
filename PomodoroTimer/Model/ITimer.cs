using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PomodoroTimer.Model
{
    public interface ITimer
    {
        int Interval { get; set; }
        void Start();
        void Stop();
        bool IsRunning();
        event ElapsedEventHandler Elapsed;
    }
}
