using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroTimer.ViewModel
{
    using BaseClasses;
    using ViewModel;
    public class SettingsViewModel : ViewModelBase
    {
        private readonly NavigationMediator _navigationMediator;
        private readonly TimerViewModel _previousTimerState;

        public SettingsViewModel(NavigationMediator navigationMediator, TimerViewModel timerViewModel)
        {
            _navigationMediator = navigationMediator;
            _previousTimerState = timerViewModel;
        }
    }
}
