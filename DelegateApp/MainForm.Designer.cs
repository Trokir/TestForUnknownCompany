namespace DelegateApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnFirstEvent = new System.Windows.Forms.Button();
            this.btnSecondEvent = new System.Windows.Forms.Button();
            this.txtFirst = new System.Windows.Forms.TextBox();
            this.txtSecond = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnFirstEvent
            // 
            this.btnFirstEvent.Location = new System.Drawing.Point(89, 225);
            this.btnFirstEvent.Name = "btnFirstEvent";
            this.btnFirstEvent.Size = new System.Drawing.Size(168, 23);
            this.btnFirstEvent.TabIndex = 0;
            this.btnFirstEvent.Text = "Start/Stop First ";
            this.btnFirstEvent.UseVisualStyleBackColor = true;
            // 
            // btnSecondEvent
            // 
            this.btnSecondEvent.Location = new System.Drawing.Point(356, 225);
            this.btnSecondEvent.Name = "btnSecondEvent";
            this.btnSecondEvent.Size = new System.Drawing.Size(157, 23);
            this.btnSecondEvent.TabIndex = 1;
            this.btnSecondEvent.Text = "Start/Stop Second";
            this.btnSecondEvent.UseVisualStyleBackColor = true;
            // 
            // txtFirst
            // 
            this.txtFirst.Location = new System.Drawing.Point(73, 118);
            this.txtFirst.Name = "txtFirst";
            this.txtFirst.Size = new System.Drawing.Size(184, 20);
            this.txtFirst.TabIndex = 2;
            // 
            // txtSecond
            // 
            this.txtSecond.Location = new System.Drawing.Point(356, 118);
            this.txtSecond.Name = "txtSecond";
            this.txtSecond.Size = new System.Drawing.Size(217, 20);
            this.txtSecond.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(101, 322);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(362, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "label3";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSecond);
            this.Controls.Add(this.txtFirst);
            this.Controls.Add(this.btnSecondEvent);
            this.Controls.Add(this.btnFirstEvent);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFirstEvent;
        private System.Windows.Forms.Button btnSecondEvent;
        private System.Windows.Forms.TextBox txtFirst;
        private System.Windows.Forms.TextBox txtSecond;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

