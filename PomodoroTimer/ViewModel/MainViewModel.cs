using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PomodoroTimer.ViewModel
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;
    using ViewModel;
    using ViewModel.BaseClasses;
    using Settings = Properties.Settings;
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationMediator _navigationMediator;
        public ViewModelBase CurrentViewModel => _navigationMediator.CurrentViewModel;
        private WindowState _windowState;
        private bool _showInTaskbar;
        private readonly NotifyIconWrapper _notifyIconWrapper;


        public WindowState WindowState
        {
            get => _windowState;
            set
            {
                _windowState = value;
                ShowInTaskbar = true;
                OnPropertyChanged(nameof(WindowState));
            }
        }
        public bool ShowInTaskbar
        {
            get => _showInTaskbar;
            set
            {
                _showInTaskbar = value;
                OnPropertyChanged(nameof(ShowInTaskbar));
            }
        }

        private ICommand _closingWindow = null;
        public ICommand ClosingWindow
        {
            get
            {
                if (_closingWindow == null)
                {
                    _closingWindow = new RelayCommand<CancelEventArgs>(ClosingWindowF, arg => true);
                }
                return _closingWindow;
            }
        }

        public MainViewModel(NavigationMediator navigationMediator)
        {
            _navigationMediator = navigationMediator;
            _navigationMediator.CurrentViewModelChanged += OnCurrentViewModelChanged;

            _windowState = WindowState.Normal;
            _showInTaskbar = true;

            if (Settings.Default.MinimizeOnClosing)
            {
                _notifyIconWrapper = new NotifyIconWrapper();

                _notifyIconWrapper.Opening += OnOpening;
                _notifyIconWrapper.Exiting += OnExiting;

                _closingWindow = new RelayCommand<CancelEventArgs>(ClosingWindowF, arg => true);
            }
            else
            {
                _notifyIconWrapper = null;
                _closingWindow = new RelayCommand<CancelEventArgs>(DoNothing, arg => false);

            }
        }

        private void OnOpening()
        {
            WindowState = WindowState.Normal;
        }

        private void OnExiting()
        {
            _notifyIconWrapper?.Dispose();
            Application.Current.Shutdown();
        }

        private void ClosingWindowF(CancelEventArgs e)
        {
            if (e != null)
            {
                e.Cancel = true;
                WindowState = WindowState.Minimized;
                ShowInTaskbar = false;
            }
        }

        private void DoNothing(CancelEventArgs e) { }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
