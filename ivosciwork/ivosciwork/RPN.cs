using System;
using System.Collections.Generic;

namespace ivosciwork
{
    public class RPN
    {
        public enum Mode { IX105NP, IX105, HX12, off };
        public enum Frequency { F1, F2, F3, F4 };
        public int n;
        private Mode currentMode = Mode.off;
        private HashSet<Frequency> frequencySet = new HashSet<Frequency>();

        private bool running = false;
        private bool change = false;
        private bool stopPressed = false;

        private Vector4D azimut = 0;
        private double epsilon = 0;

        public int delay = 100;
        public int freqDelay = 1;

        public struct Vector4D
        {
            private double F1;
            private double F2;
            private double F3;
            private double F4;

            public static implicit operator Vector4D(double v)
            {
                return new Vector4D() { F1 = v, F2 = v, F3 = v, F4 = v };
            }

            internal void set( Frequency f, double v ) {
                switch (f) {
                    case Frequency.F1:
                        F1 = v; break;
                    case Frequency.F2:
                        F2 = v; break;
                    case Frequency.F3:
                        F3 = v; break;
                    case Frequency.F4:
                        F4 = v; break;
                }
            }

            internal double get(Frequency f)
            {
                switch (f)
                {
                    case Frequency.F1:
                        return F1;
                    case Frequency.F2:
                        return F2;
                    case Frequency.F3:
                        return F3;
                    case Frequency.F4:
                        return F4;
                }
                return Double.NaN;
            }
        }

        public void changeMode(Mode m)
        {

            currentMode = m;
            if (m == Mode.off)
            {
                running = false;
            }
            change = true;
        }

        public void changeEpsilon(double e)
        {
            if (currentMode != Mode.IX105NP)
            {
                epsilon = e;
            }
        }

        public double getEpsilon()
        {
            return epsilon;
        }

        public Vector4D getAzimut()
        {
            return azimut;
        }

        public HashSet<Frequency> getFreqSet()
        {
            HashSet<Frequency> toReturn = new HashSet<Frequency>();
            foreach (Frequency f in frequencySet)
                toReturn.Add(f);
            return toReturn;
        }

        public bool OnStopButtonState()
        {
            stopPressed = true;
            return stopPressed;
        }
        public bool OffStopButtonState()
        {
            stopPressed = false;
            return stopPressed;
        }

        public RPN()
        {
            setFrequencies(Frequency.F1, Frequency.F2, Frequency.F3, Frequency.F4);
        }

        private void setFrequencies(params Frequency[] f)
        {
            frequencySet.Clear();
            foreach (Frequency fr in f)
            {
                frequencySet.Add(fr);
            }
        }

        public void changeFrequency(params Frequency[] f)
        {
            setFrequencies(f);
        }

        public void turnOn()
        {
            running = true;
            change = true;
        }
        public void turnOff()
        {
            running = false;
        }

        public void eventLoop()
        {
            while (true)
            {
                if (running == true)
                {
                    switch (currentMode)
                    {
                        case Mode.IX105NP:
                            {
                                change = false;
                                setFrequencies(Frequency.F1, Frequency.F2, Frequency.F4);
                                turnOn(1.0 / 3.0, 0, 0, 0.3, 315, 1);
                                break;
                            }
                        case Mode.IX105:
                            {
                                change = false;
                                setFrequencies(Frequency.F1, Frequency.F2, Frequency.F3, Frequency.F4);
                                turnOn(1.0 / 3.0, 0, 0, epsilon, 315, 1);
                                break;
                            }
                        case Mode.HX12:
                            {
                                change = false;
                                setFrequencies(Frequency.F1, Frequency.F2, Frequency.F3, Frequency.F4);
                                turnOn(1.0 / 3.0, 1.0 / 3.0, 0, epsilon, 36, 12);
                                break;
                            }
                        case Mode.off:
                            {
                                running = false;
                                break;
                            }
                    }
                }
            }
        }

        private void turnOn(double stepX, double stepY, double X0, double Y0, int NX, int NY)
        {
            epsilon = Y0;
            int y = 1;
            int x = 1;
            n = 1;
            azimut = X0;
            while ((running == true) & (change == false))
            {
                foreach (Frequency f in frequencySet) {
                    azimut.set(f, azimut.get(f) + 1); 
                    if (n == 18) { n = 1; }
                    else { n++; }
                }
                x++;
                if (x == NX + 1)
                {
                    x = 1;
                    azimut = X0;
                    if (!stopPressed)
                    {
                        epsilon += stepY;
                        y++;
                        if (y == NY + 1)
                        {
                            y = 1;
                            epsilon = Y0;
                        }
                    }
                }
                System.Threading.Thread.Sleep(delay * frequencySet.Count);
            }
        }

        internal Mode getCurrentMode()
        {
            return currentMode;
        }
    }
}