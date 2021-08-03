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

        public event Action Opening;
        public event Action Exiting;

        public NotifyIconWrapper()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            _notifyIcon = new NotifyIcon
            {
                Icon = Properties.Resources._590779,
                Visible = true,
                ContextMenuStrip = CreateContextMenu()
            };
        }

        private void OpenItemOnClick(object sender, EventArgs e)
        {
            Opening?.Invoke();
        }

        private void ExitItemOnClick(object sender, EventArgs e)
        {
            Exiting?.Invoke();
        }

        public void Dispose()
        {
            _notifyIcon.Dispose();
        }

        private ContextMenuStrip CreateContextMenu()
        {
            var openItem = new ToolStripMenuItem("Open");
            openItem.Click += OpenItemOnClick;

            var exitItem = new ToolStripMenuItem("Exit");
            exitItem.Click += ExitItemOnClick;

            var contextMenu = new ContextMenuStrip { Items = { openItem, exitItem } };
            return contextMenu;
        }
    }
}
