using System;
using System.Windows.Forms;
using ServerApp.Data;

namespace ServerApp.UI
{
    public partial class MainForm : Form
    {
        private delegate void SafeCallDelegate(RichTextBox control, string text);
        private readonly UiConnector _server;
        public MainForm()
        {

            InitializeComponent();
            _server = new UiConnector();
            _server.RefreshData += _server_RefreshData;
            Load += Form1_Load;
            btnStartListenig.Click += BtnStartListening_Click;
        }

        private void _server_RefreshData(object sender, string e)
        {
            WriteTextSafe(txtLog, e);
        }
        private void WriteTextSafe(RichTextBox control, string text)
        {
            if (control is null) throw new ArgumentNullException(nameof(control));
            if (control.InvokeRequired)
            {
                var d = new SafeCallDelegate(WriteTextSafe);
                control.Invoke(d, new object[] { control, text });
            }
            else
            {
                AddEntryToLog(text);
            }
        }
        /// <summary>
        /// Adds the entry to log.
        /// </summary>
        /// <param name="output">The output.</param>
        private void AddEntryToLog(string output)
        {
            if (!string.IsNullOrWhiteSpace(txtLog.Text))
            {
                txtLog.AppendText("\r\n" + output);
            }
            else
            {
                txtLog.AppendText(output);
            }
            txtLog.ScrollToCaret();
        }
        private void BtnStartListening_Click(object sender, EventArgs e)

        { 
            toolStripStatusLabel1.Text = $@"Connected to{_server.GetCurrentIpAddress} {_server.GetCurrentPort}";
            _server.StartListener();
            btnStartListenig.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Waiting for a connection...";
        }
    }
}
