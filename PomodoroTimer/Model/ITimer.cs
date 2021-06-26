using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroTimer.Model
{
    interface ITimer
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }
}
