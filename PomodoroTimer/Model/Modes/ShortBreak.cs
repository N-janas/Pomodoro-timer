using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroTimer.Model.Modes
{
    class ShortBreak : TimeMode
    {
        public ShortBreak()
        {
            Duration = GetSeconds(5);
        }

        public override TimeMode NextMode(BreaksCollection breaksCollection = null)
        {
            return new WorkTime();
        }
    }
}
