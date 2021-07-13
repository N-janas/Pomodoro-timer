using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroTimer
{
    using ViewModel;
    using ViewModel.BaseClasses;
    public class NavigationMediator
    {
        public event Action CurrentViewModelChanged;

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewMOdelChanged();
            }
        }

        private void OnCurrentViewMOdelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
