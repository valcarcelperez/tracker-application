using System;
using System.Threading;
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
                RequestInterval = TimeSpan.FromMilliseconds(1000),
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
    }
}
