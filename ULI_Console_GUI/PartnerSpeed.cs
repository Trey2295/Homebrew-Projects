using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeatGUI
{
    public static class PartnerSpeed
    {
        //*******************************************************
        public static float TestSpeedLimitsAndApply(float data, float SpeedMaxAbs, bool useNegativeSpeedOnly)
        //*******************************************************
        {
            float result = 0.0f;

                if (useNegativeSpeedOnly == false) //range 0 to speed max
                {
                    if (data >= 0.0)
                    {
                        if (data <= SpeedMaxAbs)
                        {
                            result = data;
                        }
                        else
                        {
                            result = SpeedMaxAbs;
                        }
                    }
                    else
                    {
                        result = 0.0f;
                    }
                }
                else //range from 0 to negative max speed
                {
                    if (data > 0.0)
                    {
                        result = 0.0f;
                    }
                    else
                    {
                        if (data >= -SpeedMaxAbs)
                        {
                            result = data;
                        }
                        else
                        {
                            result = -SpeedMaxAbs;
                        }
                    }
                }
            return result;
        }//end func
    }
}
