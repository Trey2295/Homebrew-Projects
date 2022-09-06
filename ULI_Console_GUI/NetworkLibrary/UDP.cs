using System;
//using LogLibrary;


namespace UDPLibrary
{





    public class GIMCClosedPortEventArgs : EventArgs
    {
        public void GIMCMessageDataEventArgs()
        {
           //function left empty
        }
       
    }//end class
     /// <summary>
     /// implements the GIMC commands
     /// </summary>
    public class UDP_GIMC
    {

        public event EventHandler<UdpDataReceiveEventArgsQueueEventArgs> MessageDataReceived;
        public event EventHandler<GIMCClosedPortEventArgs> PortClosedEvent;

        int numberOfChannels = 0;
        //int payloadSize = 0;


        bool _connectionIsOpen = false;
        string _remoteIP = string.Empty;
        int _remotePort = 11111;
        string _currentTime = DateTime.Now.ToString();
        UDPMessage udpMessage;

       

        public enum HEADER_ENTITY_CHAR0_TYPE :  byte
        {		
          INITIAL_CONNECTION=0,		
          VARIABLE_UPDATE=1,
          DATA_FOR_GUI=2,
          START_STREAMING_DATA=3,
          STOP_STREAMING_DATA=4,
          ACK=5,
          NACK=6,
          VARIABLE_PACKET_UPDATE=7,   //this was added to allow multiple packets of floats to be updated
          DATA_FOR_GUI_WITH_INFO=8    //this will specify the length of the fields and the type
        };



        public enum UDP_PACKET_DATA_OFFSET:byte
        {
            PACKET_TYPE = 0,
            NUMBER_OF_CHANNELS_HIGH = 1,
            NUMBER_OF_CHANNELS_LOW = 2,
            CHANNEL_PAYLOAD_SIZE = 3,
            TIME_STAMP_1 = 4, //32 bit counter 
            TIME_STAMP_2 = 5,
            TIME_STAMP_3 = 6,
            TIME_STAMP_4 = 7,
            DATA_OFFSET = 8
        };


        public string LocalIP { get; set; }
        public string LocalPort { get; set; }

        public bool IsReceiveEnabled { get; set; }

      //  readonly LogWriter log = LogWriter.Instance;

        //constructor
        //**********************************************
        public UDP_GIMC(string localPort)
        //**********************************************
        {
         
            LocalPort = localPort;
            IsReceiveEnabled = false; 



        }//end func

        //**********************************************
        private void UdpMessage_RemotePortClosedEvent(object sender, UDPRemotePortClosedEventArgs e)
        //**********************************************
        {


            _connectionIsOpen = false;

            if (udpMessage != null)
            {
                udpMessage.CloseConnection();
            }
            OnPortClosed(new GIMCClosedPortEventArgs());
        }

        //**********************************************
        private void UdpMessage_MessageReceived(object sender, UDPMessageEventArgs e)
        //**********************************************
        {


            if (IsReceiveEnabled == false) return;

            //decode the header
            if (e.Message[(int)UDP_PACKET_DATA_OFFSET.PACKET_TYPE] == (byte)HEADER_ENTITY_CHAR0_TYPE.DATA_FOR_GUI_WITH_INFO)  //PACKET_TYPE = 0,
            {

                numberOfChannels = (int)e.Message[(int)UDP_PACKET_DATA_OFFSET.NUMBER_OF_CHANNELS_LOW] + (int)e.Message[(int)UDP_PACKET_DATA_OFFSET.NUMBER_OF_CHANNELS_HIGH] *256;
                //check the value 

                int sizeOfDataIndex = (int)UDP_PACKET_DATA_OFFSET.CHANNEL_PAYLOAD_SIZE;

                if(sizeOfDataIndex >= e.Message.Length)
                {
                    //error
                    return;
                }
                int sizeOfData = (int)e.Message[sizeOfDataIndex];

                //send the data to the board
                //
                UdpDataReceiveEventArgsQueueEventArgs udpDataTransmitEventArgs = new UdpDataReceiveEventArgsQueueEventArgs();
                for (int a = 0; a < numberOfChannels; a++)
                {
                    

                    TransmitDataPayload payload = new TransmitDataPayload();

                    //determine the type
                    int dataTypeIndex = (int)UDP_PACKET_DATA_OFFSET.DATA_OFFSET + 2 + a * 7;
                    if(dataTypeIndex >= e.Message.Length)
                    {
                        //error
                        continue;
                    }
                   var dataType =  e.Message[dataTypeIndex];  //type


                   int offset = (int)UDP_PACKET_DATA_OFFSET.DATA_OFFSET + 3 + (3 + sizeOfData) * a;
                    if (offset >= e.Message.Length)
                    {
                        //error
                        continue;
                    }

                    switch (dataType)
                   {

                       
                        case (byte)TransmitDataPayload.DataType.UINT32:
                            payload.Type = TransmitDataPayload.DataType.UINT32;
                            UInt32[] data = new UInt32[1];
                            Buffer.BlockCopy(e.Message, offset, data, 0, sizeof(UInt32));
                            payload.Data = (object)data[0];
                            break;

                        case (byte)TransmitDataPayload.DataType.UINT16:
                            payload.Type = TransmitDataPayload.DataType.UINT16;
                            UInt16[] data1 = new UInt16[1];
                            Buffer.BlockCopy(e.Message, offset, data1, 0, sizeof(UInt16));
                            payload.Data = (object)data1[0];
                            break;

                        case (byte)TransmitDataPayload.DataType.INT16:
                            payload.Type = TransmitDataPayload.DataType.INT16;
                            Int16[] data2 = new Int16[1];
                            Buffer.BlockCopy(e.Message, offset, data2, 0, sizeof(Int16));
                            payload.Data = (object)data2[0];
                            break;

                        case (byte)TransmitDataPayload.DataType.FLOAT:
                            payload.Type = TransmitDataPayload.DataType.FLOAT;

                            float[] data3 = new float[1];

                            Buffer.BlockCopy(e.Message, offset, data3, 0, sizeof(float));
                            payload.Data = (object)data3[0];
                            break;

                        case (byte)TransmitDataPayload.DataType.INT32:
                            payload.Type = TransmitDataPayload.DataType.INT32;
                            Int32[] data4 = new Int32[1];
                            Buffer.BlockCopy(e.Message, offset, data4, 0, sizeof(Int32));
                            payload.Data = (object)data4[0];
                            break;

                        case (byte)TransmitDataPayload.DataType.UINT8:
                            payload.Type = TransmitDataPayload.DataType.UINT8;
                            char[] data5 = new char[1];
                            Buffer.BlockCopy(e.Message, offset, data5, 0, sizeof(char));
                            payload.Data = (object)data5[0];
                            break;

                        case (byte)TransmitDataPayload.DataType.INT8:
                            payload.Type = TransmitDataPayload.DataType.INT8;
                            char[] data6 = new char[1];
                            Buffer.BlockCopy(e.Message, offset, data6, 0, sizeof(char));
                            payload.Data = (object)data6[0];
                            break;
                        case (byte)TransmitDataPayload.DataType.BOOL:
                            payload.Type = TransmitDataPayload.DataType.BOOL;
                            Int32[] data7 = new Int32[1];
                            Buffer.BlockCopy(e.Message, offset, data7, 0, sizeof(Int32));

                            bool resultBool;
                            if(data7[0] >0)
                            {
                                resultBool = true;
                                payload.Data = (object)resultBool;
                            }
                            else
                            {
                                resultBool = false;
                                payload.Data = (object)resultBool;
                            }
                           
                            break;

                        default://error
                            continue;
                            

                   }//end switch



                    int channelNumberLowIndex = (int)UDP_PACKET_DATA_OFFSET.DATA_OFFSET + 1 + a * 7;
                    int channelNumberHighIndex = (int)UDP_PACKET_DATA_OFFSET.DATA_OFFSET + a * 7;

                    if (channelNumberLowIndex >= e.Message.Length)
                    {
                        //error
                        continue;
                    }

                    if (channelNumberHighIndex >= e.Message.Length)
                    {
                        //error
                        continue;
                    }

                    payload.ChannelNumber = (int)e.Message[channelNumberLowIndex] +(int)e.Message[channelNumberHighIndex] * 256; //channel
                   

                    udpDataTransmitEventArgs.TransmitList.Add(payload);



                }//end for
                
               

                OnMessageReceived(udpDataTransmitEventArgs);


            }//end func

        }


      


        //************************************
        protected virtual void OnMessageReceived(UdpDataReceiveEventArgsQueueEventArgs e)
        //************************************
        {
            MessageDataReceived?.Invoke(this, e);
        }//end func


        //**********************************************
        //public bool SendFloatValue(int channel, float value)
        ////**********************************************
        //{
        //    var byteArray = new byte[6 + (int)UDP_PACKET_DATA_OFFSET.DATA_OFFSET];
        //    float[] tempFloat = new float[1];
        //    tempFloat[0] = value;

        //    //add the header
        //    byteArray[(int)UDP_PACKET_DATA_OFFSET.PACKET_TYPE] = (byte)HEADER_ENTITY_CHAR0_TYPE.VARIABLE_PACKET_UPDATE;
        //    byteArray[(int)UDP_PACKET_DATA_OFFSET.NUMBER_OF_CHANNELS_HIGH] = 0;
        //    byteArray[(int)UDP_PACKET_DATA_OFFSET.NUMBER_OF_CHANNELS_LOW] = 1;
        //    byteArray[(int)UDP_PACKET_DATA_OFFSET.CHANNEL_PAYLOAD_SIZE] = 4;


        //    //channel
        //    byteArray[(int)UDP_PACKET_DATA_OFFSET.DATA_OFFSET] = (byte)channel;
        //    byteArray[(int)UDP_PACKET_DATA_OFFSET.DATA_OFFSET + 1] = 0;
        //    byteArray[(int)UDP_PACKET_DATA_OFFSET.DATA_OFFSET + 2] = 4;

        //    int offset = (int)UDP_PACKET_DATA_OFFSET.DATA_OFFSET + 3;
        //    Buffer.BlockCopy(tempFloat, 0, byteArray, offset, sizeof(float));



        //    return Send(byteArray, _remoteIP, _remotePort.ToString());

        //}//end func

        //************************************
        //public bool SendFloatArrayData(float[] data)
        ////************************************
        //{
        //    var byteArray = new byte[data.Length * 7 + (int)UDP_PACKET_DATA_OFFSET.DATA_OFFSET];

        //    //add the header
        //    byteArray[(int)UDP_PACKET_DATA_OFFSET.PACKET_TYPE] = (byte)HEADER_ENTITY_CHAR0_TYPE.VARIABLE_PACKET_UPDATE;
        //    byteArray[(int)UDP_PACKET_DATA_OFFSET.NUMBER_OF_CHANNELS_HIGH] = 0;
        //    byteArray[(int)UDP_PACKET_DATA_OFFSET.NUMBER_OF_CHANNELS_LOW] = (byte)data.Length;
        //    byteArray[(int)UDP_PACKET_DATA_OFFSET.CHANNEL_PAYLOAD_SIZE] = 4;

        //    for (int a = 0; a < data.Length; a++)
        //    {
        //        //channel
        //        byteArray[(int)UDP_PACKET_DATA_OFFSET.DATA_OFFSET + a * 7] = (byte)a; //channel
        //        byteArray[(int)UDP_PACKET_DATA_OFFSET.DATA_OFFSET + 1 + a * 7] = 4;  //type

        //        int offset = (int)UDP_PACKET_DATA_OFFSET.DATA_OFFSET + 3 + (3 + sizeof(float)) * a;
        //        Buffer.BlockCopy(data, sizeof(float) * a, byteArray, offset, sizeof(float));
        //    }


        //    return Send(byteArray, _remoteIP, _remotePort.ToString());

        //}//end func


        /// <summary>
        /// can send multiple channels with different payload(floats, ints, etc.) at once
        /// </summary>
        /// <param name = "queue" ></ param >
        /// < returns ></ returns >
        //************************************
        public bool SendData(UdpDataTransmitEventArgsQueue queue,System.UInt32 counter)
        //************************************
        {

            int length = queue.TransmitQueue.Count;

            if (length == 0) return false;

            var byteArray = new byte[(int)UDP_PACKET_DATA_OFFSET.DATA_OFFSET + length *7];

            ////add the header
            byteArray[(int)UDP_PACKET_DATA_OFFSET.PACKET_TYPE] = (byte)HEADER_ENTITY_CHAR0_TYPE.VARIABLE_PACKET_UPDATE;
            byteArray[(int)UDP_PACKET_DATA_OFFSET.NUMBER_OF_CHANNELS_HIGH] = 0;
            byteArray[(int)UDP_PACKET_DATA_OFFSET.NUMBER_OF_CHANNELS_LOW] = (byte)length; 
            byteArray[(int)UDP_PACKET_DATA_OFFSET.CHANNEL_PAYLOAD_SIZE] = 4;

            //add the counter
            uint[] intData = new UInt32[1];
            intData[0] = counter;
            Buffer.BlockCopy(intData, 0, byteArray, (int)UDP_PACKET_DATA_OFFSET.TIME_STAMP_1, sizeof(uint));


            //go through the queeue and dequeue all the channels and add them in the buffer
            for (int a = 0; a < length; a++)
            {

                try
                {
                    var dequeuedValue = queue.TransmitQueue.Dequeue();
                    
                    var sizeOfData = 4;

                    byteArray[(int)UDP_PACKET_DATA_OFFSET.DATA_OFFSET    + a * 7] = 0; //channel high 
                    byteArray[(int)UDP_PACKET_DATA_OFFSET.DATA_OFFSET + 1+ a * 7] = (byte) dequeuedValue.Channel; //channel low 
                    byteArray[(int)UDP_PACKET_DATA_OFFSET.DATA_OFFSET + 2 + a * 7] = (byte)dequeuedValue.Type;  //type

                    int offset = (int)UDP_PACKET_DATA_OFFSET.DATA_OFFSET + 3 + (3 + sizeOfData) * a;


                    switch (dequeuedValue.Type)
                    {

                        case TransmitDataPayload.DataType.FLOAT:
                            float[] data = new float[1];
                            data[0] = (float)dequeuedValue.Data;
                            Buffer.BlockCopy(data, 0, byteArray, offset, sizeof(float));
                            break;

                        case TransmitDataPayload.DataType.INT16:
                            Int16[] data1 = new Int16[1];
                            data1[0] = (Int16)dequeuedValue.Data;
                            Buffer.BlockCopy(data1, 0, byteArray, offset, sizeof(Int16));
                            break;

                        case TransmitDataPayload.DataType.INT32:
                            Int32[] data2 = new Int32[1];
                            data2[0] = (Int32)dequeuedValue.Data;
                            Buffer.BlockCopy(data2, 0, byteArray, offset, sizeof(Int32));
                            break;

                        case TransmitDataPayload.DataType.INT8:
                            char[] data3 = new char[1];
                            data3[0] = (char)dequeuedValue.Data;
                            Buffer.BlockCopy(data3, 0, byteArray, offset, sizeof(char));
                            break;

                        case TransmitDataPayload.DataType.UINT16:
                            UInt16[] data4 = new UInt16[1];
                            data4[0] = (UInt16) dequeuedValue.Data;
                            Buffer.BlockCopy(data4, 0, byteArray, offset, sizeof(UInt16));
                            break;

                        case TransmitDataPayload.DataType.UINT32:
                            UInt32[] data5 = new UInt32[1];
                            data5[0] = (UInt32)dequeuedValue.Data;
                            Buffer.BlockCopy(data5, 0, byteArray, offset, sizeof(UInt32));
                            break;

                        case TransmitDataPayload.DataType.UINT8:
                            char[] data6 = new  char[1];
                            data6[0] = ( char)dequeuedValue.Data;
                            Buffer.BlockCopy(data6, 0, byteArray, offset, sizeof(char));
                            break;
                        case TransmitDataPayload.DataType.BOOL:
                            Int32[] data7 = new Int32[1];
                            data7[0] = 0;
                            if ((bool)dequeuedValue.Data == true)
                            {
                                data7[0] = 1;
                            }
                            else
                            {
                                data7[0] = 0;
                            }
                            Buffer.BlockCopy(data7, 0, byteArray, offset, sizeof(Int32));
                            break;


                    }
                    
                }
                catch(Exception ex)
                {

                    //log.WriteToLog(ex.ToString());
                    return false;
                }

                    //    //channel
           
            }
            //

            return Send(byteArray, _remoteIP, _remotePort.ToString());
        }//end func


        //**********************************************
        public bool Connect(string address, int port, string localIP)
        //**********************************************
        {
            _remoteIP = address;
            _remotePort = port;
            _connectionIsOpen = true;
            LocalIP = localIP;

            udpMessage = new UDPMessage(LocalPort,"11111" , localIP);
            udpMessage.MessageReceived += new EventHandler<UDPMessageEventArgs>(UdpMessage_MessageReceived);
            udpMessage.RemotePortClosedEvent += new EventHandler<UDPRemotePortClosedEventArgs>(UdpMessage_RemotePortClosedEvent);
            return true;
        }//end func



        //**********************************************
        public  void Close()
        //**********************************************
        {
            if (_connectionIsOpen == false)
            {
                return ;
            }

            IsReceiveEnabled = false; 

            _connectionIsOpen = false;
            if (udpMessage != null)
            {
                udpMessage.CloseConnection();
            }
        }//end func

        ////**********************************************
        //public bool AreValuesAvailable()
        ////**********************************************
        //{
        //    return false;
        //}

       

        
        //************************************
        public bool StartStreamingData()
        //************************************
        {
            if(_connectionIsOpen ==false)
            {
                return false;
            }

            byte[] msg = new byte[1];
            msg[0] = (byte)HEADER_ENTITY_CHAR0_TYPE.START_STREAMING_DATA; 
            Send(msg, _remoteIP, _remotePort.ToString());
            Console.WriteLine(msg[0]);
            return true;
        }//end func

        //************************************
        public bool StopStreamingData()
        //************************************
        {
            if (_connectionIsOpen == false)
            {
                return false;
            }

            byte[] msg = new byte[1];
            msg[0] = (byte)HEADER_ENTITY_CHAR0_TYPE.STOP_STREAMING_DATA ;
            Send(msg, _remoteIP, _remotePort.ToString());
            return true;
        }//end func


        //************************************
        public bool SendInitialConnectionPacket()
        //************************************
        {
            if (_connectionIsOpen == false)
            {
                return false;
            }
            byte[] msg = new byte[1];
            string Time = DateTime.Now.ToString(); 
            msg[0] = (byte)HEADER_ENTITY_CHAR0_TYPE.INITIAL_CONNECTION;
            Send(msg, _remoteIP, _remotePort.ToString());
            return true;
        }//end func

        
        //************************************
        public bool Send(byte [] msg, string remoteIP, string remotePort)
        //************************************
        {
            if (_connectionIsOpen != true) return false;

            if (udpMessage != null)
            {
                udpMessage.Send(msg, remoteIP, remotePort, LocalIP);
            }
            
            return true;
        }//end func


       

        ////************************************
        //protected virtual void OnMessageDataReceived(UdpDataReceiveEventArgsQueueEventArgs e)
        ////************************************
        //{
        //    MessageDataReceived?.Invoke(this, e);
        //}//end func


        //************************************
        protected virtual void OnPortClosed(GIMCClosedPortEventArgs e)
        //************************************
        {
            PortClosedEvent?.Invoke(this, e);
        }//end func

    }//end class
}
