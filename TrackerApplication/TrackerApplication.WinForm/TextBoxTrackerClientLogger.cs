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
            var text = $"{Environment.NewLine}{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {message}";
            SafeAppendTextBox(text);
        }

        private void SafeAppendTextBox(string text)
        {
            if (_textBox.InvokeRequired)
            {
                var action = new Action(() => { _textBox.AppendText(text); });
                _textBox.Invoke(action);
            }
            else
            {
                _textBox.AppendText(text);
            }
        }
    }
}
