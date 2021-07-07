using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroTimer.Model.Modes
{
    class LongBreak : TimeMode
    {
        public LongBreak()
        {
            Duration = GetSeconds(10);
        }

        public override TimeMode NextMode(BreaksCollection breaksCollection = null)
        {
            return new WorkTime();
        }
    }
}
