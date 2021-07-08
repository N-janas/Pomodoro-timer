﻿using System;
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

    class TimerViewModel : ViewModelBase
    {
        #region Fields
        private PomodoroOperator _pomodoro = new PomodoroOperator(new IntervalTimer());
        private int _minutes;
        private int _seconds;
        private string _startButtonContent;
        private bool _skipVisible;
        private bool _workTimeChecked;
        private bool _shortBreakChecked;
        private bool _longBreakChecked;
        private TimeMode _visibleTimeMode;
        #endregion

        public TimerViewModel()
        {
            UpdateTime();
            // Attach viewmodel's update as observer
            _pomodoro.IntervalPassed += UpdateTime;
            _pomodoro.ModeChanged += UpdateVisibleMode;
            // Assign starter label 
            _startButtonContent = res.Start;
            // Assign starter button visibility
            SkipVisible = false;
        }

        #region Properties
        public int Minutes
        {
            get { return _minutes; }
            set { _minutes = value; onPropertyChanged(nameof(Minutes)); }
        }
        public int Seconds
        {
            get { return _seconds; }
            set { _seconds = value; onPropertyChanged(nameof(Seconds)); }

        }
        public string StartButtonContent
        {
            get { return _startButtonContent; }
            set { _startButtonContent = value; onPropertyChanged(nameof(StartButtonContent)); }
        }
        public bool SkipVisible
        {
            get { return _skipVisible; }
            set { _skipVisible = value; onPropertyChanged(nameof(SkipVisible)); }
        }
        public TimeMode VisibleTimeMode
        {
            get { return _visibleTimeMode; }
            set { _visibleTimeMode = value; onPropertyChanged(nameof(VisibleTimeMode)); }
        }
        #endregion

        #region Methods
        private void UpdateTime()
        {
            Minutes = CalculateMinutes(_pomodoro.CurrentTime);
            Seconds = CalculateSeconds(_pomodoro.CurrentTime, Minutes);
        }

        private int CalculateMinutes(int time) => time / 60;

        private int CalculateSeconds(int time, int minutes) => time - minutes*60;

        private void UpdateVisibleMode()
        {
            VisibleTimeMode = _pomodoro.CurrentMode;
        }
        private void StartPauseF(object sender)
        {
            // Start was clicked
            if (_startButtonContent == res.Start)
            {
                _pomodoro.Timer.Start();
                StartClockUpdateView();
            }
            // Pause was clicked
            else if (_startButtonContent == res.Pause)
            {
                _pomodoro.Timer.Stop();
                StopClockUpdateView();
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
            if (_pomodoro.Timer.IsRunning())
            {
                _pomodoro.Timer.Stop();
                var result = ShowChangeModeDialog();
                if (result == MessageBoxResult.Yes)
                {
                    SetVisibleTimeMode(mode);
                    StopClockUpdateView();
                }
                else if (result == MessageBoxResult.No)
                {
                    UpdateVisibleMode();
                    _pomodoro.Timer.Start();
                }
            }
            else
            {
                SetVisibleTimeMode(mode);
            }
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

        private void SkipF(object sender)
        {
            _pomodoro.Skip();
            StopClockUpdateView();
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
                    _turnOnWorkTime = new RelayCommand(WorkTimeOn, arg => !(_pomodoro.CurrentMode is WorkTime)); //!(_pomodoro.CurrentMode is WorkTime)
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
                    _turnOnShortBreak = new RelayCommand(ShortBreakOn, arg => !(_pomodoro.CurrentMode is ShortBreak));
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
                    _turnOnLongBreak = new RelayCommand(LongBreakOn, arg => !(_pomodoro.CurrentMode is LongBreak));
                }
                return _turnOnLongBreak;
            }
        }

        #endregion
    }
}
