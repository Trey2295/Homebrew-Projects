using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDPLibrary
{
    public class NEATPacket
    {
        //**********************************************
        public enum UDP_PACKET_DATA_OFFSET : byte
        //**********************************************
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

        //**********************************************
        public enum HEADER_ENTITY_CHAR0_TYPE : byte
        //**********************************************
        {
            INITIAL_CONNECTION = 0,
            VARIABLE_UPDATE = 1,
            DATA_FOR_GUI = 2,
            START_STREAMING_DATA = 3,
            STOP_STREAMING_DATA = 4,
            ACK = 5,
            NACK = 6,
            VARIABLE_PACKET_UPDATE = 7,   //this was added to allow multiple packets of floats to be updated
            DATA_FOR_GUI_WITH_INFO = 8    //this will specify the length of the fields and the type
        };



            //**********************************************
            public byte[] CreateFloatValuePacket(int channel, float value)
            //**********************************************
            {
                var byteArray = new byte[6 + (int)UDP_PACKET_DATA_OFFSET.DATA_OFFSET];
                float[] tempFloat = new float[1];
                tempFloat[0] = value;

                //add the header
                byteArray[(int)UDP_PACKET_DATA_OFFSET.PACKET_TYPE] = (byte)HEADER_ENTITY_CHAR0_TYPE.VARIABLE_PACKET_UPDATE;
                byteArray[(int)UDP_PACKET_DATA_OFFSET.NUMBER_OF_CHANNELS_HIGH] = 0;
                byteArray[(int)UDP_PACKET_DATA_OFFSET.NUMBER_OF_CHANNELS_LOW] = 1;
                byteArray[(int)UDP_PACKET_DATA_OFFSET.CHANNEL_PAYLOAD_SIZE] = 4;


                //channel
                byteArray[(int)UDP_PACKET_DATA_OFFSET.DATA_OFFSET] = (byte)channel;
                byteArray[(int)UDP_PACKET_DATA_OFFSET.DATA_OFFSET + 1] = 0;
                byteArray[(int)UDP_PACKET_DATA_OFFSET.DATA_OFFSET + 2] = 4;

                int offset = (int)UDP_PACKET_DATA_OFFSET.DATA_OFFSET + 3;
                Buffer.BlockCopy(tempFloat, 0, byteArray, offset, sizeof(float));

                return byteArray;

            }//end func

            //************************************
            public byte[] CreateFloatArrayData(float[] data)
            //************************************
            {
                var byteArray = new byte[data.Length * 7 + (int)UDP_PACKET_DATA_OFFSET.DATA_OFFSET];

                //add the header
                byteArray[(int)UDP_PACKET_DATA_OFFSET.PACKET_TYPE] = (byte)HEADER_ENTITY_CHAR0_TYPE.VARIABLE_PACKET_UPDATE;
                byteArray[(int)UDP_PACKET_DATA_OFFSET.NUMBER_OF_CHANNELS_HIGH] = 0;
                byteArray[(int)UDP_PACKET_DATA_OFFSET.NUMBER_OF_CHANNELS_LOW] = (byte)data.Length;
                byteArray[(int)UDP_PACKET_DATA_OFFSET.CHANNEL_PAYLOAD_SIZE] = 4;

                for (int a = 0; a < data.Length; a++)
                {
                    //channel
                    byteArray[(int)UDP_PACKET_DATA_OFFSET.DATA_OFFSET + a * 7] = (byte)a; //channel
                    byteArray[(int)UDP_PACKET_DATA_OFFSET.DATA_OFFSET + 1 + a * 7] = 4;  //type

                    int offset = (int)UDP_PACKET_DATA_OFFSET.DATA_OFFSET + 3 + (3 + sizeof(float)) * a;
                    Buffer.BlockCopy(data, sizeof(float) * a, byteArray, offset, sizeof(float));
                }

                return byteArray;

            }//end func

            //**********************************************
            private UdpDataReceiveEventArgsQueueEventArgs ParseMessageReceived( byte[] e)
            //**********************************************
            {
                int numberOfChannels = 0;
                UdpDataReceiveEventArgsQueueEventArgs udpDataTransmitEventArgs = new UdpDataReceiveEventArgsQueueEventArgs();


                //decode the header
                if (e[(int)UDP_PACKET_DATA_OFFSET.PACKET_TYPE] == (byte)HEADER_ENTITY_CHAR0_TYPE.DATA_FOR_GUI_WITH_INFO) //PACKET_TYPE = 0
                {

                    numberOfChannels = (int)e[(int)UDP_PACKET_DATA_OFFSET.NUMBER_OF_CHANNELS_LOW] + (int)e[(int)UDP_PACKET_DATA_OFFSET.NUMBER_OF_CHANNELS_HIGH] * 256;
                    //check the value 

                    int sizeOfDataIndex = (int)UDP_PACKET_DATA_OFFSET.CHANNEL_PAYLOAD_SIZE;

                    if (sizeOfDataIndex >= e.Length)
                    {
                        //error
                        return udpDataTransmitEventArgs;
                    }
                    int sizeOfData = (int)e[sizeOfDataIndex];

                    //send the data to the board
                    //

                    for (int a = 0; a < numberOfChannels; a++)
                    {

                        TransmitDataPayload payload = new TransmitDataPayload();

                        //determine the type
                        int dataTypeIndex = (int)UDP_PACKET_DATA_OFFSET.DATA_OFFSET + 2 + a * 7;
                        if (dataTypeIndex >= e.Length)
                        {
                            //error
                            continue;
                        }
                        var dataType = e[dataTypeIndex];  //type


                        int offset = (int)UDP_PACKET_DATA_OFFSET.DATA_OFFSET + 3 + (3 + sizeOfData) * a;
                        if (offset >= e.Length)
                        {
                            //error
                            continue;
                        }

                        switch (dataType)
                        {


                            case (byte)TransmitDataPayload.DataType.UINT32:
                                payload.Type = TransmitDataPayload.DataType.UINT32;
                                UInt32[] data = new UInt32[1];
                                Buffer.BlockCopy(e, offset, data, 0, sizeof(UInt32));
                                payload.Data = (object)data[0];
                                break;

                            case (byte)TransmitDataPayload.DataType.UINT16:
                                payload.Type = TransmitDataPayload.DataType.UINT16;
                                UInt16[] data1 = new UInt16[1];
                                Buffer.BlockCopy(e, offset, data1, 0, sizeof(UInt16));
                                payload.Data = (object)data1[0];
                                break;

                            case (byte)TransmitDataPayload.DataType.INT16:
                                payload.Type = TransmitDataPayload.DataType.INT16;
                                Int16[] data2 = new Int16[1];
                                Buffer.BlockCopy(e, offset, data2, 0, sizeof(Int16));
                                payload.Data = (object)data2[0];
                                break;

                            case (byte)TransmitDataPayload.DataType.FLOAT:
                                payload.Type = TransmitDataPayload.DataType.FLOAT;

                                float[] data3 = new float[1];

                                Buffer.BlockCopy(e, offset, data3, 0, sizeof(float));
                                payload.Data = (object)data3[0];
                                break;

                            case (byte)TransmitDataPayload.DataType.INT32:
                                payload.Type = TransmitDataPayload.DataType.INT32;
                                Int32[] data4 = new Int32[1];
                                Buffer.BlockCopy(e, offset, data4, 0, sizeof(Int32));
                                payload.Data = (object)data4[0];
                                break;

                            case (byte)TransmitDataPayload.DataType.UINT8:
                                payload.Type = TransmitDataPayload.DataType.UINT8;
                                char[] data5 = new char[1];
                                Buffer.BlockCopy(e, offset, data5, 0, sizeof(char));
                                payload.Data = (object)data5[0];
                                break;

                            case (byte)TransmitDataPayload.DataType.INT8:
                                payload.Type = TransmitDataPayload.DataType.INT8;
                                char[] data6 = new char[1];
                                Buffer.BlockCopy(e, offset, data6, 0, sizeof(char));
                                payload.Data = (object)data6[0];
                                break;
                            case (byte)TransmitDataPayload.DataType.BOOL:
                                payload.Type = TransmitDataPayload.DataType.BOOL;
                                Int32[] data7 = new Int32[1];
                                Buffer.BlockCopy(e, offset, data7, 0, sizeof(Int32));

                                bool resultBool;
                                if (data7[0] > 0)
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

                        if (channelNumberLowIndex >= e.Length)
                        {
                            //error
                            continue;
                        }

                        if (channelNumberHighIndex >= e.Length)
                        {
                            //error
                            continue;
                        }

                        payload.ChannelNumber = (int)e[channelNumberLowIndex] + (int)e[channelNumberHighIndex] * 256; //channel
                        udpDataTransmitEventArgs.TransmitList.Add(payload);

                    }//end for


                }
                return udpDataTransmitEventArgs;


            }//end func

            //************************************
            public byte[] CreateStopStreamingDataPacket()
            //************************************
            {
                byte[] msg = new byte[1];
                msg[0] = (byte)HEADER_ENTITY_CHAR0_TYPE.STOP_STREAMING_DATA;
                return msg;
            }//end func


            //************************************
            public byte[] CreateInitialConnectionPacket()
            //************************************
            {  
                byte[] msg = new byte[1];
                msg[0] = (byte)HEADER_ENTITY_CHAR0_TYPE.INITIAL_CONNECTION;
                return msg;
            }//end func

        }//end class
    }

