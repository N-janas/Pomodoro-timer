using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Diagnostics;

namespace PomodoroTimer.ViewModel
{
    using Model;
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
        #endregion

        public TimerViewModel()
        {
            // Set starter timer
            Update();
            // Set mode 
            WorkTimeChecked = true;
            ShortBreakChecked = LongBreakChecked = false;
            // Attach viewmodel's update as observer
            _pomodoro.IntervalPassed += Update;
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
        public bool WorkTimeChecked
        {
            get { return _workTimeChecked; }
            set { _workTimeChecked = value; onPropertyChanged(nameof(WorkTimeChecked)); }
        }
        public bool ShortBreakChecked
        {
            get { return _shortBreakChecked; }
            set { _shortBreakChecked = value; onPropertyChanged(nameof(ShortBreakChecked)); }
        }
        public bool LongBreakChecked
        {
            get { return _longBreakChecked; }
            set { _longBreakChecked = value; onPropertyChanged(nameof(LongBreakChecked)); }
        }
        #endregion

        #region Methods
        // Updating time
        private void Update()
        {
            Minutes = CalculateMinutes(_pomodoro.CurrentTime);
            Seconds = CalculateSeconds(_pomodoro.CurrentTime, Minutes);
        }
        // Get minutes of time
        private int CalculateMinutes(int time) => time / 60;

        // Get rest of seconds
        private int CalculateSeconds(int time, int minutes) => time - minutes*60;

        private void StartPauseF(object sender)
        {
            // Start was clicked
            if (_startButtonContent == res.Start)
            {
                // Start timer
                _pomodoro.Timer.Start();
                // Set other label
                StartButtonContent = res.Pause;
                // Set skip button visible
                SkipVisible = true;
            }
            // Pause was clicked
            else if (_startButtonContent == res.Pause)
            {
                // Pause timer
                _pomodoro.Timer.Stop();
                // Set other label
                StartButtonContent = res.Start;
                // Set skip to hidden
                SkipVisible = false;
            }
        }

        private void WorkTimeOn(object sender)
        {
            // Set Modes
            WorkTimeChecked = true;
            ShortBreakChecked = LongBreakChecked = false;
            // Set in model and update visuals
            _pomodoro.SetTimeMode(_pomodoro.WorkTime);
            Update();
        }
        private void ShortBreakOn(object sender)
        {
            // Set Modes
            ShortBreakChecked = true;
            WorkTimeChecked = LongBreakChecked = false;
            // Set in model and update visuals
            _pomodoro.SetTimeMode(_pomodoro.ShortBreak);
            Update();
        }
        private void LongBreakOn(object sender)
        {
            // Set Modes
            LongBreakChecked = true;
            ShortBreakChecked = WorkTimeChecked = false;
            // Set in model and update visuals
            _pomodoro.SetTimeMode(_pomodoro.LongBreak);
            Update();
        }

        private void SkipF(object sender)
        {

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

        #endregion
    }
}
