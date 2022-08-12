namespace Squeeze_It
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
            this.Start_Btn = new System.Windows.Forms.Button();
            this.sec_lbl = new System.Windows.Forms.Label();
            this.Reset_Btn = new System.Windows.Forms.Button();
            this.reps_lbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.miliSec_lbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.dwnButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Start_Btn
            // 
            this.Start_Btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Start_Btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Start_Btn.Location = new System.Drawing.Point(185, 268);
            this.Start_Btn.Name = "Start_Btn";
            this.Start_Btn.Size = new System.Drawing.Size(242, 103);
            this.Start_Btn.TabIndex = 0;
            this.Start_Btn.Text = "Start";
            this.Start_Btn.UseVisualStyleBackColor = false;
            this.Start_Btn.Click += new System.EventHandler(this.Start_Btn_Click);
            // 
            // sec_lbl
            // 
            this.sec_lbl.AutoSize = true;
            this.sec_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sec_lbl.Location = new System.Drawing.Point(74, 98);
            this.sec_lbl.Name = "sec_lbl";
            this.sec_lbl.Size = new System.Drawing.Size(127, 91);
            this.sec_lbl.TabIndex = 1;
            this.sec_lbl.Text = "00";
            // 
            // Reset_Btn
            // 
            this.Reset_Btn.BackColor = System.Drawing.Color.White;
            this.Reset_Btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Reset_Btn.Location = new System.Drawing.Point(185, 409);
            this.Reset_Btn.Name = "Reset_Btn";
            this.Reset_Btn.Size = new System.Drawing.Size(106, 81);
            this.Reset_Btn.TabIndex = 5;
            this.Reset_Btn.Text = "Reset Time";
            this.Reset_Btn.UseVisualStyleBackColor = false;
            this.Reset_Btn.Click += new System.EventHandler(this.Reset_Btn_Click);
            // 
            // reps_lbl
            // 
            this.reps_lbl.AutoSize = true;
            this.reps_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reps_lbl.Location = new System.Drawing.Point(455, 98);
            this.reps_lbl.Name = "reps_lbl";
            this.reps_lbl.Size = new System.Drawing.Size(83, 91);
            this.reps_lbl.TabIndex = 6;
            this.reps_lbl.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(179, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 32);
            this.label3.TabIndex = 7;
            this.label3.Text = "Time";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(476, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 32);
            this.label4.TabIndex = 8;
            this.label4.Text = "Reps";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // miliSec_lbl
            // 
            this.miliSec_lbl.AutoSize = true;
            this.miliSec_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.miliSec_lbl.Location = new System.Drawing.Point(241, 98);
            this.miliSec_lbl.Name = "miliSec_lbl";
            this.miliSec_lbl.Size = new System.Drawing.Size(127, 91);
            this.miliSec_lbl.TabIndex = 9;
            this.miliSec_lbl.Text = "00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(185, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 91);
            this.label2.TabIndex = 10;
            this.label2.Text = ":";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(321, 409);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 81);
            this.button1.TabIndex = 11;
            this.button1.Text = "Clear Count";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // upButton
            // 
            this.upButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.upButton.Location = new System.Drawing.Point(578, 98);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(42, 39);
            this.upButton.TabIndex = 12;
            this.upButton.Text = "+";
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // dwnButton
            // 
            this.dwnButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dwnButton.Location = new System.Drawing.Point(578, 143);
            this.dwnButton.Name = "dwnButton";
            this.dwnButton.Size = new System.Drawing.Size(42, 39);
            this.dwnButton.TabIndex = 13;
            this.dwnButton.Text = "-";
            this.dwnButton.UseVisualStyleBackColor = true;
            this.dwnButton.Click += new System.EventHandler(this.dwnButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 529);
            this.Controls.Add(this.dwnButton);
            this.Controls.Add(this.upButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.miliSec_lbl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.reps_lbl);
            this.Controls.Add(this.Reset_Btn);
            this.Controls.Add(this.sec_lbl);
            this.Controls.Add(this.Start_Btn);
            this.Name = "Form1";
            this.Text = "Squeeze It!";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Start_Btn;
        private System.Windows.Forms.Label sec_lbl;
        private System.Windows.Forms.Button Reset_Btn;
        private System.Windows.Forms.Label reps_lbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label miliSec_lbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button dwnButton;
    }
}

