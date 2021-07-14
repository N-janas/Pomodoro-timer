﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PomodoroTimer.ViewModel
{
    using ViewModel;
    using ViewModel.BaseClasses;
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationMediator _navigationMediator;
        public ViewModelBase CurrentViewModel => _navigationMediator.CurrentViewModel;

        public MainViewModel(NavigationMediator navigationMediator)
        {
            _navigationMediator = navigationMediator;

            _navigationMediator.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
