using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Diagnostics;
using System.Windows;

namespace PomodoroTimer.ViewModel
{
    using Model;
    using Model.Modes;
    using BaseClasses;
    // Resources' alias
    using res = Properties.Resources;
    using Settings = Properties.Settings;

    public class TimerViewModel : ViewModelBase
    {
        #region Fields
        private PomodoroOperator _pomodoro = new PomodoroOperator(new IntervalTimer());
        private int _minutes;
        private int _seconds;
        private string _startButtonContent;
        private bool _skipVisible;
        private TimeMode _visibleTimeMode;
        private readonly NavigationMediator _navigationMediator;
        private bool _longBreakModeVisible;
        private bool _isWorkTimeOn = true;
        private bool _isShortBreakOn = false;
        private bool _isLongBreakOn = false;
        #endregion

        public TimerViewModel(NavigationMediator navigationMediator)
        {
            UpdateTime();
            UpdateVisibleMode();

            _longBreakModeVisible = Settings.Default.LongBreaksAllowed;
            // Attach viewmodel's update as observer
            _pomodoro.IntervalPassed += UpdateTime;

            _pomodoro.ModeChanged += OnModeChanged;

            _pomodoro.TimerStarted += OnTimerStarted;
            _pomodoro.TimerStopped += OnTimerStopped;
            // Assign starter label 
            _startButtonContent = res.Start;
            // Assign starter button visibility
            _skipVisible = false;
            _navigationMediator = navigationMediator;
        }

        #region Properties
        public int Minutes
        {
            get { return _minutes; }
            set { _minutes = value; OnPropertyChanged(nameof(Minutes)); }
        }
        public int Seconds
        {
            get { return _seconds; }
            set { _seconds = value; OnPropertyChanged(nameof(Seconds)); }

        }
        public string StartButtonContent
        {
            get { return _startButtonContent; }
            set { _startButtonContent = value; OnPropertyChanged(nameof(StartButtonContent)); }
        }
        public bool SkipVisible
        {
            get { return _skipVisible; }
            set { _skipVisible = value; OnPropertyChanged(nameof(SkipVisible)); }
        }
        public TimeMode VisibleTimeMode
        {
            get { return _visibleTimeMode; }
            set { _visibleTimeMode = value; OnPropertyChanged(nameof(VisibleTimeMode)); }
        }
        public bool LongBreakModeVisible
        {
            get => _longBreakModeVisible;
            set { _longBreakModeVisible = value; OnPropertyChanged(nameof(LongBreakModeVisible)); }
        }
        public bool IsWorkTimeOn { get => _isWorkTimeOn; set { _isWorkTimeOn = value; OnPropertyChanged(nameof(IsWorkTimeOn)); } }
        public bool IsShortBreakOn { get => _isShortBreakOn; set { _isShortBreakOn = value; OnPropertyChanged(nameof(IsShortBreakOn)); } }
        public bool IsLongBreakOn { get => _isLongBreakOn; set { _isLongBreakOn = value; OnPropertyChanged(nameof(IsLongBreakOn)); } }

        #endregion

        #region Methods
        private void OnTimerStopped()
        {
            StopClockUpdateView();
        }

        private void OnTimerStarted()
        {
            StartClockUpdateView();
        }

        private void OnModeChanged()
        {
            UpdateVisibleMode();
            UpdateTime();
        }

        private void StopClockUpdateView()
        {
            StartButtonContent = res.Start;
            SkipVisible = false;
        }

        private void StartClockUpdateView()
        {
            StartButtonContent = res.Pause;
            SkipVisible = true;
        }

        private void UpdateTime()
        {
            Minutes = CalculateMinutes(_pomodoro.CurrentTime);
            Seconds = CalculateSeconds(_pomodoro.CurrentTime, Minutes);
        }

        private int CalculateMinutes(int time) => time / 60;

        private int CalculateSeconds(int time, int minutes) => time - minutes * 60;

        private void UpdateVisibleMode()
        {
            VisibleTimeMode = _pomodoro.CurrentMode;
        }
        private void StartPauseF(object sender)
        {
            // Start was clicked
            if (_startButtonContent == res.Start)
            {
                _pomodoro.StartTimer();
            }
            // Pause was clicked
            else if (_startButtonContent == res.Pause)
            {
                _pomodoro.StopTimer();
            }
        }

        private void WorkTimeOn(object sender)
        {
            TrySetModeOn(new WorkTime());
        }
        private void ShortBreakOn(object sender)
        {
            TrySetModeOn(new ShortBreak());
        }
        private void LongBreakOn(object sender)
        {
            TrySetModeOn(new LongBreak());
        }

        private void TrySetModeOn(TimeMode mode)
        {
            if (!VisibleTimeMode.GetType().Equals(mode.GetType()))
            {
                if (_pomodoro.Timer.IsRunning())
                {
                    _pomodoro.StopTimer();

                    var result = ShowChangeModeDialog();

                    if (result == MessageBoxResult.Yes)
                    {
                        SetVisibleTimeMode(mode);
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        _pomodoro.StartTimer();
                    }
                }
                else
                {
                    SetVisibleTimeMode(mode);
                }
            }
        }

        private void SkipF(object sender)
        {
            _pomodoro.Skip();
        }

        public void SetVisibleTimeMode(TimeMode timeMode)
        {
            _pomodoro.SetTimeMode(timeMode);
        }

        private MessageBoxResult ShowChangeModeDialog()
        {
            return MessageBox.Show(
                "It will cancel current mode.",
                "Are you sure?",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );
        }
        private void GoToSettingsF(object sender)
        {
            _pomodoro.StopTimer();
            _navigationMediator.CurrentViewModel = new SettingsViewModel(_navigationMediator, this);
        }

        #endregion

        #region Commands
        private ICommand _startPause = null;
        public ICommand StartPause
        {
            get
            {
                if (_startPause == null)
                {
                    _startPause = new RelayCommand(StartPauseF, arg => true);
                }
                return _startPause;
            }
        }

        private ICommand _skip = null;
        public ICommand Skip
        {
            get
            {
                if (_skip == null)
                {
                    _skip = new RelayCommand(SkipF, arg => true);
                }
                return _skip;
            }
        }

        private ICommand _turnOnWorkTime = null;
        public ICommand TurnOnWorkTime
        {
            get
            {
                if (_turnOnWorkTime == null)
                {
                    _turnOnWorkTime = new RelayCommand(WorkTimeOn, arg => true);
                }
                return _turnOnWorkTime;
            }
        }

        private ICommand _turnOnShortBreak = null;
        public ICommand TurnOnShortBreak
        {
            get
            {
                if (_turnOnShortBreak == null)
                {
                    _turnOnShortBreak = new RelayCommand(ShortBreakOn, arg => true);
                }
                return _turnOnShortBreak;
            }
        }

        private ICommand _turnOnLongBreak = null;
        public ICommand TurnOnLongBreak
        {
            get
            {
                if (_turnOnLongBreak == null)
                {
                    _turnOnLongBreak = new RelayCommand(LongBreakOn, arg => true);
                }
                return _turnOnLongBreak;
            }
        }

        private ICommand _goToSettings = null;

        public ICommand GoToSettings
        {
            get
            {
                if (_goToSettings == null)
                {
                    _goToSettings = new RelayCommand(GoToSettingsF, arg => true);
                }
                return _goToSettings;
            }
        }



        #endregion
    }
}
