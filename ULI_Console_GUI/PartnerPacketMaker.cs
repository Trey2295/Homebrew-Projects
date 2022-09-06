using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UDPLibrary;
using LogLibrary;

namespace NeatGUI
{
    public class PartnerPacketMaker
    {
        private bool BM4Status = false;
        private bool GFM022Status = false;
        private bool GFM025Status = false;
        private bool ULI_Fault_Status = false;

        //
        LogWriter log;
        /// <summary>
        /// the key is the UDP channel
        /// </summary>
        public Dictionary<int, PartnerChannelData> PartnerPacketStructure = new Dictionary<int, PartnerChannelData>()
        {
                    {37,   new PartnerChannelData {Index=1,   Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="dyno1SpeedCommandEcho" } },       //1 Dyno #1 speed command echo
                    {301,  new PartnerChannelData {Index=2,   Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="torqueCommandM3" } },             //2
                    {401,  new PartnerChannelData {Index=3,   Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="torqueCommandM4" } },             //3
                    {701,  new PartnerChannelData {Index=4,   Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="torqueCommandM7" } },             //4
                    {801,  new PartnerChannelData {Index=5,   Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="torqueCommandM8" } },             //5
                    {92,   new PartnerChannelData {Index=6,   Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="Pparameter" } },                  //6 Dyno #1
                    {93,   new PartnerChannelData {Index=7,   Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="Iparameter" } },                  //7 Dyno #1 
                    {94,   new PartnerChannelData {Index=8,   Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="Dparameter" } },                  //8 Dyno #1
                    {303,  new PartnerChannelData {Index=9,   Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="moduleATemperatureM3" } },        //9
                    {304,  new PartnerChannelData {Index=10,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="moduleBTemperatureM3" } },        //10 
                    {305,  new PartnerChannelData {Index=11,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="moduleCTemperatureM3" } },        //11
                    {313,  new PartnerChannelData {Index=12,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="motorTemperatureM3" } },          //12
                    {319,  new PartnerChannelData {Index=13,  Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="lowWordInternalStatesM3" } },     //13
                    {320,  new PartnerChannelData {Index=14,  Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="highWordInternalStatesM3" } },    //14
                    {321,  new PartnerChannelData {Index=15,  Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="POSTWordFaultCodesM3" } },        //15
                    {322,  new PartnerChannelData {Index=16,  Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="RUNWordFaultCodesM3" } },         //16
                    {338,  new PartnerChannelData {Index=17,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="motorSpeedFeedbackM3" } },        //17
                    {339,  new PartnerChannelData {Index=18,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="electricalOutputFrequencyM3" } }, //18
                    {341,  new PartnerChannelData {Index=19,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseACurrentM3" } },             //19 
                    {342,  new PartnerChannelData {Index=20,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseBCurrentM3" } },             //20
                    {343,  new PartnerChannelData {Index=21,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseCCurrentM3" } },             //21  
                    {344,  new PartnerChannelData {Index=22,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="DCBusCurrentM3" } },              //22
                    {345,  new PartnerChannelData {Index=23,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="DCBusVoltageM3" } },              //23
                    {346,  new PartnerChannelData {Index=24,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="outputVoltageM3" } },             //24
                    {347,  new PartnerChannelData {Index=25,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseABVoltageM3" } },            //25
                    {348,  new PartnerChannelData {Index=26,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseBCVoltageM3" } },            //26
                    {349,  new PartnerChannelData {Index=27,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="fluxCommandM3" } },               //27
                    {350,  new PartnerChannelData {Index=28,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="fluxFeedbackM3" } },              //28
                    {351,  new PartnerChannelData {Index=29,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="idFeedbackM3" } },                //29
                    {352,  new PartnerChannelData {Index=30,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="iqFeedbackM3" } },                //30
                    {353,  new PartnerChannelData {Index=31,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="commandedTorqueM3" } },           //31
                    {354,  new PartnerChannelData {Index=32,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="torqueFeedbackM3" } },            //32
                    {356,  new PartnerChannelData {Index=33,  Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="modulationIndexM3" } },           //33
                    {358,  new PartnerChannelData {Index=34,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="idcommandM3" } },                 //34
                    {359,  new PartnerChannelData {Index=35,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="iqcommandM3" } },                 //35
                    {403,  new PartnerChannelData {Index=36,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="moduleATemperatureM4" } },        //36
                    {404,  new PartnerChannelData {Index=37,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="moduleBTemperatureM4" } },        //37
                    {405,  new PartnerChannelData {Index=38,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="moduleCTemperatureM4" } },        //38
                    {413,  new PartnerChannelData {Index=39,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="motorTemperatureM4" } },          //39
                    {419,  new PartnerChannelData {Index=40,  Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="lowWordInternalStatesM4" } },     //40
                    {420,  new PartnerChannelData {Index=41,  Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="highWordInternalStatesM4" } },    //41
                    {421,  new PartnerChannelData {Index=42,  Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="POSTWordFaultCodesM4" } },        //42
                    {422,  new PartnerChannelData {Index=43,  Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="RUNWordFaultCodesM4" } },         //43 
                    {438,  new PartnerChannelData {Index=44,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="motorSpeedFeedbackM4" } },        //44
                    {439,  new PartnerChannelData {Index=45,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="electricalOutputFrequencyM4" } }, //45
                    {441,  new PartnerChannelData {Index=46,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseACurrentM4" } },             //46
                    {442,  new PartnerChannelData {Index=47,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseBCurrentM4" } },             //47
                    {443,  new PartnerChannelData {Index=48,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseCCurrentM4" } },             //48
                    {444,  new PartnerChannelData {Index=49,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="DCBusCurrentM4" } },              //49
                    {445,  new PartnerChannelData {Index=50,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="DCBusVoltageM4" } },              //50
                    {446,  new PartnerChannelData {Index=51,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="outputVoltageM4" } },             //51
                    {447,  new PartnerChannelData {Index=52,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseABVoltageM4" } },            //52 
                    {448,  new PartnerChannelData {Index=53,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseBCVoltageM4" } },            //53
                    {449,  new PartnerChannelData {Index=54,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="fluxCommandM4" } },               //54
                    {450,  new PartnerChannelData {Index=55,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="fluxFeedbackM4" } },              //55
                    {451,  new PartnerChannelData {Index=56,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="idFeedbackM4" } },                //56
                    {452,  new PartnerChannelData {Index=57,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="iqFeedbackM4" } },                //57
                    {453,  new PartnerChannelData {Index=58,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="commandedTorqueM4" } },           //58 
                    {454,  new PartnerChannelData {Index=59,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="torqueFeedbackM4" } },            //59
                    {456,  new PartnerChannelData {Index=60,  Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="modulationIndexM4" } },           //60
                    {458,  new PartnerChannelData {Index=61,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="idcommandM4" } },                 //61 
                    {459,  new PartnerChannelData {Index=62,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="iqcommandM4" } },                 //62
                    {703,  new PartnerChannelData {Index=63,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="moduleATemperatureM7" } },        //63
                    {704,  new PartnerChannelData {Index=64,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="moduleBTemperatureM7" } },        //64
                    {705,  new PartnerChannelData {Index=65,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="moduleCTemperatureM7" } },        //65
                    {713,  new PartnerChannelData {Index=66,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="motorTemperatureM7" } },          //66
                    {719,  new PartnerChannelData {Index=67,  Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="lowWordInternalStatesM7" } },     //67 
                    {720,  new PartnerChannelData {Index=68,  Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="highWordInternalStatesM7" } },    //68
                    {721,  new PartnerChannelData {Index=69,  Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="POSTWordFaultCodesM7" } },        //69
                    {722,  new PartnerChannelData {Index=70,  Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="RUNWordFaultCodesM7" } },         //70
                    {738,  new PartnerChannelData {Index=71,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="motorSpeedFeedbackM7" } },        //71
                    {739,  new PartnerChannelData {Index=72,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="electricalOutputFrequencyM7" } }, //72 
                    {741,  new PartnerChannelData {Index=73,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseACurrentM7" } },             //73  
                    {742,  new PartnerChannelData {Index=74,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseBCurrentM7" } },             //74
                    {743,  new PartnerChannelData {Index=75,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseCCurrentM7" } },             //75
                    {744,  new PartnerChannelData {Index=76,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="DCBusCurrentM7" } },              //76
                    {745,  new PartnerChannelData {Index=77,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="DCBusVoltageM7" } },              //77
                    {746,  new PartnerChannelData {Index=78,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="outputVoltageM7" } },             //78 
                    {747,  new PartnerChannelData {Index=79,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseABVoltageM7" } },            //79
                    {748,  new PartnerChannelData {Index=80,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseBCVoltageM7" } },            //80 
                    {749,  new PartnerChannelData {Index=81,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="fluxCommandM7" } },               //81 
                    {750,  new PartnerChannelData {Index=82,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="fluxFeedbackM7" } },              //82 
                    {751,  new PartnerChannelData {Index=83,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="idFeedbackM7" } },                //83
                    {752,  new PartnerChannelData {Index=84,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="iqFeedbackM7" } },                //84 
                    {753,  new PartnerChannelData {Index=85,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="commandedTorqueM7" } },           //85 
                    {754,  new PartnerChannelData {Index=86,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="torqueFeedbackM7" } },            //86
                    {756,  new PartnerChannelData {Index=87,  Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="modulationIndexM7" } },           //87
                    {758,  new PartnerChannelData {Index=88,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="idcommandM7" } },                 //88
                    {759,  new PartnerChannelData {Index=89,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="iqcommandM7" } },                 //89 
                    {803,  new PartnerChannelData {Index=90,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="moduleATemperatureM8" } },        //90 
                    {804,  new PartnerChannelData {Index=91,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="moduleBTemperatureM8" } },        //91
                    {805,  new PartnerChannelData {Index=92,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="moduleCTemperatureM8" } },        //92 
                    {813,  new PartnerChannelData {Index=93,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="motorTemperatureM8" } },          //93
                    {819,  new PartnerChannelData {Index=94,  Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="lowWordInternalStatesM8" } },     //94
                    {820,  new PartnerChannelData {Index=95,  Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="highWordInternalStatesM8" } },    //95
                    {821,  new PartnerChannelData {Index=96,  Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="POSTWordFaultCodesM8" } },        //96
                    {822,  new PartnerChannelData {Index=97,  Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="RUNWordFaultCodesM8" } },         //97
                    {838,  new PartnerChannelData {Index=98,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="motorSpeedFeedbackM8" } },        //98
                    {839,  new PartnerChannelData {Index=99,  Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="electricalOutputFrequencyM8" } }, //99 
                    {841,  new PartnerChannelData {Index=100, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseACurrentM8" } },             //100  
                    {842,  new PartnerChannelData {Index=101, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseBCurrentM8" } },             //101
                    {843,  new PartnerChannelData {Index=102, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseCCurrentM8" } },             //102    
                    {844,  new PartnerChannelData {Index=103, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="DCBusCurrentM8" } },              //103
                    {845,  new PartnerChannelData {Index=104, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="DCBusVoltageM8" } },              //104 
                    {846,  new PartnerChannelData {Index=105, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="outputVoltageM8" } },             //105 
                    {847,  new PartnerChannelData {Index=106, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseABVoltageM8" } },            //106  
                    {848,  new PartnerChannelData {Index=107, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseBCVoltageM8" } },            //107 
                    {849,  new PartnerChannelData {Index=108, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="fluxCommandM8" } },               //108
                    {850,  new PartnerChannelData {Index=109, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="fluxFeedbackM8" } },              //109
                    {851,  new PartnerChannelData {Index=110, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="idFeedbackM8" } },                //110 
                    {852,  new PartnerChannelData {Index=111, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="iqFeedbackM8" } },                //111
                    {853,  new PartnerChannelData {Index=112, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="commandedTorqueM8" } },           //112  
                    {854,  new PartnerChannelData {Index=113, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="torqueFeedbackM8" } },            //113  
                    {856,  new PartnerChannelData {Index=114, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="modulationIndexM8" } },           //114 
                    {858,  new PartnerChannelData {Index=115, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="idcommandM8" } },                 //115
                    {859,  new PartnerChannelData {Index=116, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="iqcommandM8" } },                 //116
                    {38,   new PartnerChannelData {Index=117, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="dyno2SpeedCommandEcho" } },       //117 Dyno #2 speed command echo
                    {101,  new PartnerChannelData {Index=118, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="torqueCommandM1" } },             //118
                    {201,  new PartnerChannelData {Index=119, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="torqueCommandM2" } },             //119
                    {501,  new PartnerChannelData {Index=120, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="torqueCommandM5" } },             //120
                    {601,  new PartnerChannelData {Index=121, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="torqueCommandM6" } },             //121
                    {72,   new PartnerChannelData {Index=122, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="Pparameter" } },                  //122 Dyno #2
                    {73,   new PartnerChannelData {Index=123, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="Iparameter" } },                  //123 Dyno #2
                    {74,   new PartnerChannelData {Index=124, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="Dparameter" } },                  //124 Dyno #2
                    {103,  new PartnerChannelData {Index=125, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="moduleATemperatureM1" } },        //125 
                    {104,  new PartnerChannelData {Index=126, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="moduleBTemperatureM1" } },        //126
                    {105,  new PartnerChannelData {Index=127, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="moduleCTemperatureM1" } },        //127
                    {113,  new PartnerChannelData {Index=128, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="motorTemperatureM1" } },          //128
                    {119,  new PartnerChannelData {Index=129, Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="lowWordInternalStatesM1" } },     //129
                    {120,  new PartnerChannelData {Index=130, Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="highWordInternalStatesM1" } },    //130
                    {121,  new PartnerChannelData {Index=131, Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="POSTWordFaultCodesM1" } },        //131
                    {122,  new PartnerChannelData {Index=132, Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="RUNWordFaultCodesM1" } },         //132
                    {138,  new PartnerChannelData {Index=133, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="motorSpeedFeedbackM1" } },        //133
                    {139,  new PartnerChannelData {Index=134, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="electricalOutputFrequencyM1" } }, //134
                    {141,  new PartnerChannelData {Index=135, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseACurrentM1" } },             //135  
                    {142,  new PartnerChannelData {Index=136, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseBCurrentM1" } },             //136
                    {143,  new PartnerChannelData {Index=137, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseCCurrentM1" } },             //137 
                    {144,  new PartnerChannelData {Index=138, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="DCBusCurrentM1" } },              //138
                    {145,  new PartnerChannelData {Index=139, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="DCBusVoltageM1" } },              //139
                    {146,  new PartnerChannelData {Index=140, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="outputVoltageM1" } },             //140
                    {147,  new PartnerChannelData {Index=141, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseABVoltageM1" } },            //141
                    {148,  new PartnerChannelData {Index=142, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseBCVoltageM1" } },            //142
                    {149,  new PartnerChannelData {Index=143, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="fluxCommandM1" } },               //143
                    {150,  new PartnerChannelData {Index=144, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="fluxFeedbackM1" } },              //144
                    {151,  new PartnerChannelData {Index=145, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="idFeedbackM1" } },                //145
                    {152,  new PartnerChannelData {Index=146, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="iqFeedbackM1" } },                //146
                    {153,  new PartnerChannelData {Index=147, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="commandedTorqueM1" } },           //147
                    {154,  new PartnerChannelData {Index=148, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="torqueFeedbackM1" } },            //148
                    {156,  new PartnerChannelData {Index=149, Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="modulationIndexM1" } },           //149
                    {158,  new PartnerChannelData {Index=150, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="idcommandM1" } },                 //150
                    {159,  new PartnerChannelData {Index=151, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="iqcommandM1" } },                 //151
                    {203,  new PartnerChannelData {Index=152, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="moduleATemperatureM2" } },        //152 
                    {204,  new PartnerChannelData {Index=153, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="moduleBTemperatureM2" } },        //153
                    {205,  new PartnerChannelData {Index=154, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="moduleCTemperatureM2" } },        //154
                    {213,  new PartnerChannelData {Index=155, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="motorTemperatureM2" } },          //155
                    {219,  new PartnerChannelData {Index=156, Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="lowWordInternalStatesM2" } },     //156
                    {220,  new PartnerChannelData {Index=157, Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="highWordInternalStatesM2" } },    //157
                    {221,  new PartnerChannelData {Index=158, Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="POSTWordFaultCodesM2" } },        //158
                    {222,  new PartnerChannelData {Index=159, Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="RUNWordFaultCodesM2" } },         //159
                    {238,  new PartnerChannelData {Index=160, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="motorSpeedFeedbackM2" } },        //160
                    {239,  new PartnerChannelData {Index=161, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="electricalOutputFrequencyM2" } }, //161
                    {241,  new PartnerChannelData {Index=162, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseACurrentM2" } },             //162
                    {242,  new PartnerChannelData {Index=163, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseBCurrentM2" } },             //163
                    {243,  new PartnerChannelData {Index=164, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseCCurrentM2" } },             //164    
                    {244,  new PartnerChannelData {Index=165, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="DCBusCurrentM2" } },              //165
                    {245,  new PartnerChannelData {Index=166, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="DCBusVoltageM2" } },              //166
                    {246,  new PartnerChannelData {Index=167, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="outputVoltageM2" } },             //167
                    {247,  new PartnerChannelData {Index=168, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseABVoltageM2" } },            //168
                    {248,  new PartnerChannelData {Index=169, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseBCVoltageM2" } },            //169
                    {249,  new PartnerChannelData {Index=170, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="fluxCommandM2" } },               //170
                    {250,  new PartnerChannelData {Index=171, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="fluxFeedbackM2" } },              //171
                    {251,  new PartnerChannelData {Index=172, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="idFeedbackM2" } },                //172
                    {252,  new PartnerChannelData {Index=173, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="iqFeedbackM2" } },                //173
                    {253,  new PartnerChannelData {Index=174, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="commandedTorqueM2" } },           //174
                    {254,  new PartnerChannelData {Index=175, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="torqueFeedbackM2" } },            //175
                    {256,  new PartnerChannelData {Index=176, Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="modulationIndexM2" } },           //176
                    {258,  new PartnerChannelData {Index=177, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="idcommandM2" } },                 //177
                    {259,  new PartnerChannelData {Index=178, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="iqcommandM2" } },                 //178
                    {503,  new PartnerChannelData {Index=179, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="moduleATemperatureM5" } },        //179
                    {504,  new PartnerChannelData {Index=180, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="moduleBTemperatureM5" } },        //180
                    {505,  new PartnerChannelData {Index=181, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="moduleCTemperatureM5" } },        //181 
                    {513,  new PartnerChannelData {Index=182, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="motorTemperatureM5" } },          //182
                    {519,  new PartnerChannelData {Index=183, Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="lowWordInternalStatesM5" } },     //183
                    {520,  new PartnerChannelData {Index=184, Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="highWordInternalStatesM5" } },    //184
                    {521,  new PartnerChannelData {Index=185, Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="POSTWordFaultCodesM5" } },        //185
                    {522,  new PartnerChannelData {Index=186, Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="RUNWordFaultCodesM5" } },         //186
                    {538,  new PartnerChannelData {Index=187, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="motorSpeedFeedbackM5" } },        //187
                    {539,  new PartnerChannelData {Index=188, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="electricalOutputFrequencyM5" } }, //188 
                    {541,  new PartnerChannelData {Index=189, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseACurrentM5" } },             //189  
                    {542,  new PartnerChannelData {Index=190, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseBCurrentM5" } },             //190
                    {543,  new PartnerChannelData {Index=191, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseCCurrentM5" } },             //191    
                    {544,  new PartnerChannelData {Index=192, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="DCBusCurrentM5" } },              //192
                    {545,  new PartnerChannelData {Index=193, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="DCBusVoltageM5" } },              //193 
                    {546,  new PartnerChannelData {Index=194, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="outputVoltageM5" } },             //194 
                    {547,  new PartnerChannelData {Index=195, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseABVoltageM5" } },            //195  
                    {548,  new PartnerChannelData {Index=196, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseBCVoltageM5" } },            //196 
                    {549,  new PartnerChannelData {Index=197, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="fluxCommandM5" } },               //197
                    {550,  new PartnerChannelData {Index=198, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="fluxFeedbackM5" } },              //198
                    {551,  new PartnerChannelData {Index=199, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="idFeedbackM5" } },                //199 
                    {552,  new PartnerChannelData {Index=200, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="iqFeedbackM5" } },                //200
                    {553,  new PartnerChannelData {Index=201, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="commandedTorqueM5" } },           //201  
                    {554,  new PartnerChannelData {Index=202, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="torqueFeedbackM5" } },            //202  
                    {556,  new PartnerChannelData {Index=203, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="modulationIndexM5" } },           //203 
                    {558,  new PartnerChannelData {Index=204, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="idcommandM5" } },                 //204
                    {559,  new PartnerChannelData {Index=205, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="iqcommandM5" } },                 //205
                    {603,  new PartnerChannelData {Index=206, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="moduleATemperatureM6" } },        //206 
                    {604,  new PartnerChannelData {Index=207, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="moduleBTemperatureM6" } },        //207
                    {605,  new PartnerChannelData {Index=208, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="moduleCTemperatureM6" } },        //208 
                    {613,  new PartnerChannelData {Index=209, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="motorTemperatureM6" } },          //209
                    {619,  new PartnerChannelData {Index=210, Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="lowWordInternalStatesM6" } },     //210
                    {620,  new PartnerChannelData {Index=211, Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="highWordInternalStatesM6" } },    //211
                    {621,  new PartnerChannelData {Index=212, Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="POSTWordFaultCodesM6" } },        //212
                    {622,  new PartnerChannelData {Index=213, Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="RUNWordFaultCodesM6" } },         //213
                    {638,  new PartnerChannelData {Index=214, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="motorSpeedFeedbackM6" } },        //214
                    {639,  new PartnerChannelData {Index=215, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="electricalOutputFrequencyM6" } }, //215 
                    {641,  new PartnerChannelData {Index=216, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseACurrentM6" } },             //216  
                    {642,  new PartnerChannelData {Index=217, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseBCurrentM6" } },             //217
                    {643,  new PartnerChannelData {Index=218, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseCCurrentM6" } },             //218    
                    {644,  new PartnerChannelData {Index=219, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="DCBusCurrentM6" } },              //219
                    {645,  new PartnerChannelData {Index=220, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="DCBusVoltageM6" } },              //220 
                    {646,  new PartnerChannelData {Index=221, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="outputVoltageM6" } },             //221 
                    {647,  new PartnerChannelData {Index=222, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseABVoltageM6" } },            //222  
                    {648,  new PartnerChannelData {Index=223, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="phaseBCVoltageM6" } },            //223 
                    {649,  new PartnerChannelData {Index=224, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="fluxCommandM6" } },               //224
                    {650,  new PartnerChannelData {Index=225, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="fluxFeedbackM6" } },              //225
                    {651,  new PartnerChannelData {Index=226, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="idFeedbackM6" } },                //226 
                    {652,  new PartnerChannelData {Index=227, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="iqFeedbackM6" } },                //227
                    {653,  new PartnerChannelData {Index=228, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="commandedTorqueM6" } },           //228  
                    {654,  new PartnerChannelData {Index=229, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="torqueFeedbackM6" } },            //239  
                    {656,  new PartnerChannelData {Index=230, Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="modulationIndexM6" } },           //230 
                    {658,  new PartnerChannelData {Index=231, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="idcommandM6" } },                 //231
                    {659,  new PartnerChannelData {Index=232, Type=PartnerChannelData.DataType.FLOAT,  Data=new byte[4]  ,DataByteSize=4, Name="iqcommandM6" } },                 //232

                    {1,    new PartnerChannelData {Index=233, Type=PartnerChannelData.DataType.FLOAT, Data=new byte[4]  ,DataByteSize=4, Name="Spare1" } },                      //233
                    {2,    new PartnerChannelData {Index=234, Type=PartnerChannelData.DataType.FLOAT, Data=new byte[4]  ,DataByteSize=4, Name="Spare2" } },                      //234
                    {3,    new PartnerChannelData {Index=235, Type=PartnerChannelData.DataType.FLOAT, Data=new byte[4]  ,DataByteSize=4, Name="Spare3" } },                      //235
                    {4,    new PartnerChannelData {Index=236, Type=PartnerChannelData.DataType.FLOAT, Data=new byte[4]  ,DataByteSize=4, Name="Spare4" } },                      //236

                    {733,  new PartnerChannelData {Index=237, Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="GroundFaultStatus" } },           //237 
                    {91,   new PartnerChannelData {Index=238, Type=PartnerChannelData.DataType.UINT32, Data=new byte[4]  ,DataByteSize=4, Name="masterFault" } },                 //238
        };




        int byteSize = 0;
        byte[] packetBytes;

        public int PacketByteSize
        {
            get { return byteSize; }
        }

        //*******************************************************
        public PartnerPacketMaker()
        //*******************************************************
        {
            log = LogWriter.Instance;

            // Count the amount of space needed to hold the out bound data
            byteSize = 0;
            foreach (var entry in PartnerPacketStructure)
            {
                // do something with entry.Value or entry.Key
                byteSize += entry.Value.DataByteSize;
            }
            //Populate the dictionary 

            // Allocate memory to hold the packet to be sent based on the space required by: PartnerPacketStructure
            packetBytes = new byte[byteSize];
            log.WriteToLogNow("Space allocated for outbound packet buffer: " + byteSize.ToString() );

        }

        //        //*******************************************************
        //
        // *** NOTE *** NOTE *** NOTE *** NOTE *** NOTE *** NOTE *** NOTE ***
        //
        // The below version of the method, "PartnerPacketToBytes()" is test code
        // used in the verification of telemetry packets sent to the Partner system.
        // 
        // The trick that it pulls is: replacing data as sent to the Partner system
        // with the channel number of each individual data item. The data is encoded
        // as 32 bit float or 32 bit int depending on the configured item.
        //
        //        public byte[] PartnerPacketToBytes()
        //        {
        //            int index = 0;
        //            //int valueCount = 0;
        //            UInt32 iValue;
        //            float fValue;
        //
        //            //*******************************************************
        //            foreach (var entry in PartnerPacketStructure)
        //            {
        //                if (entry.Value.Type == PartnerChannelData.DataType.FLOAT)
        //                {
        //                    fValue = entry.Key;
        //                    float[] data = new float[1];
        //                    data[0] = fValue;
        //                    Buffer.BlockCopy((Array)data, // source
        //                                     0,           // offset into source
        //                                     packetBytes, // destination
        //                                     index,       // offset into destination
        //                                     4);          // number of bytes to copy
        //                }
        //                else if (entry.Value.Type == PartnerChannelData.DataType.UINT32)
        //                {
        //                    iValue = (UInt32)entry.Key;
        //                    Int32[] data = new Int32[1];
        //                    data[0] = (Int32)iValue;
        //                    Buffer.BlockCopy((Array)data, // source
        //                                     0,           // offset into source
        //                                     packetBytes, // destination
        //                                     index,       // offset into destination
        //                                     4);          // number of bytes to copy
        //                }
        //
        //                // Now bump the destination index
        //                index += entry.Value.DataByteSize;
        //            }
        //            return packetBytes;
        //        }


        //*******************************************************
        public byte[] PartnerPacketToBytes()
        //*******************************************************
        {
            int masterFault = 0; // 0 == good
            int index = 0;
            int valueCount = 0;

            foreach (var entry in PartnerPacketStructure)
            {
                if (entry.Value.Name == "POSTWordFaultCodesM1")
                {
                    if (entry.Value.Data[0] > 0 || entry.Value.Data[1] > 0 || entry.Value.Data[2] > 0 || entry.Value.Data[3] > 0)
                    {
                        masterFault = 1; // bad
                    }
                }
                        
                if (entry.Value.Name == "RUNWordFaultCodesM1")
                {
                    if (entry.Value.Data[0] > 0 || entry.Value.Data[1] > 0 || entry.Value.Data[2] > 0 || entry.Value.Data[3] > 0)
                    {
                        masterFault = 1; // bad
                    }
                }


                if (entry.Value.Name == "POSTWordFaultCodesM2")
                {
                    if (entry.Value.Data[0] > 0 || entry.Value.Data[1] > 0 || entry.Value.Data[2] > 0 || entry.Value.Data[3] > 0)
                    {
                        masterFault = 1; // bad
                    }
                }
                if (entry.Value.Name == "RUNWordFaultCodesM2")
                {
                    if (entry.Value.Data[0] > 0 || entry.Value.Data[1] > 0 || entry.Value.Data[2] > 0 || entry.Value.Data[3] > 0)
                    {
                        masterFault = 1; // bad
                    }
                }


                if (entry.Value.Name == "POSTWordFaultCodesM3")
                {
                    if (entry.Value.Data[0] > 0 || entry.Value.Data[1] > 0 || entry.Value.Data[2] > 0 || entry.Value.Data[3] > 0)
                    {
                        masterFault = 1; // bad
                    }
                }
                if (entry.Value.Name == "RUNWordFaultCodesM3")
                {
                    if (entry.Value.Data[0] > 0 || entry.Value.Data[1] > 0 || entry.Value.Data[2] > 0 || entry.Value.Data[3] > 0)
                    {
                        masterFault = 1; // bad
                    }
                }


                if (entry.Value.Name == "POSTWordFaultCodesM4")
                {
                    if (entry.Value.Data[0] > 0 || entry.Value.Data[1] > 0 || entry.Value.Data[2] > 0 || entry.Value.Data[3] > 0)
                    {
                        masterFault = 1; // bad
                    }
                }
                if (entry.Value.Name == "RUNWordFaultCodesM4")
                {
                    if (entry.Value.Data[0] > 0 || entry.Value.Data[1] > 0 || entry.Value.Data[2] > 0 || entry.Value.Data[3] > 0)
                    {
                        masterFault = 1; // bad
                    }
                }


                if (entry.Value.Name == "POSTWordFaultCodesM5")
                {
                    if (entry.Value.Data[0] > 0 || entry.Value.Data[1] > 0 || entry.Value.Data[2] > 0 || entry.Value.Data[3] > 0)
                    {
                        masterFault = 1; // bad
                    }
                }
                if (entry.Value.Name == "RUNWordFaultCodesM5")
                {
                    if (entry.Value.Data[0] > 0 || entry.Value.Data[1] > 0 || entry.Value.Data[2] > 0 || entry.Value.Data[3] > 0)
                    {
                        masterFault = 1; // bad
                    }
                }


                if (entry.Value.Name == "POSTWordFaultCodesM6")
                {
                    if (entry.Value.Data[0] > 0 || entry.Value.Data[1] > 0 || entry.Value.Data[2] > 0 || entry.Value.Data[3] > 0)
                    {
                        masterFault = 1; // bad
                    }
                }
                if (entry.Value.Name == "RUNWordFaultCodesM6")
                {
                    if (entry.Value.Data[0] > 0 || entry.Value.Data[1] > 0 || entry.Value.Data[2] > 0 || entry.Value.Data[3] > 0)
                    {
                        masterFault = 1; // bad
                    }
                }


                if (entry.Value.Name == "POSTWordFaultCodesM7")
                {
                    if (entry.Value.Data[0] > 0 || entry.Value.Data[1] > 0 || entry.Value.Data[2] > 0 || entry.Value.Data[3] > 0)
                    {
                        masterFault = 1; // bad
                    }
                }
                if (entry.Value.Name == "RUNWordFaultCodesM7")
                {
                    if (entry.Value.Data[0] > 0 || entry.Value.Data[1] > 0 || entry.Value.Data[2] > 0 || entry.Value.Data[3] > 0)
                    {
                        masterFault = 1; // bad
                    }
                }


                if (entry.Value.Name == "POSTWordFaultCodesM8")
                {
                    if (entry.Value.Data[0] > 0 || entry.Value.Data[1] > 0 || entry.Value.Data[2] > 0 || entry.Value.Data[3] > 0)
                    {
                        masterFault = 1; // bad
                    }
                }
                if (entry.Value.Name == "RUNWordFaultCodesM8")
                {
                    if (entry.Value.Data[0] > 0 || entry.Value.Data[1] > 0 || entry.Value.Data[2] > 0 || entry.Value.Data[3] > 0)
                    {
                        masterFault = 1; // bad
                    }
                }
            }

            if (BM4Status == false)
            {
                masterFault = 1; // bad
            }

            if (GFM022Status == false)
            {
                masterFault = 1; // bad
            }

            if (GFM025Status == false)
            {
                masterFault = 1; // bad
            }

            if (ULI_Fault_Status == false)
            {
                masterFault = 1; // bad
            }
            // Work through the PartnerPacketStructure for the 'masterFault' entry.
            // Then we will set the value based on masterFault flag as derived above.
            //
            foreach (var entry in PartnerPacketStructure)
            {
                if (entry.Value.Name == "masterFault")
                {
                    if (masterFault > 0) // bad
                    {
//                        log.WriteToLog("masterFault: >0 " + entry.Value.Name);
                        entry.Value.Data[0] = 1;
                    }
                    else // good
                    {
//                        log.WriteToLog("masterFault: <= 0 " + entry.Value.Name);
                        entry.Value.Data[0] = 0;
                    }
                }

                if (entry.Value.Name == "GroundFaultStatus")
                {
                    if (GFM022Status == false)  // We have a ground fault
                        entry.Value.Data[0] = 1;
                    else
                        entry.Value.Data[0] = 0;
                }

            }

            // Do the dirty work of copying the data into the outbound block.
            foreach (var entry in PartnerPacketStructure)
            {
                // Copy values out of each "entry" in the PartnerPacketStructure
                Buffer.BlockCopy(
                    (byte[])entry.Value.Data, // Source of data
                    0,                        // offset into source
                    packetBytes,              // Destination for copy operation
                    index,                    // Index into the destination
                    entry.Value.DataByteSize  // Number of bytes to copy
                    );
                // Now bump the destination index
                index += entry.Value.DataByteSize;
                valueCount += 1;
            }

            return packetBytes;
        }

        //*******************************************************
        public void PartnerPacketPrintStructure()
        //*******************************************************
        {
            foreach (var entry in PartnerPacketStructure)
            {
                if (entry.Value.Type == PartnerChannelData.DataType.FLOAT)
                {
                    Console.WriteLine("Index: " + entry.Value.Index.ToString() + " " + entry.Value.Name + " Type:Float    Value: " + entry.Value.Data );
                }
                else if (entry.Value.Type == PartnerChannelData.DataType.UINT32)
                {
                    Console.WriteLine("Index: " + entry.Value.Index.ToString() + " " + entry.Value.Name + " Type:UINT32   Value: " + entry.Value.Data );
                }
            }

        }//end func

        //*******************************************************
        public bool TryAddValue(int channelNumber, TransmitDataPayload payload)
        //*******************************************************
        {
            bool result = false; // bad

            //BM4 feedback  M8DIN5 0 fault
            if (payload.ChannelNumber == 833 && payload.Type == TransmitDataPayload.DataType.BOOL)
            {
                BM4Status = (bool)payload.Data;
                result = true; // good
            }

            //GFM022
            if (payload.ChannelNumber == 733 && payload.Type == TransmitDataPayload.DataType.BOOL)
            {
                GFM022Status = (bool)payload.Data;
                result = true; // good
            }

            //GFM025
            if (payload.ChannelNumber == 433 && payload.Type == TransmitDataPayload.DataType.BOOL)
            {
                GFM025Status = (bool)payload.Data;
                result = true; // good
            }

            //ULI
            if (payload.ChannelNumber == 86 && payload.Type == TransmitDataPayload.DataType.BOOL)
            {
                ULI_Fault_Status = (bool)payload.Data;
                result = true; // good
            }

            try
            {
                if (this.PartnerPacketStructure.ContainsKey(channelNumber))
                {
                    switch (payload.Type)
                    {
                        case TransmitDataPayload.DataType.UINT32:
                            UInt32[] data = new UInt32[1];
                            data[0] = (UInt32)payload.Data;
                            Buffer.BlockCopy((Array)data, 0, (Array)this.PartnerPacketStructure[channelNumber].Data, 0, sizeof(UInt32));
                            result = true; // good
                            break;

                        case TransmitDataPayload.DataType.UINT16:
                            UInt16[] data1 = new UInt16[1];
                            data1[0] = (UInt16)payload.Data;
                            Buffer.BlockCopy((Array)data1, 0, (Array)this.PartnerPacketStructure[channelNumber].Data, 0, sizeof(UInt16));
                            result = true; // good
                            break;

                        case TransmitDataPayload.DataType.INT16:
                            UInt16[] data2 = new UInt16[1];
                            data2[0] = (UInt16)payload.Data;
                            Buffer.BlockCopy((Array)data2, 0, (Array)this.PartnerPacketStructure[channelNumber].Data, 0, sizeof(Int16));
                            result = true; // good
                            break;

                        case TransmitDataPayload.DataType.FLOAT:
                            float[] data3 = new float[1];
                            data3[0] = (float)payload.Data;
                            Buffer.BlockCopy((Array)data3, 0, (Array)this.PartnerPacketStructure[channelNumber].Data, 0, sizeof(float));
                            result = true; // good
                            break;

                        case TransmitDataPayload.DataType.INT32:
                            Int32[] data4 = new Int32[1];
                            data4[0] = (Int32)payload.Data;
                            Buffer.BlockCopy((Array)data4, 0, (Array)this.PartnerPacketStructure[channelNumber].Data, 0, sizeof(Int32));
                            result = true; // good
                            break;

                        case TransmitDataPayload.DataType.UINT8:
                            char[] data5 = new char[1];
                            data5[0] = (char)payload.Data;
                            Buffer.BlockCopy((Array)data5, 0, (Array)this.PartnerPacketStructure[channelNumber].Data, 0, sizeof(char));
                            result = true; // good
                            break;

                        case TransmitDataPayload.DataType.INT8:
                            char[] data6 = new char[1];
                            data6[0] = (char)payload.Data;
                            Buffer.BlockCopy((Array)data6, 0, (Array)this.PartnerPacketStructure[channelNumber].Data, 0, sizeof(char));
                            result = true; // good
                            break;

                        case TransmitDataPayload.DataType.BOOL: //UInt32[]
                            UInt32[] data7 = new UInt32[1];
                            if (payload.Equals(true))
                                data7[0] = 1;
                            else
                                data7[0] = 0;
                            Buffer.BlockCopy((Array)data7, 0,  // Source, Offset
                                             (Array)this.PartnerPacketStructure[channelNumber].Data, 0, sizeof(UInt32)); // Destination, Offset, Number of bytes
                            result = true; // good
                            break;

                        default: // Not an error.
                            break;
                    }//end switch
                } // if (this.PartnerPacketStructure.ContainsKey(channelNumber))
            }
            catch (Exception ex)
            {
                log.WriteToLogNow("EXCEPTION thrown in: public bool TryAddValue(int channelNumber, TransmitDataPayload payload)");
                log.WriteToLogNow("channelNumber = " + channelNumber.ToString());
                log.WriteToLogNow("payload = " + payload.ToString());
                log.WriteToLogNow("exception: " + ex.ToString());
                result = false; // bad
            }

            return result;
        }//end func

        //*******************************************************
        public bool TryAddValue(int channelNumber, TransmitDataPayload payload, int foo)
        //*******************************************************
        {
            bool result = false; // bad

            //BM4 feedback  M8DIN5 0 fault
            if (payload.ChannelNumber == 833 && payload.Type == TransmitDataPayload.DataType.BOOL)
            {
                BM4Status = (bool)payload.Data;
                result = true; // good
            }

            //GFM022
            if (payload.ChannelNumber == 733 && payload.Type == TransmitDataPayload.DataType.BOOL)
            {
                GFM022Status = (bool)payload.Data;
                result = true; // good
            }

            //GFM025
            if (payload.ChannelNumber == 433 && payload.Type == TransmitDataPayload.DataType.BOOL)
            {
                GFM025Status = (bool)payload.Data;
                result = true; // good
            }

            //ULI
            if (payload.ChannelNumber == 86 && payload.Type == TransmitDataPayload.DataType.BOOL)
            {
                ULI_Fault_Status = (bool)payload.Data;
                result = true; // good
            }

            try
            {
                if (this.PartnerPacketStructure.ContainsKey(channelNumber))
                {
                    //                    keyFound = true;
                    switch (payload.Type)
                    {
                        case TransmitDataPayload.DataType.UINT32:
                            UInt32[] data = new UInt32[1];
                            data[0] = (UInt32)payload.Data;
                            Buffer.BlockCopy((Array)data, 0, (Array)this.PartnerPacketStructure[channelNumber].Data, 0, sizeof(UInt32));
                            result = true; // good
                            break;

                        case TransmitDataPayload.DataType.UINT16:
                            UInt16[] data1 = new UInt16[1];
                            data1[0] = (UInt16)payload.Data;
                            Buffer.BlockCopy((Array)data1, 0, (Array)this.PartnerPacketStructure[channelNumber].Data, 0, sizeof(UInt16));
                            result = true; // good
                            break;

                        case TransmitDataPayload.DataType.INT16:
                            UInt16[] data2 = new UInt16[1];
                            data2[0] = (UInt16)payload.Data;
                            Buffer.BlockCopy((Array)data2, 0, (Array)this.PartnerPacketStructure[channelNumber].Data, 0, sizeof(Int16));
                            result = true; // good
                            break;

                        case TransmitDataPayload.DataType.FLOAT:
                            float[] data3 = new float[1];
                            data3[0] = (float)payload.Data;
                            Buffer.BlockCopy((Array)data3, 0, (Array)this.PartnerPacketStructure[channelNumber].Data, 0, sizeof(float));
                            result = true; // good
                            break;

                        case TransmitDataPayload.DataType.INT32:
                            Int32[] data4 = new Int32[1];
                            data4[0] = (Int32)payload.Data;
                            Buffer.BlockCopy((Array)data4, 0, (Array)this.PartnerPacketStructure[channelNumber].Data, 0, sizeof(Int32));
                            result = true; // good
                            break;

                        case TransmitDataPayload.DataType.UINT8:
                            char[] data5 = new char[1];
                            data5[0] = (char)payload.Data;
                            Buffer.BlockCopy((Array)data5, 0, (Array)this.PartnerPacketStructure[channelNumber].Data, 0, sizeof(char));
                            result = true; // good
                            break;

                        case TransmitDataPayload.DataType.INT8:
                            char[] data6 = new char[1];
                            data6[0] = (char)payload.Data;
                            Buffer.BlockCopy((Array)data6, 0, (Array)this.PartnerPacketStructure[channelNumber].Data, 0, sizeof(char));
                            result = true; // good
                            break;

                        case TransmitDataPayload.DataType.BOOL: //UInt32[]
                            UInt32[] data7 = new UInt32[1];
                            if (payload.Equals(true))
                                data7[0] = 1;
                            else
                                data7[0] = 0;
                            Buffer.BlockCopy((Array)data7, 0,  // Source, Offset
                                             (Array)this.PartnerPacketStructure[channelNumber].Data, 0, sizeof(UInt32)); // Destination, Offset, Number of bytes
                            result = true; // good
                            break;

                        default: // Not an error.
                            break;
                    }//end switch
                } // if (this.PartnerPacketStructure.ContainsKey(channelNumber))
            }
            catch (Exception ex)
            {
                log.WriteToLogNow("EXCEPTION thrown in: public bool TryAddValue(int channelNumber, TransmitDataPayload payload)");
                log.WriteToLogNow("channelNumber = " + channelNumber.ToString());
                log.WriteToLogNow("payload = " + payload.ToString());
                log.WriteToLogNow("exception: " + ex.ToString());
                result = false; // bad
            }
            return result;
        }//end func

    }//end class

    //*******************************************************
    public class PartnerChannelData
    //*******************************************************
    {
        public enum DataType { UINT32 = 1, UINT16 = 2, INT16 = 3, FLOAT = 4, INT32 = 5, UINT8 = 6, INT8 = 7, BOOL = 8 };
        public DataType Type { get; set; }
        public int ChannelNumber { get; set; }
        /// <summary>
        /// this is the storage for a generic objec that has to the converted to a specific type such as float
        /// </summary>
        public byte[] Data { get; set; }
        public string Name { get; set; }
        public int DataByteSize { get; set; }
        public int Index { get; set; }

    }//end class

}
