using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroTimer.Model
{
    interface IObserver
    {
        void Update(ITimer timer);
    }
}
