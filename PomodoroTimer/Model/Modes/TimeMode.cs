using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroTimer.Model.Modes
{
    public abstract class TimeMode
    {
        public int Duration { get; set; }
        protected static int GetSeconds(int mins) => 3;
        public abstract TimeMode NextMode(BreaksCollection breaksCollection);

    }
}
