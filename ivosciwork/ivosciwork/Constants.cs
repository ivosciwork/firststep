using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ivosciwork
{
    static class Constants
    {
        internal static int RPN_DELAY = 30000;//ms
        internal static int BEAM_DELAY = 1;
        internal static int DIAGRAMM_DELAY = 1;

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
