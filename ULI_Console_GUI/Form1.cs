using System;
using System.Drawing;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Text;
using System.Linq;
using System.Threading;


namespace ULI_Console_GUI
{


    public partial class Form1 : Form

    {

        TcpClient SpeedClient; //= new TcpClient();
        TcpClient FaultClient; //= new TcpClient();
        byte[] speedByte;
        float SpeedCheck;
        float MaxSpeed = 20000;

        Color greenColor = Color.FromArgb(0, 192, 0);

        enum ButtonConnect { Connect, Disconnect };
        ButtonConnect buttonConnectState = ButtonConnect.Connect;

        Image disconnectImage = new Bitmap(@".\Disconnect.jpg");
        Image connectImage = new Bitmap(@".\hand1.jpg");


        public Form1()
        {
            InitializeComponent();
            //FormBorderStyle = FormBorderStyle.None;
            //WindowState = FormWindowState.Maximized;
        }

        private void ConnDisconnBtn_Click(object sender, EventArgs e)
        {
            ConnectionStatusTimer.Enabled = true;
            Image disconnectImage = new Bitmap(@".\Disconnect.jpg");
            Image connectImage = new Bitmap(@".\hand1.jpg");


            if (buttonConnectState == ButtonConnect.Connect)
            {
                buttonConnectState = ButtonConnect.Disconnect;
                ConnDisconnBtn.Image = disconnectImage;
                try
                {
                    TcpClient SpeedClient = new TcpClient();
                    SpeedClient.BeginConnect("192.168.1.90", 40021, onCompleteConnect, SpeedClient);
                    Thread.Sleep(500);
                    TcpClient FaultClient = new TcpClient();
                    FaultClient.BeginConnect("192.168.1.90", 40021, onCompleteConnect1, FaultClient);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }

            }
            else
            {
                buttonConnectState = ButtonConnect.Connect;
                ConnDisconnBtn.Image = connectImage;
                SendRateTimer.Enabled = false;
                FaultDataTimer.Enabled = false;
                SpeedClient.GetStream().Close();
                SpeedClient.Close();
                FaultClient.GetStream().Close();
                FaultClient.Close();
                CMDspeedLabel.Text = "0.00";
                FDBspeedLabel.Text = "0.00";

            }

            //FaultDataTimer.Enabled = true;
        }


        void onCompleteConnect(IAsyncResult iar)
        {
            Image connectImage = new Bitmap(@".\hand1.jpg");
            byte[] cnnTx = Encoding.ASCII.GetBytes("SpeedConnect");

            try
            {

                SpeedClient = (TcpClient)iar.AsyncState;
                SpeedClient.EndConnect(iar);

                SpeedClient.GetStream().Write(cnnTx, 0, cnnTx.Length);

            }
            catch (Exception exc)
            {
                ConnectionStatusTimer.Enabled = false;
                ConnDisconnBtn.Image = connectImage;
                buttonConnectState = ButtonConnect.Connect;
                MessageBox.Show(exc.Message);
            }
        }

        void onCompleteConnect1(IAsyncResult iar)
        {
            //TcpClient tcpc;
            Image connectImage = new Bitmap(@".\hand1.jpg");
            byte[] cnnTx = Encoding.ASCII.GetBytes("FaultConnect");

            try
            {

                FaultClient = (TcpClient)iar.AsyncState;
                FaultClient.EndConnect(iar);



                FaultClient.GetStream().Write(cnnTx, 0, cnnTx.Length);

            }
            catch (Exception exc)
            {
                ConnectionStatusTimer.Enabled = false;
                ConnDisconnBtn.Image = connectImage;
                buttonConnectState = ButtonConnect.Connect;
                MessageBox.Show(exc.Message);
            }
        }

        void onCompleteReadFromServerStream(IAsyncResult iar)
        {
            int nCountBytesReceivedFromServer;

            try
            {

                SpeedClient = (TcpClient)iar.AsyncState;
                nCountBytesReceivedFromServer = SpeedClient.GetStream().EndRead(iar);


                if (nCountBytesReceivedFromServer == 0)
                {
                    MessageBox.Show("Connection broken");
                    return;
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }

        void onCompleteReadFromServerStream1(IAsyncResult iar)
        {
            int nCountBytesReceivedFromServer;

            try
            {

                FaultClient = (TcpClient)iar.AsyncState;
                nCountBytesReceivedFromServer = FaultClient.GetStream().EndRead(iar);


                if (nCountBytesReceivedFromServer == 0)
                {
                    MessageBox.Show("Connection broken");
                    return;
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {

            speedTextBox.Font = new Font(speedTextBox.Font.FontFamily, 8, FontStyle.Italic);
            speedTextBox.ForeColor = Color.Gray;
            speedTextBox.Text = "Enter Speed Value";
            this.speedTextBox.Leave += new System.EventHandler(this.speedTextBox_Leave);
            this.speedTextBox.Enter += new System.EventHandler(this.speedTextBox_Enter);

            uliPanel.BackColor = greenColor;
            dynoPanel.BackColor = greenColor;

            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.BackColor = SystemColors.Control;

        }




        public void setSpeedBtn_Click(object sender, EventArgs e)
        {
            speedTextBox.Font = new Font(speedTextBox.Font.FontFamily, 8, FontStyle.Italic);
            speedTextBox.ForeColor = Color.Gray;
            double valueCheck;

            if (SpeedClient == null)
            {
                speedTextBox.Text = "Enter Speed Value";
                MessageBox.Show("Please Connect to System First");
                return;
            }

            if (SpeedClient.Client.Connected == false)
            {
                speedTextBox.Text = "Enter Speed Value";
                MessageBox.Show("Please Connect to System First");
                return;
            }

            if (!Double.TryParse(speedTextBox.Text, out valueCheck))
            {
                speedTextBox.Text = "Enter Speed Value";
                MessageBox.Show("Please Enter a Speed Value");
                return;
            }

            SpeedCheck = float.Parse(speedTextBox.Text);

            if (SpeedCheck >= MaxSpeed)
            {
                MessageBox.Show("The Speed Value exceeds the Max Value of 20,000.  Please Enter again");
                speedTextBox.Clear();
                speedTextBox.Text = " ";
                return;
            }

            if (SpeedCheck < 0)
            {
                MessageBox.Show("The Speed Value must be greater than Zero");
                speedTextBox.Clear();
                speedTextBox.Text = " ";
                return;
            }


            try
            {
                speedByte = Encoding.ASCII.GetBytes(speedTextBox.Text);

                if (SpeedClient.Connected && SpeedClient.GetStream().CanWrite)
                {
                    SpeedClient.GetStream().Write(speedByte, 0, speedByte.Length);
                    speedTextBox.Clear();
                    speedTextBox.Text = "";
                }
                else
                {
                    MessageBox.Show("Can't Send Speed Value");
                }


            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

            SendRateTimer.Enabled = true;
            ReadTimer.Enabled = true;

            speedTextBox.Font = new Font(speedTextBox.Font.FontFamily, 8, FontStyle.Italic);
            speedTextBox.ForeColor = Color.Gray;
            speedTextBox.Text = "Enter Speed Value";
        }

        public void SendRateTimer_Tick(object sender, EventArgs e)
        {
            if (SpeedClient.Connected == true && SpeedClient.GetStream().CanWrite)
            {
                Console.WriteLine(Encoding.ASCII.GetString(speedByte));
                SpeedClient.GetStream().BeginWrite(speedByte, 0, speedByte.Length, onCompleteWriteToNEAT, SpeedClient);
            }
        }

        void onCompleteWriteToNEAT(IAsyncResult iar)
        {


            try
            {
                SpeedClient = (TcpClient)iar.AsyncState;
                SpeedClient.GetStream().EndWrite(iar);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        private void ReadTimer_Tick(object sender, EventArgs e)
        {

            byte[] incomingUDPBytes = new byte[952];
            byte[] commandSpeedBytes = new byte[4];
            byte[] feedbackSpeedBytes = new byte[4];
            //string commandSpeedCheck;
            //string feedbackStringCheck;
            //string[] values = { "ULI_Fault", "DynoFault", "ULI_AOkay", "DynoAOkay" };

            if (SpeedClient != null && SpeedClient.Connected == true && buttonConnectState == ButtonConnect.Disconnect && SpeedClient.GetStream().CanRead == true && SpeedClient.GetStream().DataAvailable == true)
            {
                SpeedClient.GetStream().BeginRead(incomingUDPBytes, 0, incomingUDPBytes.Length, onCompleteReadFromServerStream, SpeedClient);
            }
            else
            {
                return;
            }


            for (int i = 0; i < 4; i++)
            {
                commandSpeedBytes[i] = incomingUDPBytes[i];
            }
            //commandSpeedCheck = Encoding.ASCII.GetString(commandSpeedBytes);


            for (int i = 0; i < 4; i++)
            {
                feedbackSpeedBytes[i] = incomingUDPBytes[64 + i];
            }
            //feedbackStringCheck = Encoding.ASCII.GetString(feedbackSpeedBytes);


            //if (values.Any(commandSpeedCheck.Contains) || values.Any(feedbackStringCheck.Contains))
            //{
            //    return;
            //}


            float cmdSpeedFloat = System.BitConverter.ToSingle(commandSpeedBytes, 0);
            float uliCmdSpeed = cmdSpeedFloat * (float)3.025;

            CMDspeedLabel.Invoke(new MethodInvoker(delegate { CMDspeedLabel.Text = uliCmdSpeed.ToString(); }));

            float fdbSpeedFloat = System.BitConverter.ToSingle(feedbackSpeedBytes, 0);
            float uliFdbSpeed = fdbSpeedFloat * (float)3.025;
            FDBspeedLabel.Invoke(new MethodInvoker(delegate { FDBspeedLabel.Text = uliFdbSpeed.ToString(); }));
        }


        private void speedTextBox_Leave(object sender, EventArgs e)
        {
            if (speedTextBox.Text.Length == 0)
            {
                speedTextBox.Font = new Font(speedTextBox.Font.FontFamily, 8, FontStyle.Italic);
                speedTextBox.ForeColor = Color.Gray;
                speedTextBox.Text = "Enter Speed Value";
            }
        }


        private void speedTextBox_Enter(object sender, EventArgs e)
        {
            speedTextBox.Clear();
            speedTextBox.Text = "";
            speedTextBox.Font = new Font(speedTextBox.Font.FontFamily, 14, FontStyle.Regular);
            speedTextBox.ForeColor = Color.Black;

        }



        private void E_StopBtn_Click(object sender, EventArgs e)
        {
            E_StopBtn.Image = null;
            E_StopBtn.Font = new Font(speedTextBox.Font.FontFamily, 15, FontStyle.Bold);
            E_StopBtn.ForeColor = Color.White;
            E_StopBtn.Text = "E-Stop Pressed!";

            //FaultDataTimer.Enabled = false;
            //SendRateTimer.Enabled = false;

            byte[] tx;


            try
            {
                tx = Encoding.ASCII.GetBytes("Emergency");

                if (SpeedClient.Connected && SpeedClient.GetStream().CanWrite)
                {

                    SpeedClient.GetStream().Write(tx, 0, tx.Length);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void E_StopTimer_Tick(object sender, EventArgs e)
        {
            E_StopBtn.Text = "";
            Image myimage = new Bitmap(@".\images.png");
            E_StopBtn.Image = myimage;
        }



        public void ConnectionStatusTimer_Tick(object sender, EventArgs e)
        {
            if (SpeedClient != null && SpeedClient.Connected == true /*&& buttonConnectState == ButtonConnect.Disconnect*/ /*&& SpeedClient.GetStream().CanRead == true && SpeedClient.GetStream().DataAvailable == true*/)
            {
                buttonConnectState = ButtonConnect.Disconnect;
                ConnDisconnBtn.Image = disconnectImage;
                panel3.BorderStyle = BorderStyle.Fixed3D;
                panel3.BackColor = greenColor;
                label3.Font = new Font(label3.Font.FontFamily, 9, FontStyle.Bold);
                label3.Text = "Connected!";
                FaultDataTimer.Enabled = true;

            }
            else
            {
                buttonConnectState = ButtonConnect.Connect;
                ConnDisconnBtn.Image = connectImage;
                panel3.BorderStyle = BorderStyle.Fixed3D;
                panel3.BackColor = Color.Red;
                label3.Font = new Font(label3.Font.FontFamily, 9, FontStyle.Bold);
                label3.Text = "Disconnected";

                SendRateTimer.Enabled = false;
                FaultDataTimer.Enabled = false;
            }
        }



        public void FaultDataTimer_Tick(object sender, EventArgs e)
        {
            byte[] incomingFaultBytes = new byte[9];
            string strRecv;
            string[] values = { "ULI_Fault", "DynoFault", "ULI_AOkay", "DynoAOkay" };

            if (FaultClient.Connected == true && FaultClient.GetStream().CanRead == true && FaultClient.GetStream().DataAvailable == true)
            {
                try
                {
                    FaultClient.GetStream().BeginRead(incomingFaultBytes, 0, incomingFaultBytes.Length, onCompleteReadFromServerStream1, FaultClient);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }

                strRecv = Encoding.ASCII.GetString(incomingFaultBytes);

                if (strRecv == "ULI_Fault")
                {
                    uliPanel.BackColor = Color.Red;
                    speedByte = Encoding.ASCII.GetBytes("0");
                }
                else if (strRecv == "DynoFault")
                {
                    dynoPanel.BackColor = Color.Red;
                    speedByte = Encoding.ASCII.GetBytes("0");
                }

                else if (strRecv == "ULI_AOkay")
                {
                    uliPanel.BackColor = greenColor;
                }
                else if (strRecv == "DynoAOkay")
                {
                    dynoPanel.BackColor = greenColor;
                }
            }
        }



        private void speedTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("You must click \"Set Speed\" button");
                speedTextBox.Clear();
                speedTextBox.Text = " ";
                e.SuppressKeyPress = true;
            }
        }

     
    }
}
