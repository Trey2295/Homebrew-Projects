using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace UDPLibrary
{
    public class TCPIPServer : IDisposable
    {
        /// <summary>
        /// https://github.com/KarimGabr/TCPIP-client-server
        /// heavily modified
        /// </summary>

        public class DataReceivedEventArgs : EventArgs
        {
            public byte[] Stream { get; private set; }
            public ClientNode Client{get ; private set ; }


            public DataReceivedEventArgs(byte[] stream, ClientNode client)
            {
                Stream = stream;
                Client = client;
            }
        }

        public event EventHandler<DataReceivedEventArgs> OnDataReceived;

        readonly int transmitByteSize = 0;
        TcpListener mTCPListener;
        private readonly List<ClientNode> mlClientSocks=new List<ClientNode>();

        //****************************************************************************
        public TCPIPServer(int transmitByteLength)
        //****************************************************************************
        {
            transmitByteSize = transmitByteLength;
        }
        //****************************************************************************
        public void Start(int port)
            //****************************************************************************
        {
            mTCPListener = new TcpListener(IPAddress.Parse("192.168.1.90"), port);

           // log.WriteToLogNow("public void Start(int port)");
            try
            {
                mTCPListener.Start();
                mTCPListener.BeginAcceptTcpClient(OnCompleteAcceptTcpClient, mTCPListener);
            }
            catch (Exception ex)
            {
             //   log.WriteToLog(ex.ToString());
            }
        }

        //****************************************************************************
        void OnCompleteAcceptTcpClient(IAsyncResult iar)
        //****************************************************************************
        {
            TcpListener tcpl = (TcpListener)iar.AsyncState;
            TcpClient tclient = null;
            ClientNode cNode = null;

            try
            {
                //log.WriteToLogNow("Entry to:  \"OnCompleteAcceptTcpClient(IAsyncResult iar)\" "); //sss
                tclient = tcpl.EndAcceptTcpClient(iar);

                //printLine("Client Connected...");

                tcpl.BeginAcceptTcpClient(OnCompleteAcceptTcpClient, tcpl);

                lock (mlClientSocks)
                {
                    mlClientSocks.Add((
                        cNode = new ClientNode(tclient, 
                        new byte[transmitByteSize], 
                        new byte[12], // receive buffer (was 8 bytes) 
                        tclient.Client.RemoteEndPoint.ToString())));
                    //lbClients.Items.Add(cNode.ToString());
                }

                //log.WriteToLogNow("Before the BeginRead call, cNode.Rx.Length = " + cNode.Rx.Length.ToString());
                tclient.GetStream().BeginRead(cNode.Rx, 0, cNode.Rx.Length, OnCompleteReadFromTCPClientStream, tclient);
               // log.WriteToLogNow("After the BeginRead call.");
            }
            catch (Exception ex)
            {
                //log.WriteToLogNow("Exception TCPIPServer.cs line 97");
                //log.WriteToLog(ex.ToString());
            }
        }//end func


        //****************************************************************************
        void OnCompleteReadFromTCPClientStream(IAsyncResult iar)
        //****************************************************************************  
        {
            TcpClient tcpc;
            int nCountReadBytes = 0;
           // string strRecv;
            ClientNode cn = null;

            try
            {
                lock (mlClientSocks)
                {
                    tcpc = (TcpClient)iar.AsyncState;

                    cn = mlClientSocks.Find(x => x.strId == tcpc.Client.RemoteEndPoint.ToString());

                    nCountReadBytes = tcpc.GetStream().EndRead(iar);

                    if (nCountReadBytes == 0)// this happens when the client is disconnected
                    {
                       // MessageBox.Show("Client disconnected.");
                        mlClientSocks.Remove(cn);
                        //lbClients.Items.Remove(cn.ToString());
                        return;
                    }

                    //strRecv = Encoding.ASCII.GetString(cn.Rx, 0, nCountReadBytes).Trim();

                    //call send here
                    OnMessageReceived(new DataReceivedEventArgs(cn.Rx ,cn));
                    //SendData(FileToByte(path), cn);
 
                    cn.Rx = new byte[12]; // SSS Was 8

                    tcpc.GetStream().BeginRead(cn.Rx, 0, cn.Rx.Length, OnCompleteReadFromTCPClientStream, tcpc);
                }
            }
            catch (Exception ex)
            {
                //log.WriteToLog(ex.ToString());

                lock (mlClientSocks)
                {
                    //printLine("Client disconnected: " + cn.ToString());
                    mlClientSocks.Remove(cn);
                    //lbClients.Items.Remove(cn.ToString());
                }

            }
        }

        //************************************
        protected virtual void OnMessageReceived(DataReceivedEventArgs e)
        //************************************
        {
            OnDataReceived?.Invoke(this, e);
        }//end func

        //****************************************************************************
        private void OnCompleteWriteToClientStream(IAsyncResult iar)
        //****************************************************************************
        {
            try
            {
                TcpClient tcpc = (TcpClient)iar.AsyncState;
                tcpc.GetStream().EndWrite(iar);
            }
            catch (Exception ex)
            {
                //log.WriteToLogNow(ex.ToString());
            }
        }

        //****************************************************************************
        public void SendData(byte[] RequestedBytes, ClientNode RequestedClient)
        //****************************************************************************
        {

            //if (string.IsNullOrEmpty(tbPayload.Text)) return;

            ClientNode cn = RequestedClient;
            if (RequestedBytes.Length == 0) return;
            if (RequestedClient == null) return;
            lock (mlClientSocks)
            {
                try
                {
                    
                    cn = RequestedClient;
 
                    cn.Tx = new byte[RequestedBytes.Length];
                }
                catch (Exception ex)
                {
                    //log.WriteToLogNow(ex.ToString());
                }


                try
                {
                    if (cn != null)
                    {
                        if (cn.tclient != null)
                        {
                            if (cn.tclient.Client.Connected)
                            {
                               
                                cn.Tx = RequestedBytes;
                              
                                cn.tclient.GetStream().BeginWrite(cn.Tx, 0, cn.Tx.Length, OnCompleteWriteToClientStream, cn.tclient);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //log.WriteToLogNow(ex.ToString());
                }
            }
        }

        //****************************************************************************
        public void Dispose()
        //****************************************************************************
        {
            Stop();
        }

        //****************************************************************************
        public void Stop()
        //****************************************************************************
        {

        }
    }
}
