using JblQuantumTweaker.Properties;
using System;
using System.Windows.Forms;

namespace JblQuantumTweaker
{
    internal static class Program
    {
        private static MyCustomApplicationContext applicationContext;

        public static bool hidConnected { get; set; }
        public static bool headphonesConnected { get; set; }

        public static int lBattery { get; set; }
        public static int rBattery { get; set; }
        public static int caseBattery { get; set; }

        public static void updateTooltip()
        {
            if (!hidConnected) {
                applicationContext.trayIcon.Text = "HID не подключен";
                return;
            }
            if (!headphonesConnected)
            {
                applicationContext.trayIcon.Text = "Наушники не подключены";
                return;
            }
            applicationContext.trayIcon.Text = string.Format("Левый: {0}%\nПравый: {1}%\nКейс: {2}%", lBattery, rBattery, caseBattery);
        }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            applicationContext = new MyCustomApplicationContext();
            Application.Run(applicationContext);
        }
    }

    public class MyCustomApplicationContext : ApplicationContext
    {
        public NotifyIcon trayIcon;

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
            trayIcon.Text = "Test";
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
