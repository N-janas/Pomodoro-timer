using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Diagnostics;

namespace PomodoroTimer.Model
{
    public class PomodoroOperator
    {
        private BreaksCollection _breaksCollection = new BreaksCollection();
        public event Action IntervalPassed;
        public bool IsWorkTime { get; private set; } = true;
        public bool IsShortBreakTime { get; private set; } = false;
        public bool IsLongBreakTime { get; private set; } = false;
        public int ShortBreakAmount { get; private set; } = 3;
        public int WorkTime { get; private set; } = GetSeconds(15);
        public int ShortBreak { get; private set; } = GetSeconds(6);
        public int LongBreak { get; private set; } = GetSeconds(10);
        public int CurrentTime { get; private set; }
        public ITimer Timer { get; }

        public PomodoroOperator(ITimer timer)
        {
            Timer = timer;
            Timer.Elapsed += CountDown;
            CurrentTime = WorkTime;
            FillBreaksOrder();
        }

        #region Methods 
        private static int GetSeconds(int mins) => mins * 60;

        private void FillBreaksOrder()
        {
            for (int i = 0; i < ShortBreakAmount; i++)
            {
                _breaksCollection.AddItem(Modes.ShortB);
            }
            _breaksCollection.AddItem(Modes.LongB);
        }

        public void CountDown(Object source, ElapsedEventArgs e)
        {
            CurrentTime -= 1;
            IntervalPassed?.Invoke();
            if (CurrentTime <= 0)
                EndMode();
        }

        private void EndMode()
        {
            Timer.Stop();
            CurrentTime = 0;
            NextMode();
        }

        private void NextMode()
        {
            if (IsWorkTime)
            {
                _breaksCollection.GetEnumerator().MoveNext();
                // If Current is ShortB, else LongB
                if (Modes.ShortB.CompareTo(_breaksCollection.GetEnumerator().Current) == 0)
                {
                    IsShortBreakTime = true;
                    IsWorkTime = IsLongBreakTime = false;
                }
                else
                {
                    IsLongBreakTime = true;
                    IsWorkTime = IsShortBreakTime = false;
                }
            } 
            else if (IsShortBreakTime || IsLongBreakTime)
            {
                IsShortBreakTime = IsLongBreakTime = false;
                IsWorkTime = true;
            }
        }

        public void SetTimeMode(int timeMode)
        {
            CurrentTime = timeMode;
        }

        public void Skip()
        {
            NextMode();
        }
        #endregion
    }


}
