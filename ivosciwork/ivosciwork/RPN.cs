using System;
using System.Collections.Generic;

namespace ivosciwork
{
    public class RPN
    {
        public enum Mode { IX105NP, IX105, HX12, off };
        private Mode currentMode;
        private bool running = false;
        private bool change = false;

        public void changeMode(Mode m)
        {
            
            currentMode = m;
            if (m == Mode.off)
            {
                running = false;
            }
            change = true;
        }

        private double azimut = 0;
        private double epsilon = 0;
        public enum Frequency { F1, F2, F3, F4 };
        private HashSet<Frequency> frequencySet = new HashSet<Frequency>();
        public int delay = 5;
        private bool stopPressed = false;

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

        public double getAzimut()
        {
            return azimut;
        }

        public HashSet<Frequency> getFreqSet()
        {
            return frequencySet;
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

        public void on()
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
                                eventLoop(1.0 / 3.0, 0, 0, 0.3, 315, 1);
                                break;
                            }
                        case Mode.IX105:
                            {
                                change = false;
                                setFrequencies(Frequency.F1, Frequency.F2, Frequency.F3, Frequency.F4);
                                eventLoop(1.0 / 3.0, 0, 0, epsilon, 315, 1);
                                break;
                            }
                        case Mode.HX12:
                            {
                                change = false;
                                setFrequencies(Frequency.F1, Frequency.F2, Frequency.F3, Frequency.F4);
                                eventLoop(1.0 / 3.0, 1.0 / 3.0, 0, epsilon, 36, 12);
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

        private void eventLoop(double stepX, double stepY, double X0, double Y0, int NX, int NY) {
            epsilon = Y0;
            int y = 1;
            int x = 1;
            azimut = X0;
            while ((running == true) & (change == false)) {
                azimut += stepX;
                x++; 
                if (x == NX+1) {
                    x = 1;
                    azimut = X0;
                    if (!stopPressed)
                    {
                        epsilon += stepY;
                        y++;
                        if (y == NY+1)
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
  

