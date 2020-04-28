using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DelegateApp
{
    public partial class MainForm : Form
    {
        public event EventHandler<DateTime> SetTime;
        public MainForm()
        {
            InitializeComponent();
            Load += MainForm_Load;
            btnFirstEvent.Click += BtnFirstEvent_Click;
            btnSecondEvent.Click += BtnSecondEvent_Click;
            timer1.Interval = 1000;
            timer1.Tick += Timer1_Tick;
            label2.Text = @"UnSubscribed";
            label3.Text = @"UnSubscribed";
        }

        private void BtnSecondEvent_Click(object sender, EventArgs e)
        {
            SubscribeOrUnSubscribeOnCurrentTime(txtSecond, label3);
        }
        private void BtnFirstEvent_Click(object sender, EventArgs e)
        {
            SubscribeOrUnSubscribeOnCurrentTime(txtFirst, label2);
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "";
            label1.Text = DateTime.Now.ToLongTimeString();
            OnSetTime(DateTime.Now);
        }


        /// <summary>
        /// Subscribes the or un subscribe on current time.
        /// </summary>
        /// <param name="textBox">The text box.</param>
        /// <param name="label">The label.</param>
        private void SubscribeOrUnSubscribeOnCurrentTime(TextBox textBox, Label label)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                SetTime += (ss, ee) =>
                {
                    textBox.Text = ee.ToLongTimeString();
                };
                label.Text = @"Subscribed";
            }

            else
            {
                SetTime = null;
                textBox.Clear();
                label.Text = @"UnSubscribed";
            }
        }




        private void MainForm_Load(object sender, EventArgs e)
        {
            timer1.Start();

        }

        protected virtual void OnSetTime(DateTime e)
        {
            SetTime?.Invoke(this, e);
        }
    }
}
