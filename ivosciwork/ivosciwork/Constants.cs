﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ivosciwork
{
    static class Constants
    {
        internal static readonly int RPN_DELAY = 10000; // time in milliseconds per one frequency 
        internal static readonly double PRECISION = 0.05; //precision of time measure

        internal static Color getFreqColor(RPN.Frequency f)
        {
            Color toRet = new Color();
            switch (f) {
                case RPN.Frequency.F1: toRet = Color.Red; break;
                case RPN.Frequency.F2: toRet = Color.Blue; break;
                case RPN.Frequency.F3: toRet = Color.Yellow; break;
                case RPN.Frequency.F4: toRet = Color.GreenYellow; break;
            }
            return toRet;
        }
        internal static Image getFreqImage(RPN.Frequency f)
        {
           Image toRet = Properties.Resources.f1;
            switch (f)
            {
                case RPN.Frequency.F1: toRet = Properties.Resources.f1; break;
                case RPN.Frequency.F2: toRet = Properties.Resources.f2; break;
                case RPN.Frequency.F3: toRet = Properties.Resources.f3; break;
                case RPN.Frequency.F4: toRet = Properties.Resources.f4; break;
            }
            return toRet;
        }
    }
}
