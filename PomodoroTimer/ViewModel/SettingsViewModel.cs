using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PomodoroTimer.ViewModel
{
    using BaseClasses;
    using System.Windows.Input;
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

        private void SaveSettings(object sender)
        {
            Settings.Default.WorkTimeMins = _workTime;
            Settings.Default.ShortBreakMins = _shortBreakTime;
            Settings.Default.LongBreakMins = _longBreakTime;
            Settings.Default.LongBreaksAllowed = _isLongBreakAllowed;
            Settings.Default.ShortBreaksCount = _shortBreakCount;

            LeaveSettings(new TimerViewModel(_navigationMediator));
        }

        private void CancelSaving(object sender)
        {
            LeaveSettings(_previousTimerState);
        }

        private void LeaveSettings(ViewModelBase viewModel)
        {
            _navigationMediator.CurrentViewModel = viewModel;
        }

        private ICommand _save = null;

        public ICommand Save
        {
            get
            {
                if (_save == null)
                {
                    _save = new RelayCommand(SaveSettings, arg => true);
                }
                return _save;
            }
        }

        private ICommand _cancel = null;

        public ICommand Cancel
        {
            get
            {
                if (_cancel == null)
                {
                    _cancel = new RelayCommand(CancelSaving, arg => true);
                }
                return _cancel;
            }
        }

    }
}
