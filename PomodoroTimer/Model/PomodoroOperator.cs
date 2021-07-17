using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Diagnostics;

namespace PomodoroTimer.Model
{
    using Modes;
    using Settings = Properties.Settings;
    public class PomodoroOperator
    {
        private BreaksCollection _breaksCollection = new BreaksCollection();
        public event Action IntervalPassed;
        public event Action ModeChanged;
        public int ShortBreakAmount { get; private set; } = Settings.Default.ShortBreaksCount;
        public TimeMode CurrentMode { get; private set; }
        public int CurrentTime { get; private set; }
        public ITimer Timer { get; }

        public PomodoroOperator(ITimer timer)
        {
            Timer = timer;
            Timer.Elapsed += CountDown;
            SetTimeMode(new WorkTime());
            FillBreaksOrder();
        }

        #region Methods 
        private void FillBreaksOrder()
        {
            if (Settings.Default.LongBreaksAllowed)
            {
                for (int i = 0; i < ShortBreakAmount; i++)
                {
                    _breaksCollection.AddItem(new ShortBreak());
                }
                _breaksCollection.AddItem(new LongBreak());
            }
            else
                _breaksCollection.AddItem(new ShortBreak());
        }

        public void CountDown(Object source, ElapsedEventArgs e)
        {
            CurrentTime -= 1;
            IntervalPassed?.Invoke();
            if (CurrentTime <= 0)
                EndMode();
        }

        public void Skip()
        {
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
            SetTimeMode(CurrentMode.NextMode(_breaksCollection));
        }

        public void SetTimeMode(TimeMode timeMode)
        {
            CurrentMode = timeMode;
            CurrentTime = CurrentMode.Duration;
            ModeChanged?.Invoke();
            IntervalPassed?.Invoke();
        }
        #endregion
    }


}
