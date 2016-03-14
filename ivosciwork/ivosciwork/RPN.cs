using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;

namespace ivosciwork
{
    public class RPN
    {
        public enum Mode { IX105NP, IX105, HX12, off };
        public enum Frequency { F1, F2, F3, F4 };
        private Mode currentMode = Mode.off;
        private Frequency currentFreq;
        private HashSet<Frequency> frequencySet = new HashSet<Frequency>();

        private bool running = false;
        private bool change = false;
        private bool stopPressed = false;
        public bool on = false;

        private bool changefreq = false;

        private Vector4D azimut = 0;
        private double epsilon = 0;

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

            internal void set(Frequency f, double v)
            {
                switch (f)
                {
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
                on = false;
                currentState.isActive = false;
                stateChanged(currentState);
            }
            change = true;
        }

        public void changeEpsilon(double e)
        {
            if (currentMode != Mode.IX105NP)
            {
                epsilon = e;
                currentState.currentDirection.epsilon = e;
                directionChanged(currentState);
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

        public SortedSet<Frequency> getFreqSet()
        {
            
            SortedSet<Frequency> toReturn = new SortedSet<Frequency>();
            lock(frequencySet)
            {
                foreach (Frequency f in frequencySet)
                    toReturn.Add(f);
                return toReturn;
            }
            
            
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

        public void setFrequencies(params Frequency[] f)
        {
            changefreq = true;
            lock (frequencySet)
            {
                frequencySet.Clear();
                foreach (Frequency fr in f)
                {
                    frequencySet.Add(fr);
                }
            }
            
        }

        public void turnOn()
        {
            running = true;
            on = true;
            change = true;
            currentState.isActive = true;
            stateChanged(currentState);
        }
        public void turnOff()
        {
            running = false;
            on = false;
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

        private CompleteRPNState currentState = new CompleteRPNState(false, Frequency.F1, new ScanningDirection(0, 0));

        public struct CompleteRPNState {
            internal bool isActive;
            internal Frequency currentFrequency;
            internal ScanningDirection currentDirection;

            public CompleteRPNState(bool state, Frequency f, ScanningDirection d) {
                this.isActive = state;
                this.currentFrequency = f;
                this.currentDirection = d;
            }

        }

        public struct ScanningDirection {
            internal double azimut;
            internal double epsilon;

            public ScanningDirection(double azimut, double epsilon) {
                this.azimut = azimut;
                this.epsilon = epsilon;
            }
        }

        public delegate void RPNEvent(CompleteRPNState currentState);

        public event RPNEvent stateChanged;
        public event RPNEvent frequencyChanged;
        public event RPNEvent directionChanged;
        public event RPNEvent tick;

        private void turnOn(double stepX, double stepY, double X0, double Y0, int NX, int NY)
        {
            epsilon = Y0;
            int y = 1;
            Vector4D x = 1;
            azimut = X0;
            changefreq = false;
            double azimuttek;
            while ((running == true) & (change == false))
            {
                SortedSet<Frequency> currentSet = getFreqSet();
                foreach (Frequency f in currentSet)
                {
                    
                    var watch = Stopwatch.StartNew(); //it's for control precision of time measure

                    currentFreq = f;
                    currentState.currentFrequency = f;
                    frequencyChanged(currentState);

                    x.set(f, x.get(f) + 1);
                    azimut.set(f, azimut.get(f) + stepX);
                    currentState.currentDirection.azimut = azimut.get(f) + stepX;
                    azimuttek = currentState.currentDirection.azimut;
                    if (x.get(f) == NX + 1)
                    {
                        x.set(f, 1);
                        azimut.set(f, X0);
                        currentState.currentDirection.azimut = X0;
                        if (!stopPressed)
                        {
                            epsilon += stepY;
                            currentState.currentDirection.epsilon += stepY/ frequencySet.Count;
                            y++;
                            if (y == NY + 1)
                            {
                                y = 1;
                                epsilon = Y0;
                                currentState.currentDirection.epsilon = Y0;
                            }
                        }
                    }

                    

                    directionChanged(currentState);

                    System.Threading.Thread.Sleep(Constants.RPN_DELAY);

                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;
                    var delta = Math.Abs(elapsedMs - Constants.RPN_DELAY);
                    if (delta > Constants.RPN_DELAY * Constants.PRECISION)
                    {
                        Console.Beep();
                    }
                    if (changefreq) { break; }
                }

             }
        }

        internal Mode getCurrentMode()
        {
            Mode toRet = currentMode;
            return toRet;
        }

        internal Frequency getCurrentFreq()
        {
            Frequency toRet = currentFreq;
            return toRet;
        }
    }
}