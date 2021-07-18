using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroTimer.ViewModel
{
    using BaseClasses;
    using ViewModel;
    using Settings = Properties.Settings;
    public class SettingsViewModel : ViewModelBase
    {
        private readonly NavigationMediator _navigationMediator;
        private readonly TimerViewModel _previousTimerState;

        private int _workTime;
        private int _shortBreakTime;
        private int _longBreakTime;
        private bool _isLongBreakAllowed;
        private int _shortBreakCount;

        public int WorkTime { get => _workTime; set { _workTime = value; OnPropertyChanged(nameof(WorkTime)); } }
        public int ShortBreakTime { get => _shortBreakTime; set { _shortBreakTime = value; OnPropertyChanged(nameof(ShortBreakTime)); } }
        public int LongBreakTime { get => _longBreakTime; set { _longBreakTime = value; OnPropertyChanged(nameof(LongBreakTime)); } }
        public bool IsLongBreakAllowed { get => _isLongBreakAllowed; set { _isLongBreakAllowed = value; OnPropertyChanged(nameof(IsLongBreakAllowed)); } }
        public int ShortBreakCount { get => _shortBreakCount; set { _shortBreakCount = value; OnPropertyChanged(nameof(ShortBreakCount)); } }


        public SettingsViewModel(NavigationMediator navigationMediator, TimerViewModel timerViewModel)
        {
            _navigationMediator = navigationMediator;
            _previousTimerState = timerViewModel;

            _workTime = Settings.Default.WorkTimeMins;
            _shortBreakTime = Settings.Default.ShortBreakMins;
            _longBreakTime = Settings.Default.LongBreakMins;
            _isLongBreakAllowed = Settings.Default.LongBreaksAllowed;
            _shortBreakCount = Settings.Default.ShortBreaksCount;
        }


    }
}
