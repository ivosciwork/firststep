using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ivosciwork
{
    static class Constants
    {
        internal static readonly int RPN_DELAY = 1000; // time in milliseconds per one frequency 
        internal static readonly double PRECISION = 0.05; //precision of time measure

        internal static Color getFreqColor(RPN.Frequency f)
        {
            Color toRet = new Color();
            switch (f) {
                case RPN.Frequency.F1: toRet = Color.Red; break;
                case RPN.Frequency.F2: toRet = Color.Green; break;
                case RPN.Frequency.F3: toRet = Color.Blue; break;
                case RPN.Frequency.F4: toRet = Color.Yellow; break;
            }
            return toRet;
        }
    }
}
