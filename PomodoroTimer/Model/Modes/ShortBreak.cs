using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroTimer.Model.Modes
{
    using Settings = Properties.Settings;
    class ShortBreak : TimeMode
    {
        public ShortBreak()
        {
            Duration = GetSeconds(Settings.Default.ShortBreakMins);
        }

        public override TimeMode NextMode(BreaksCollection breaksCollection = null)
        {
            return new WorkTime();
        }
    }
}
