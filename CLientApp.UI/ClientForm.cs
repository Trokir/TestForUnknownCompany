using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientApp.Data;

namespace CLientApp.UI
{
    public partial class ClientForm : Form
    {
        private readonly UiConnector _client;
        private delegate void SafeCallDelegate(RichTextBox control, string text);
        public ClientForm()
        {
            InitializeComponent();
            _client = new UiConnector();
            _client.RefreshData += _client_RefreshData;
            btnConnect.Click += BtnConnect_Click;

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
        private void _client_RefreshData(object sender, string e)
        {
            WriteTextSafe(txtLog, e);
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
           _client.ConnectToServer(txtText.Text);
        }
    }
}
