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

        private string _workTime;
        private string _shortBreakTime;
        private string _longBreakTime;
        private bool _isLongBreakAllowed;
        private string _shortBreakCount;



        public string WorkTime { get => _workTime; set { _workTime = value; OnPropertyChanged(nameof(WorkTime)); } }
        public string ShortBreakTime { get => _shortBreakTime; set { _shortBreakTime = value; OnPropertyChanged(nameof(ShortBreakTime)); } }
        public string LongBreakTime { get => _longBreakTime; set { _longBreakTime = value; OnPropertyChanged(nameof(LongBreakTime)); } }
        public bool IsLongBreakAllowed { get => _isLongBreakAllowed; set { _isLongBreakAllowed = value; OnPropertyChanged(nameof(IsLongBreakAllowed)); } }
        public string ShortBreakCount { get => _shortBreakCount; set { _shortBreakCount = value; OnPropertyChanged(nameof(ShortBreakCount)); } }


        public SettingsViewModel(NavigationMediator navigationMediator, TimerViewModel timerViewModel)
        {
            _navigationMediator = navigationMediator;
            _previousTimerState = timerViewModel;

            _workTime = Settings.Default.WorkTimeMins.ToString();
            _shortBreakTime = Settings.Default.ShortBreakMins.ToString();
            _longBreakTime = Settings.Default.LongBreakMins.ToString();
            _isLongBreakAllowed = Settings.Default.LongBreaksAllowed;
            _shortBreakCount = Settings.Default.ShortBreaksCount.ToString();
        }

        private void SaveSettings(object sender)
        {
            if (int.TryParse(_workTime, out int number))
            {
                Settings.Default.WorkTimeMins = number;
            }

            if (int.TryParse(_shortBreakTime, out number))
            {
                Settings.Default.ShortBreakMins = number;
            }

            if (int.TryParse(_longBreakTime, out number))
            {
                Settings.Default.LongBreakMins = number;
            }

            if (int.TryParse(_shortBreakCount, out number))
            {
                Settings.Default.ShortBreaksCount = number;
            }

            Settings.Default.LongBreaksAllowed = _isLongBreakAllowed;

            Settings.Default.Save();

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
