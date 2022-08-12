using System;
using System.Drawing;
using System.Windows.Forms;

namespace Squeeze_It
{
    public partial class Form1 : Form
    {
        int numReps = 0, timeMiliSeconds, timeSeconds;
        bool isActive;

        public Form1()
        {
            InitializeComponent();   
        }

        private void Start_Btn_Click(object sender, EventArgs e)
        {
            if (isActive == false)
            {
                isActive = true;
                numReps++;
                reps_lbl.Text = numReps.ToString();
                Start_Btn.BackColor = Color.Red;
                Start_Btn.Text = "Stop";
            }
            else
            {
                isActive = false;
                Start_Btn.BackColor = Color.FromArgb(0, 192, 0);
                Start_Btn.Text = "Start";
            }
            
        }

        private void Reset_Btn_Click(object sender, EventArgs e)
        {
            isActive = false;
            RestEverything();
        }

        private void RestEverything()
        {
            timeMiliSeconds = 0;
            timeSeconds = 0;
            isActive = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isActive)
            {
                timeMiliSeconds++;

                if (timeMiliSeconds >= 60)
                {
                    timeSeconds++;
                    timeMiliSeconds = 0;
                }
            }

            UpdateTimer();            
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            numReps++;
            reps_lbl.Text = numReps.ToString();
        }

        private void dwnButton_Click(object sender, EventArgs e)
        {
            numReps--;
            reps_lbl.Text = numReps.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            numReps = 0;
            reps_lbl.Text = numReps.ToString();
        }

        private void UpdateTimer()
        {
            miliSec_lbl.Text = String.Format("{0:00}", timeMiliSeconds);
            sec_lbl.Text = String.Format("{0:00}", timeSeconds);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            RestEverything();
            isActive = false;
        }
    }
}
