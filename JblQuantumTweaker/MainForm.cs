using CoreAudio;
using HidLibrary;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows.Forms;

namespace JblQuantumTweaker
{
    public partial class MainForm : Form
    {
        HidDevice _device;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            setStartup();
            setDevices();
            listenHidDevice();
            this.WindowState = FormWindowState.Minimized;
        }

        private void listenHidDevice()
        {
            int VendorId = 0x0ECB;
            int ProductId = 0x208A;
            var allDevices = HidDevices.Enumerate(VendorId, ProductId);

            _device = allDevices.FirstOrDefault();
            if (_device == null || !_device.IsConnected)
            {
                this.deviceStatusLable.Text = "Устройство не подключено";
                return;
            }
            if (!_device.IsOpen)
            {
                _device.OpenDevice();
            }
            Program.hidConnected = true;
            _device.MonitorDeviceEvents = true;
            _device.ReadReport(OnReport);
            this.deviceStatusLable.Text = "Устройство подключено";
        }

        bool previousActive = false;

        private void OnReport(HidReport report)
        {
            var reportData = report.Data;
            var id = report.ReportId;
            byte[] data = report.Data.Take(6).ToArray();

            if (data[1] != Program.lBattery && data[1] != 0)
            {
                Program.lBattery = data[1];
                Program.updateTooltip();
            }

            if (data[3] != Program.rBattery)
            {
                Program.rBattery = data[3];
                Program.updateTooltip();
            }

            if (data[5] != Program.caseBattery)
            {
                Program.caseBattery = data[5];
                Program.updateTooltip();
            }

            if (id != 11) { 
                _device.ReadReport(OnReport);
                return; 
            }

            Program.headphonesConnected = (data[0] == 0 && data[2] == 0xff)
                || (data[0] == 0xff && data[2] == 0)
                || (data[0] == 0xff && data[2] == 0)
                || (data[0] == 0 && data[2] == 1)
                || (data[0] == 0 && data[2] == 0)
                || (data[0] == 1 && data[2] == 0);

            if (Program.headphonesConnected == previousActive)
            {
                _device.ReadReport(OnReport);
                return;
            }
            previousActive = Program.headphonesConnected;

            Program.updateTooltip();

            if (Program.headphonesConnected)
            {
                this.jblDeviceComboBox.BeginInvoke(new Action(() =>
                {
                    var index = this.jblDeviceComboBox.SelectedIndex;
                    setDefaultDeviceByIndex(index);
                }));
            }
            else
            {
                this.defaultComboBox.BeginInvoke(new Action(() =>
                {
                    var index = this.defaultComboBox.SelectedIndex;
                    setDefaultDeviceByIndex(index);
                }));
            }

            this.deviceStatusLable.BeginInvoke(new Action(() =>
            {
                string activeLable;
                if (Program.headphonesConnected)
                {
                    activeLable = "подключено";
                }
                else
                {
                    activeLable = "не подключено";
                }
                this.deviceStatusLable.Text = activeLable;
            }));

            _device.ReadReport(OnReport);
        }

        private void setStartup()
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            rk.SetValue("JBLQuantumTweaker", Application.ExecutablePath);
        }

        private void setDevices()
        {
            MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator(Guid.NewGuid());
            MMDeviceCollection devCol = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            string jblId = null;
            string defaultId = null;

            try
            {
                jblId = Properties.Settings.Default.jblDeviceId as string;
            }
            catch (System.Configuration.SettingsPropertyNotFoundException ex)
            {

            }

            try
            {
                defaultId = Properties.Settings.Default.defaultDeviceId as string;
            }
            catch (System.Configuration.SettingsPropertyNotFoundException ex)
            {

            }

            var jblIndex = 0;
            var defaultIndex = 0;

            for (int i = 0; i < devCol.Count; i++)
            {
                var device = devCol[i];
                if (i == 0 && jblId == null)
                {
                    jblId = device.ID;
                    jblIndex = 0;
                }
                if (i == 0 && defaultId == null)
                {
                    defaultId = device.ID;
                    defaultIndex = 0;
                }
                if (device.ID == jblId)
                {
                    jblIndex = i;
                }
                if (device.ID == defaultId)
                {
                    defaultIndex = i;
                }
                this.jblDeviceComboBox.Items.Add(device.DeviceFriendlyName);
                this.defaultComboBox.Items.Add(device.DeviceFriendlyName);
            }
            this.defaultComboBox.SelectedIndex = defaultIndex;
            this.jblDeviceComboBox.SelectedIndex = jblIndex;
        }

        private void setDefaultDeviceByIndex(int index)
        {
            MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator(Guid.NewGuid());
            MMDeviceCollection devCol = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            MMDevice device = devCol[index];
            deviceEnumerator.SetDefaultAudioEndpoint(device);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void jblDeviceComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var index = this.jblDeviceComboBox.SelectedIndex;

            MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator(Guid.NewGuid());
            MMDeviceCollection devCol = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            MMDevice device = devCol[index];

            Properties.Settings.Default.jblDeviceId = device.ID;
            Properties.Settings.Default.Save();
        }

        private void defaultComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var index = this.defaultComboBox.SelectedIndex;

            MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator(Guid.NewGuid());
            MMDeviceCollection devCol = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            MMDevice device = devCol[index];

            Properties.Settings.Default.defaultDeviceId = device.ID;
            Properties.Settings.Default.Save();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                this.Hide();
            } else if (this.WindowState == FormWindowState.Normal) {
                this.Show();
            }
        }
    }
}
