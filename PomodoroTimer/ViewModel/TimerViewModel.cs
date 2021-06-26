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
        private int _minutes;
        private int _seconds;
        private string _startButtonContent = res.Start;
        private bool _skipVisible;
        private IntervalTimer _timer = new IntervalTimer();
        #endregion

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
