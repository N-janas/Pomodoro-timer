using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;

namespace PomodoroTimer
{
    public class NotifyIconWrapper : FrameworkElement, IDisposable
    {
        private readonly NotifyIcon _notifyIcon;

        //private static readonly RoutedEvent OpenSelectedEvent = EventManager.RegisterRoutedEvent("OpenSelected",
        //    RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(NotifyIconWrapper));

        //private static readonly RoutedEvent ExitSelectedEvent = EventManager.RegisterRoutedEvent("ExitSelected",
        //    RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(NotifyIconWrapper));

        public NotifyIconWrapper()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            _notifyIcon = new NotifyIcon
            {
                Icon = Properties.Resources._590779,
                Visible = true,
            };
            _notifyIcon.ContextMenuStrip.Items.Add("Open", null, OpenItemOnClick);
            _notifyIcon.ContextMenuStrip.Items.Add("Exit", null, ExitItemOnClick);
        }

        private void ExitItemOnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OpenItemOnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _notifyIcon.Dispose();
        }
    }
}
