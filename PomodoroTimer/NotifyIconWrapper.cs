using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.ComponentModel;

namespace PomodoroTimer
{
    public class NotifyIconWrapper : FrameworkElement, IDisposable
    {
        private readonly NotifyIcon _notifyIcon;

        public NotifyIconWrapper()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;
            _notifyIcon = new NotifyIcon
            {

            };
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
