using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BodyFat_To_Weight_Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
                // these last two lines will stop the beep sound
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
         
            try
            {
                float currentWeight = float.Parse(textBox1.Text);
                float currentBodyFat = float.Parse(textBox2.Text);
                float desiredBodyFat = float.Parse(textBox3.Text);

                float currentBodyFatDecimal = currentBodyFat / 100;
                float desiredBodyFatDecimal = desiredBodyFat / 100;

                float leanMassWeight = currentWeight * (1 - currentBodyFatDecimal);

                float targetWeight = leanMassWeight / (1 - desiredBodyFatDecimal);

                string printWeight = targetWeight.ToString("0.0");

                label5.Text = printWeight;
            }

            catch
            {
                this.Hide();
                Form2 f2 = new Form2();
                f2.ShowDialog();
            }

           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            label5.Text = "";
        }

       
    }
}
