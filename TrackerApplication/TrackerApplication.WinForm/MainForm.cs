using System;
using System.Drawing;
using System.Text.Json;
using System.Windows.Forms;
using TrackerApplication.Client;
using TrackerApplication.Contracts.Models;

namespace TrackerApplication.WinForm
{
    public partial class MainForm : Form
    {
        private readonly TrackerClient _trackerClient;
        private readonly TextBoxTrackerClientLogger _textBoxTrackerClientLogger;

        public MainForm()
        {
            InitializeComponent();

            var appConfig = AppConfig.Load();

            _textBoxTrackerClientLogger = new TextBoxTrackerClientLogger(textBoxLogs);
            LogConfiguration(appConfig);

            _trackerClient = new TrackerClient(_textBoxTrackerClientLogger, appConfig.TrackerClientConfig);
            _trackerClient.TrackerDataReceived += TrackerDataReceived;
        }

        private void LogConfiguration(AppConfig appConfig)
        {
            var jsonSerializerOptions = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(appConfig, jsonSerializerOptions);

            _textBoxTrackerClientLogger.Info($"Configuration:{Environment.NewLine}{json}");
        }

        private void TrackerDataReceived(object sender, TrackerDataReceivedEvenArgs e)
        {
            SafeSetDataGridView(e.Data);
        }

        private void SafeSetDataGridView(TrackerData[] data)
        {
            if (dataGridView.InvokeRequired)
            {
                var action = new Action(() => { dataGridView.DataSource = data; });
                dataGridView.Invoke(action);
            }
            else
            {
                dataGridView.DataSource = data;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _trackerClient.Start();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _trackerClient.Stop();
        }

        private void IncreaseFont()
        {
            if (textBoxLogs.Font.Size >= 18F)
            {
                return;
            }

            textBoxLogs.Font = new Font(textBoxLogs.Font.Name, textBoxLogs.Font.Size + 1, FontStyle.Regular, GraphicsUnit.Point);
        }

        private void DecreaseFont()
        {
            if (textBoxLogs.Font.Size <= 8F)
            {
                return;
            }

            textBoxLogs.Font = new Font(textBoxLogs.Font.Name, textBoxLogs.Font.Size - 1, FontStyle.Regular, GraphicsUnit.Point);
        }

        private void textBoxLogs_KeyDown(object sender, KeyEventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Control) && e.KeyCode == Keys.Oemplus)
            {
                IncreaseFont();
            }

            if (ModifierKeys.HasFlag(Keys.Control) && e.KeyCode == Keys.OemMinus)
            {
                DecreaseFont();
            }
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            if (buttonPause.Tag.ToString() == "1")
            {
                _trackerClient.Stop();
                buttonPause.Tag = 0;
                buttonPause.Text = "Continue";
            }
            else
            {
                _trackerClient.Start();
                buttonPause.Tag = 1;
                buttonPause.Text = "Pause";
            }
        }
    }
}
