
namespace ULI_Console_GUI
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.setSpeedBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CMDspeedLabel = new System.Windows.Forms.Label();
            this.FDBspeedLabel = new System.Windows.Forms.Label();
            this.E_StopBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.uliPanel = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.dynoPanel = new System.Windows.Forms.Panel();
            this.dynoStatusLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.speedTextBox = new System.Windows.Forms.TextBox();
            this.E_StopTimer = new System.Windows.Forms.Timer(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ConnectionStatusTimer = new System.Windows.Forms.Timer(this.components);
            this.ConnDisconnBtn = new System.Windows.Forms.Button();
            this.SendRateTimer = new System.Windows.Forms.Timer(this.components);
            this.FaultDataTimer = new System.Windows.Forms.Timer(this.components);
            this.ReadTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.uliPanel.SuspendLayout();
            this.dynoPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // setSpeedBtn
            // 
            this.setSpeedBtn.BackColor = System.Drawing.SystemColors.Control;
            this.setSpeedBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.setSpeedBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setSpeedBtn.Location = new System.Drawing.Point(138, 81);
            this.setSpeedBtn.Name = "setSpeedBtn";
            this.setSpeedBtn.Size = new System.Drawing.Size(110, 41);
            this.setSpeedBtn.TabIndex = 1;
            this.setSpeedBtn.Text = "Set Speed";
            this.setSpeedBtn.UseVisualStyleBackColor = false;
            this.setSpeedBtn.Click += new System.EventHandler(this.setSpeedBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Commanded Speed (RPM)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(207, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Feedback Speed (RPM)";
            // 
            // CMDspeedLabel
            // 
            this.CMDspeedLabel.AutoSize = true;
            this.CMDspeedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CMDspeedLabel.Location = new System.Drawing.Point(346, 21);
            this.CMDspeedLabel.Name = "CMDspeedLabel";
            this.CMDspeedLabel.Size = new System.Drawing.Size(40, 20);
            this.CMDspeedLabel.TabIndex = 5;
            this.CMDspeedLabel.Text = "0.00";
            // 
            // FDBspeedLabel
            // 
            this.FDBspeedLabel.AutoSize = true;
            this.FDBspeedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FDBspeedLabel.Location = new System.Drawing.Point(346, 21);
            this.FDBspeedLabel.Name = "FDBspeedLabel";
            this.FDBspeedLabel.Size = new System.Drawing.Size(40, 20);
            this.FDBspeedLabel.TabIndex = 6;
            this.FDBspeedLabel.Text = "0.00";
            // 
            // E_StopBtn
            // 
            this.E_StopBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.E_StopBtn.BackColor = System.Drawing.Color.Red;
            this.E_StopBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.E_StopBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.E_StopBtn.Image = ((System.Drawing.Image)(resources.GetObject("E_StopBtn.Image")));
            this.E_StopBtn.Location = new System.Drawing.Point(713, 342);
            this.E_StopBtn.Name = "E_StopBtn";
            this.E_StopBtn.Size = new System.Drawing.Size(151, 136);
            this.E_StopBtn.TabIndex = 7;
            this.E_StopBtn.UseVisualStyleBackColor = false;
            this.E_StopBtn.Click += new System.EventHandler(this.E_StopBtn_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.Yellow;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.FDBspeedLabel);
            this.panel1.Location = new System.Drawing.Point(15, 418);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(598, 60);
            this.panel1.TabIndex = 12;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.Color.Yellow;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.CMDspeedLabel);
            this.panel2.Location = new System.Drawing.Point(15, 328);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(598, 60);
            this.panel2.TabIndex = 13;
            // 
            // uliPanel
            // 
            this.uliPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uliPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.uliPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.uliPanel.Controls.Add(this.label5);
            this.uliPanel.Location = new System.Drawing.Point(698, 161);
            this.uliPanel.Name = "uliPanel";
            this.uliPanel.Size = new System.Drawing.Size(164, 54);
            this.uliPanel.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Location = new System.Drawing.Point(30, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "ULI Status";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dynoPanel
            // 
            this.dynoPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dynoPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.dynoPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dynoPanel.Controls.Add(this.dynoStatusLabel);
            this.dynoPanel.Location = new System.Drawing.Point(700, 231);
            this.dynoPanel.Name = "dynoPanel";
            this.dynoPanel.Size = new System.Drawing.Size(164, 54);
            this.dynoPanel.TabIndex = 15;
            // 
            // dynoStatusLabel
            // 
            this.dynoStatusLabel.AutoSize = true;
            this.dynoStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dynoStatusLabel.Location = new System.Drawing.Point(16, 16);
            this.dynoStatusLabel.Name = "dynoStatusLabel";
            this.dynoStatusLabel.Size = new System.Drawing.Size(112, 20);
            this.dynoStatusLabel.TabIndex = 7;
            this.dynoStatusLabel.Text = "Dyno Status";
            this.dynoStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.White;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(20, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(161, 24);
            this.label7.TabIndex = 6;
            this.label7.Text = "Set Dyno Speed";
            // 
            // speedTextBox
            // 
            this.speedTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speedTextBox.Location = new System.Drawing.Point(24, 81);
            this.speedTextBox.Multiline = true;
            this.speedTextBox.Name = "speedTextBox";
            this.speedTextBox.Size = new System.Drawing.Size(108, 39);
            this.speedTextBox.TabIndex = 0;
            this.speedTextBox.TabStop = false;
            this.speedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.speedTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.speedTextBox_KeyDown);
            // 
            // E_StopTimer
            // 
            this.E_StopTimer.Enabled = true;
            this.E_StopTimer.Interval = 1000;
            this.E_StopTimer.Tick += new System.EventHandler(this.E_StopTimer_Tick);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label3);
            this.panel3.Enabled = false;
            this.panel3.Location = new System.Drawing.Point(728, 32);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(136, 43);
            this.panel3.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "Not Connected";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(730, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "System Status";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(24, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(767, 451);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(363, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 20);
            this.label6.TabIndex = 21;
            // 
            // ConnectionStatusTimer
            // 
            this.ConnectionStatusTimer.Interval = 800;
            this.ConnectionStatusTimer.Tick += new System.EventHandler(this.ConnectionStatusTimer_Tick);
            // 
            // ConnDisconnBtn
            // 
            this.ConnDisconnBtn.Image = ((System.Drawing.Image)(resources.GetObject("ConnDisconnBtn.Image")));
            this.ConnDisconnBtn.Location = new System.Drawing.Point(602, 9);
            this.ConnDisconnBtn.Name = "ConnDisconnBtn";
            this.ConnDisconnBtn.Size = new System.Drawing.Size(120, 66);
            this.ConnDisconnBtn.TabIndex = 24;
            this.ConnDisconnBtn.UseVisualStyleBackColor = true;
            this.ConnDisconnBtn.Click += new System.EventHandler(this.ConnDisconnBtn_Click);
            // 
            // SendRateTimer
            // 
            this.SendRateTimer.Interval = 1000;
            this.SendRateTimer.Tick += new System.EventHandler(this.SendRateTimer_Tick);
            // 
            // FaultDataTimer
            // 
            this.FaultDataTimer.Interval = 500;
            this.FaultDataTimer.Tick += new System.EventHandler(this.FaultDataTimer_Tick);
            // 
            // ReadTimer
            // 
            this.ReadTimer.Interval = 500;
            this.ReadTimer.Tick += new System.EventHandler(this.ReadTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(894, 511);
            this.Controls.Add(this.ConnDisconnBtn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.speedTextBox);
            this.Controls.Add(this.dynoPanel);
            this.Controls.Add(this.uliPanel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.E_StopBtn);
            this.Controls.Add(this.setSpeedBtn);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ULI Console GUI";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.uliPanel.ResumeLayout(false);
            this.uliPanel.PerformLayout();
            this.dynoPanel.ResumeLayout(false);
            this.dynoPanel.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button setSpeedBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label CMDspeedLabel;
        private System.Windows.Forms.Label FDBspeedLabel;
        private System.Windows.Forms.Button E_StopBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel uliPanel;
        private System.Windows.Forms.Panel dynoPanel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label dynoStatusLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox speedTextBox;
        private System.Windows.Forms.Timer E_StopTimer;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer ConnectionStatusTimer;
        private System.Windows.Forms.Button ConnDisconnBtn;
        private System.Windows.Forms.Timer SendRateTimer;
        private System.Windows.Forms.Timer FaultDataTimer;
        private System.Windows.Forms.Timer ReadTimer;
        private System.Windows.Forms.Label label3;
    }
}

