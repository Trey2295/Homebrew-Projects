using System;
using System.Collections.Generic;

namespace UDPLibrary
{
    /// <summary>
    /// event sent when a user control is deleted
    /// it is informing the main form that it has to update controls 
    /// </summary>
    public class UserControlDeletedEvent : EventArgs
    {
        public UserControlDeletedEvent(int id)
        {
            ID = id;

        }
        public int ID { get; set; }
    }


    /// <summary>
    /// this is a event fired when an udp packet is received 
    /// </summary>
    public class UdpDataReceivedEventArgs : EventArgs
    {
        public enum DataType { UINT32 = 1, UINT16 = 2, INT16 = 3, FLOAT = 4, INT32 = 5, UINT8 = 6, INT8 = 7, BOOL = 8 };
        public UdpDataReceivedEventArgs(object data, UdpDataReceivedEventArgs.DataType type, int channel)
        {
            Data = data;
            Channel = channel;
            Type = type;

        }

        /// <summary>
        /// the channel number received from the GIMC board
        /// </summary>
        public int Channel { get; set; }

        /// <summary>
        /// this is the storage for a generic objec that has to the converted to a specific type such as float
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// keeps the type of the object it is received form the GIMC board
        /// </summary>
        public UdpDataReceivedEventArgs.DataType Type { get; set; }

    }


    /// <summary>
    /// event that is fired by the sending user controls and contains data to be sent over udp to the GIMC board
    /// </summary>
    public class UdpDataTransmitEventArgs : EventArgs
    {
        public enum DataType { UINT32 = 1, UINT16 = 2, INT16 = 3, FLOAT = 4, INT32 = 5, UINT8 = 6, INT8 = 7, BOOL = 8 };
        public UdpDataTransmitEventArgs(object data, UdpDataTransmitEventArgs.DataType type, int channel)
        {
            Data = data;
            Channel = channel;
            Type = type;
        }

        /// <summary>
        /// the channel number received from the GIMC board
        /// </summary>
        public int Channel { get; set; }

        /// <summary>
        /// this is the storage for a generic objec that has to the converted to a specific type such as float
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// keeps the type of the object it is received form the GIMC board
        /// </summary>
        public UdpDataTransmitEventArgs.DataType Type { get; set; }

    }

    /// <summary>
    /// used by the main form to announce that the datarate that is used to send the data to the board has changed
    /// </summary>
    public class SendDataRateChangedEventArgs : EventArgs
    {
        public int MillisecondSendRate { get; set; }

        public SendDataRateChangedEventArgs(int millisecondSendRate)
        {
            MillisecondSendRate = millisecondSendRate;

        }
    }

    public class DataStopEventArgs : EventArgs
    {


    }

    public class DataStartEventArgs : EventArgs
    {


    }



    /// <summary>
    /// this allows to packetize multiple data channels for UDP communication
    /// </summary>
    public class TransmitDataPayload
    {
        public enum DataType { UINT32 = 1, UINT16 = 2, INT16 = 3, FLOAT = 4, INT32 = 5, UINT8 = 6, INT8 = 7 ,BOOL = 8};
        public enum ChannelType
        {

            M1_TORQUE_CH1 = 1,  //udp channel 1 Boolean 1-closed 0 open
            M2_TORQUE_CH2 = 2,  //udp channel 2 Boolean 1-closed 0 open
            M3_TORQUE_CH3 = 3,  //udp channel 3 Boolean 1-closed 0 open
            M4_TORQUE_CH4 = 4,  //udp channel 4 Boolean 1-closed 0 open
            M5_TORQUE_CH5 = 5,  //udp channel 5 Boolean 1-closed 0 open
            M6_TORQUE_CH6 = 6,  //udp channel 6 Boolean 1-closed 0 open
            M7_TORQUE_CH7 = 7,  //udp channel 7 Boolean 1-closed 0 open
            M8_TORQUE_CH8 = 8,  //udp channel 8 Boolean 1-closed 0 open

            M1_SPEED_CH9  = 9,  //udp channel 9 Boolean 1-closed 0 open
            M2_SPEED_CH10 = 10, //udp channel 10 Boolean 1-closed 0 open
            M3_SPEED_CH11 = 11, //udp channel 11 Boolean 1-closed 0 open
            M4_SPEED_CH12 = 12, //udp channel 12 Boolean 1-closed 0 open
            M5_SPEED_CH13 = 13, //udp channel 13 Boolean 1-closed 0 open
            M6_SPEED_CH14 = 14, //udp channel 14 Boolean 1-closed 0 open
            M7_SPEED_CH15 = 15, //udp channel 15 Boolean 1-closed 0 open
            M8_SPEED_CH16 = 16, //udp channel 16 Boolean 1-closed 0 open

            MASTER_FEEDBACK_CH17 = 17,     //udp channel 17 Boolean 1-closed 0 open
            RIGHT_WING_FEEDBACK_CH18 = 18, //udp channel 18 Boolean 1-closed 0 open
            LEFT_WING_FEEDBACK_CH19 = 19,  //udp channel 19 Boolean 1-closed 0 open
            TAIL_WING_FEEDBACK_CH20 = 20,  //udp channel 20 Boolean 1-closed 0 open

            CT1_CONTACTOR_CH21 = 21,//udp channel 21 Boolean 1-closed 0 open
            CT2_CONTACTOR_CH22 = 22,//udp channel 22 Boolean 1-closed 0 open
            CT3_CONTACTOR_CH23 = 23,//udp channel 23 Boolean 1-closed 0 open
            CT4_CONTACTOR_CH24 = 24,//udp channel 24 Boolean 1-closed 0 open

            RL1_CONTACTOR_CH25 = 25,//udp channel 25 Boolean 1-closed 0 open
            RL2_CONTACTOR_CH26 = 26,//udp channel 26 Boolean 1-closed 0 open
            RL3_CONTACTOR_CH27 = 27,//udp channel 27 Boolean 1-closed 0 open
            RL4_CONTACTOR_CH28 = 28,//udp channel 28 Boolean 1-closed 0 open
            RL5_CONTACTOR_CH29 = 29,//udp channel 29 Boolean 1-closed 0 open
            RL6_CONTACTOR_CH30 = 30,//udp channel 30 Boolean 1-closed 0 open
            RL7_CONTACTOR_CH31 = 31,//udp channel 31 Boolean 1-closed 0 open
            RL8_CONTACTOR_CH32 = 32,//udp channel 32 Boolean 1-closed 0 open

            Partner_SPEED_1 = 37, //udp channel 37 float speed
            Partner_SPEED_2 = 38, //udp channel 38 float speed

            PID_P_SET_COMMAND_D1 = 40, //udp channel float sets the P param for Partner test Dyno 1
            PID_I_SET_COMMAND_D1 = 41, //udp channel float sets the I param for Partner test Dyno 1
            PID_D_SET_COMMAND_D1 = 42, //udp channel float sets the D param for Partner test Dyno 1
            SPEED_RATE_LIMIT_COMMAND_D1 = 43, //udp channel float sets the speed rate change in the PID param Dyno 1
            PID_MAX_TORQUE_LIMIT_D1 = 44, //udp channel float sets the max torque limit in the pid calculation Dyno 1
            PID_MIN_TORQUE_LIMIT_D1 = 45, //udp channel float sets the min torque limit in the pid calculation Dyno 1


            PID_P_SET_COMMAND_D2 = 50, //udp channel float sets the P param for Partner test Dyno 2
            PID_I_SET_COMMAND_D2 = 51, //udp channel float sets the I param for Partner test Dyno 2
            PID_D_SET_COMMAND_D2 = 52, //udp channel float sets the D param for Partner test Dyno 2
            SPEED_RATE_LIMIT_COMMAND_D2 = 53, //udp channel float sets the speed rate change in the PID param Dyno 1
            PID_MAX_TORQUE_LIMIT_D2 = 54, //udp channel float sets the max torque limit in the pid calculation Dyno 1
            PID_MIN_TORQUE_LIMIT_D2 = 55, //udp channel float sets the min torque limit in the pid calculation Dyno 1

            E_STOP_ALL = 99 ,
            
            CHANGE_STATE_D1 = 98,  // DYNO 1
            CHANGE_STATE_D2 = 88,  // DYNO 2

            FILTER_PID_DYNO_1 = 89, // Low pass filter for Dyno 1 
            FILTER_PID_DYNO_2 = 90, // Low pass filter for Dyno 2
           
        };
        public ChannelType Channel { get; set; }

        public int ChannelNumber { get; set; }
        /// <summary>
        /// this is the storage for a generic object that has to the converted to a specific type such as float
        /// </summary>
        public object Data { get; set; }

        public int Counter { get; set; }
        /// <summary>
        /// keeps the type of the object it is received form the GIMC board
        /// </summary>
        public TransmitDataPayload.DataType Type { get; set; }

        public DateTime TimeStamp { get; set; }


    }


    ///allows to transmit multiple values at once
    public class UdpDataTransmitEventArgsQueue : EventArgs
    {

        public Queue<TransmitDataPayload> TransmitQueue = new Queue<TransmitDataPayload>();
        public UdpDataTransmitEventArgsQueue()
        {

        }

        public bool AddValue(TransmitDataPayload value)
        {
            TransmitQueue.Enqueue(value);
            return true;
        }


    }//end class


    
    /// <summary>
    /// allows to receive multiple values at once
    /// </summary>
    public class UdpDataReceiveEventArgsQueueEventArgs : EventArgs
    {
      
        public List<TransmitDataPayload> TransmitList = new List<TransmitDataPayload>();
        public UdpDataReceiveEventArgsQueueEventArgs()
        {

        }

        public bool AddValue(TransmitDataPayload value)
        {
            TransmitList.Add(value);
            return true;
        }


    }//end class

  


}//
