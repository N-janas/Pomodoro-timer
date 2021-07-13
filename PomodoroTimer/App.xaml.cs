using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PomodoroTimer
{
    using ViewModel;
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            NavigationMediator navigationMediator = new NavigationMediator();

            navigationMediator.CurrentViewModel = new TimerViewModel(navigationMediator);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationMediator)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
