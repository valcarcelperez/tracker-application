using System;
using System.Drawing;
using System.Windows.Forms;
using TrackerApplication.Client;

namespace TrackerApplication.WinForm
{
    public partial class MainForm : Form
    {
        private TrackerClient _trackerClient;

        public MainForm()
        {
            InitializeComponent();

            var trackerClientConfig = new TrackerClientConfig
            {
                BaseAddress = "http://localhost:5000",
                RequestInterval = TimeSpan.FromMilliseconds(5000),
                Timeout = TimeSpan.FromSeconds(10)
            };

            var logger = new TextBoxTrackerClientLogger(textBoxLogs);
            _trackerClient = new TrackerClient(logger, trackerClientConfig);
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
    }
}
