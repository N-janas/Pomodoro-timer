using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroTimer.Model.Modes
{
    using Settings = Properties.Settings;
    class LongBreak : TimeMode
    {
        public LongBreak()
        {
            Duration = GetSeconds(Settings.Default.LongBreakMins);
        }

        public override TimeMode NextMode(BreaksCollection breaksCollection = null)
        {
            return new WorkTime();
        }
    }
}
