using System;
using System.Windows.Forms;
using TrackerApplication.Client;

namespace TrackerApplication.WinForm
{
    public class TextBoxTrackerClientLogger : ITrackerClientLogger
    {
        private readonly TextBox _textBox;

        public TextBoxTrackerClientLogger(TextBox textBox)
        {
            _textBox = textBox;
        }

        public void Error(string message)
        {
            WriteLog($"ERROR: {message}");
        }

        public void Info(string message)
        {
            WriteLog($"INFO: {message}");
        }

        public void Warning(string message)
        {
            WriteLog($"WARN: {message}");
        }

        private void WriteLog(string message)
        {
            if (_textBox.InvokeRequired)
            {
                _textBox.Invoke(new Action<string>(AppendTextBox), new object[] { message });
            }
            else
            {
                AppendTextBox(message);
            }           
        }

        private void AppendTextBox(string message)
        {
            _textBox.AppendText($"{Environment.NewLine}{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {message}");
        }
    }
}
