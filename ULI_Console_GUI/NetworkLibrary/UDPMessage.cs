/*
 * Created by SharpDevelop.
 * User: holar
 * Date: 10/22/2013
 * Time: 10:52 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
//using LogLibrary;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPLibrary
{
    // The link below is titled: Using an Asynchronous Client Socket
    //    http://msdn.microsoft.com/en-us/library/bbx2eya8.aspx

    // Excerpt of introduction
    //
    // An asynchronous client socket does not suspend the application while waiting for 
    // network operations to complete. Instead, it uses the standard .NET Framework 
    // asynchronous programming model to process the network connection on one thread 
    // while the application continues to run on the original thread. Asynchronous sockets 
    // are appropriate for applications that make heavy use of the network or that cannot 
    // wait for network operations to complete before continuing.
    //
    // The Socket class follows the .NET Framework naming pattern for asynchronous methods; 
    // for example, the synchronous Receive method corresponds to the asynchronous BeginReceive 
    // and EndReceive methods.
    //
    // Asynchronous operations require a callback method to return the result of the operation. 
    // If your application does not need to know the result, then no callback method is required. 
    // The example code in this section demonstrates using a method to start connecting to a network 
    // device and a callback method to complete the connection, a method to start sending data and 
    // a callback method to complete the send, and a method to start receiving data and a callback 
    // method to end receiving data.
    //
    // Asynchronous sockets use multiple threads from the system thread pool to process network
    // connections. One thread is responsible for initiating the sending or receiving of data;
    // other threads complete the connection to the network device and send or receive the data. 
    // In the following examples, instances of the System.Threading.ManualResetEvent class are 
    // used to suspend execution of the main thread and signal when execution can continue.


    public class StateObject
    {
        // Client socket.
        public Socket socket = null;
        // Size of receive buffer.
        public const int bufferSize = 8192;
        // Receive buffer.
        public byte[] byteData = new byte[bufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
    }

    // Define a class to hold custom event info 
    public class UDPMessageEventArgs : EventArgs
    {
        public UDPMessageEventArgs(byte[] s, string IPAddressMSG)
        {
            message = s;
            iPAddress = IPAddressMSG;
        }
        private byte[] message;
        private string iPAddress;
        public byte[] Message
        {
            get { return message; }
            set { message = value; }
        }
        public string IPAddress
        {
            get { return iPAddress; }
            set { iPAddress = value; }
        }
    }//end class

    // Define a class to hold custom event info 
    public class UDPRemotePortClosedEventArgs : EventArgs
    {
        public UDPRemotePortClosedEventArgs(string s, string IPAddressMSG)
        {
            message = s;
            iPAddress = IPAddressMSG;
        }
        private string message;
        private string iPAddress;
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        public string IPAddress
        {
            get { return iPAddress; }
            set { iPAddress = value; }
        }
    }//end class

    /// <summary>
    /// Description of UDPMessage.
    /// </summary>
    public class UDPMessage
    {
        readonly Socket udpSocket;

        //custom events
        //http://msdn.microsoft.com/en-us/library/w369ty8x.aspx

        // The EventHandler<TEventArgs> class is:
        // 1) A "Delegate" pattern.
        // 2) A system namespace class
        // 3) Represents the method that will handle an event when the Evernt provides data.
        // 
        // EventHandler<TEventArgs> Delegate
        public event EventHandler<UDPMessageEventArgs> MessageReceived;
        public event EventHandler<UDPRemotePortClosedEventArgs> RemotePortClosedEvent;

       // LogWriter log = LogWriter.Instance;
        //************************************
        public UDPMessage(string localPort, string remotePort, string localIP)
        //************************************
        {
            EndPoint epLocal;
            try
            {
                // set up socket
                udpSocket = new Socket(AddressFamily.InterNetwork, // Address IPV4
                                       SocketType.Dgram,           // Datagram (connectionless, unreliable messages of a fixed max length)
                                       ProtocolType.Udp);          // User Datagram Protocol
                                                                   //
                                                                   // Note: The socket class uses the ProtocolType enumeration to inform 
                                                                   // the Windows Sockets API of the requested protocol. low-level driver 
                                                                   // software for the requested protocol must be present  on the computer 
                                                                   // for the Socket to be created successfully.

                // bind udpSocket to our IP address and specify the port
                epLocal = new IPEndPoint(IPAddress.Parse(localIP), Convert.ToInt32(localPort));
                udpSocket.Bind(epLocal);

                IPEndPoint receiver = new IPEndPoint(IPAddress.Any, Convert.ToInt32(remotePort));

                //The epSender identifies the incoming clients
                EndPoint epReceiver = (EndPoint)receiver;

                // Create the state object.
                StateObject state = new StateObject();

                //Start receiving data
                state.socket = udpSocket;

                // Fall into the BeginReceiveFrom method.
                //
                // When a message is received, it's processed by our 'OperatorCallBack' method below.
                udpSocket.BeginReceiveFrom(state.byteData,                      // Byte[] {data buffer}
                                           0,                                   // Int32 {Starting position in data buffer for data}
                                           StateObject.bufferSize,              // Int32 {number of bytes to receive}
                                           SocketFlags.None,                    // SocketFlags {use no flags for this receive operation}
                                           ref epReceiver,                      // EndPoint {source of data}
                                           new AsyncCallback(OperatorCallBack), // AsyncCallback method, passed control when a UDP message is received. See below.
                                           state);                              // Object {An object that contains state information for this request.}

                // We fell out of the BeginReceiveFrom method. Some sort of exit thingie has happened
            }
            catch (Exception ex)
            {
                //log.WriteToLogNow(ex.ToString());
            }

        }//end func

        //************************************
        //
        // An asyncronous result message (ar) has been received
        private void OperatorCallBack(IAsyncResult ar)
        //************************************
        {
            int size = 0;

            //http://msdn.microsoft.com/en-us/library/bbx2eya8.aspx
            StateObject state = (StateObject)ar.AsyncState;
            Socket serverSocket = state.socket;

            IPEndPoint ipeSender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint epSender = (EndPoint)ipeSender;

            try
            {
                // Acquire the data from the sender and note the quantity in 'size'
                size = serverSocket.EndReceiveFrom(ar, ref epSender);

                // check if there's actually information
            }
            catch (System.Net.Sockets.SocketException exp)
            {
                // There was an exception when we attempted to do the receive operation.
                //
                // Socket error = #10054. A socket error 10054 may be the result of the 
                // remote server or some other piece of network equipment forcibly closing 
                // or resetting the connection. ... The remote server was stopped or 
                // restarted. The remote network interface is disabled for some reason.
                if (exp.ErrorCode == 10054)
                {
                    //ICMP  host not found
                    //http://social.msdn.microsoft.com/Forums/en-US/bcc98596-c9c7-48f1-92ca-021bbe42cbb1/an-existing-connection-was-forcibly-closed-by-the-remote-host?forum=netfxnetcom
                    OnRemotePortClosedEvent(new UDPRemotePortClosedEventArgs("Remote port is closed", epSender.ToString()));
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                //log.WriteToLogNow(ex.ToString());
            }

            if (size > 0)
            {
                // We have some data.
                //
                // converts from data[] to string
                byte[] data = new byte[size];

                // If we have enough data
                if (state.byteData.Length < size)
                {
                    size = state.byteData.Length;
                }
                Array.Copy(state.byteData, data, size);

                OnMessageReceived(new UDPMessageEventArgs(data, epSender.ToString()));
            }

            try
            {
                // Start listening to the message sent by the user.
                // Pass this data to the state machine for action as needed.
                serverSocket.BeginReceiveFrom(state.byteData,
                                              0,
                                              StateObject.bufferSize,
                                              SocketFlags.None,
                                              ref epSender,
                                              new AsyncCallback(OperatorCallBack),
                                              state);

            }
            catch (Exception ex)
            {

                //log.WriteToLogNow(ex.ToString());
            }
        }//end func


        //************************************
        public void Send(byte[] msg, string remoteIP, string remotePort, string localIP)
        //************************************
        {

            try
            {
                // connect to remote ip and port 
                IPEndPoint ipeSender = new IPEndPoint(IPAddress.Parse(remoteIP), Convert.ToInt32(remotePort));
                EndPoint epRemote = (EndPoint)ipeSender;

                //Send the message to all users
                if (udpSocket != null)
                {
                    udpSocket.BeginSendTo(msg, 0, msg.Length, SocketFlags.None, epRemote,
                                        new AsyncCallback(OnSend), epRemote);
                }
            }
            catch (Exception ex)
            {
                //log.WriteToLogNow(ex.ToString());
            }

        }//end func


        //************************************
        public void OnSend(IAsyncResult ar)
        //************************************
        {
            try
            {
                if (udpSocket != null)
                {
                    udpSocket.EndSendTo(ar);
                }
            }
            catch (Exception ex)
            {
                //log.WriteToLogNow(ex.ToString());
            }
        }

        //************************************
        protected virtual void OnMessageReceived(UDPMessageEventArgs e)
        //************************************
        {
            // Create a copy of the class variable: MessageReceived object. It is of type: 
            // public event EventHandler<UDPMessageEventArgs>
            EventHandler<UDPMessageEventArgs> eventCopy = MessageReceived;

            // Now, if the above copy operation worked, add the message arguments to the event.
            if (eventCopy != null)
            {
                eventCopy(this, e);
            }
        }//end func

        //************************************
        protected virtual void OnRemotePortClosedEvent(UDPRemotePortClosedEventArgs e)
        //************************************
        {
            EventHandler<UDPRemotePortClosedEventArgs> eventCopy = RemotePortClosedEvent;
            if (eventCopy != null)
            {
                eventCopy(this, e);
            }
        }//end func


        //************************************
        public void CloseConnection()
        //************************************
        {
            if (udpSocket != null)
            {
                try
                {
                    udpSocket.Shutdown(SocketShutdown.Both);
                    udpSocket.Close();
                }
                catch (Exception ex)
                {
                    //log.WriteToLogNow(ex.ToString());
                }
            }
        }


    }//end class UDPMessage
}
