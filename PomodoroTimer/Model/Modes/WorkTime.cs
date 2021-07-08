using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroTimer.Model.Modes
{
    using Settings = Properties.Settings;
    class WorkTime : TimeMode
    {
        public WorkTime()
        {
            Duration = GetSeconds(Settings.Default.WorkTimeMins);
        }
        public override TimeMode NextMode(BreaksCollection breaksCollection)
        {
            breaksCollection.GetEnumerator().MoveNext();
            return breaksCollection.GetEnumerator().Current as TimeMode;
        }
    }
}
