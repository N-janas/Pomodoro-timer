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

    class TimerViewModel : ViewModelBase//, IObserver
    {
        #region Fields
        //private int _currentTime;
        private int _minutes;
        private int _seconds;
        private string _startButtonContent;
        private bool _skipVisible;
        private IntervalTimer _timer = new IntervalTimer();
        #endregion

        public TimerViewModel()
        {
            // Set starter timer
            Update();
            // Attach viewmodel's update as observer
            _timer.IntervalPassed += Update;
            // Assign starter label 
            _startButtonContent = res.Start;
            // Assign starter button visibility
            SkipVisible = false;
            //_timer.Attach(this);
            //_currentTime = _timer.WorkTime;
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
        #endregion

        #region Methods
        // Updating time
        public void Update()
        {
            Minutes = calculateMinutes(_timer.CurrentTime);
            Seconds = calculateSeconds(_timer.CurrentTime, Minutes);
        }
        // Get minutes of time
        private int calculateMinutes(int time) => time / 60;

        // Get rest of seconds
        private int calculateSeconds(int time, int minutes) => time - minutes*60;

        private void StartPauseF(object sender)
        {
            // Start was clicked
            if (_startButtonContent == res.Start)
            {
                // Start timer
                _timer.Timer.Start();
                // Set other label
                StartButtonContent = res.Pause;
                // Set skip button visible
                SkipVisible = true;
            }
            // Pause was clicked
            else if (_startButtonContent == res.Pause)
            {
                // Pause timer
                _timer.Timer.Stop();
                // Set other label
                StartButtonContent = res.Start;
                // Set skip to hidden
                SkipVisible = false;
            }
        }

        private void ChangeMode()
        {

        }

        private void Skip()
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
        #endregion
    }
}
