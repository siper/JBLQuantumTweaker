using JblQuantumTweaker.Properties;
using System;
using System.Windows.Forms;

namespace JblQuantumTweaker
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MyCustomApplicationContext());
        }
    }

    public class MyCustomApplicationContext : ApplicationContext
    {
        private NotifyIcon trayIcon;

        public MyCustomApplicationContext()
        {
            trayIcon = new NotifyIcon()
            {
                Icon = Resources.AppIcon,
                ContextMenu = new ContextMenu(
                    new MenuItem[] {
                        new MenuItem("Exit", Exit)
                    }
                ),
                Visible = true
            };
            trayIcon.MouseClick += new MouseEventHandler(_TrayIcon_Click);
            MainForm = new MainForm();
        }

        private void _TrayIcon_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MainForm.Show();
                MainForm.WindowState = FormWindowState.Normal;
            }
        }

        void Exit(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            trayIcon.Visible = false;

            Application.Exit();
        }
    }
}
