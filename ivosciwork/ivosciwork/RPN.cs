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
        private HashSet<Frequency> frequencySet = new HashSet<Frequency>();

        private bool change = false; //indicate that something was changed and turnOn() should be relaunched
        private bool stopPressed = false;

        public bool changefreq = false; //indicate the specific frequency settings

        private Vector4D azimut = 0;

        
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
            currentState.currentMode = m;
            modeChanged(currentState);
            switch (m)
            {
                case Mode.IX105NP:
                    {
                        setFrequencies(Frequency.F1, Frequency.F2, Frequency.F4);
                        break;
                    }
                case Mode.IX105:
                    {
                        setFrequencies(Frequency.F1, Frequency.F2, Frequency.F3, Frequency.F4);
                        break;
                    }
                case Mode.HX12:
                    {
                        setFrequencies(Frequency.F1, Frequency.F2, Frequency.F3, Frequency.F4);
                        break;
                    }
                case Mode.off:
                    {
                        currentState.isActive = false;
                        stateChanged(currentState);
                        break;
                    }
            }
            change = true;
        }
        private double Epsilon0 = 0;
        public void changeEpsilon(double e)
        {
            if (currentState.currentMode != Mode.IX105NP)
            {
                currentState.currentDirection.epsilon = e;
                Epsilon0 = e;
                directionChanged(currentState);
            }
            else
            {
                currentState.currentDirection.epsilon = 0.3;
                Epsilon0 = 0.3;
                directionChanged(currentState);
            }
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
            change = true;
            currentState.isActive = true;
            stateChanged(currentState);
        }
        public void turnOff()
        {
            currentState.isActive = false;
            stateChanged(currentState);
        }

        public void eventLoop()
        {
            while (true)
            {
                if (currentState.isActive == true)
                {
                    switch (currentState.currentMode)
                    {
                        case Mode.IX105NP:
                            {
                                change = false;
                                setFrequencies(Frequency.F1, Frequency.F2, Frequency.F4);
                                turnOn(1.0 / 3.0, 0, 0, currentState.currentDirection.epsilon, 315, 1);
                                break;
                            }
                        case Mode.IX105:
                            {
                                change = false;
                                setFrequencies(Frequency.F1, Frequency.F2, Frequency.F3, Frequency.F4);
                                turnOn(1.0 / 3.0, 0, 0, currentState.currentDirection.epsilon, 315, 1);
                                break;
                            }
                        case Mode.HX12:
                            {
                                change = false;
                                setFrequencies(Frequency.F1, Frequency.F2, Frequency.F3, Frequency.F4);
                                turnOn(1.0 / 3.0, 1.0 / 3.0, 46, currentState.currentDirection.epsilon, 36, 12);
                                break;
                            }
                        case Mode.off:
                            {
                                currentState.currentDirection.azimut = 0;
                                turnOff();
                                break;
                            }
                    }
                }
            }
        }

        private CompleteRPNState currentState = new CompleteRPNState(false, Mode.off, Frequency.F1, new ScanningDirection(0, 0));

        public struct CompleteRPNState {
            internal bool isActive;
            internal Mode currentMode;
            internal Frequency currentFrequency;
            internal ScanningDirection currentDirection;

            public CompleteRPNState(bool state, Mode m, Frequency f, ScanningDirection d) {
                this.isActive = state;
                this.currentMode = m;
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
        public event RPNEvent modeChanged;
        public event RPNEvent frequencyChanged;
        public event RPNEvent directionChanged;

        private void turnOn(double stepX, double stepY, double X0, double Y0, int NX, int NY)
        {
            int y = stepY == 0 ? 1 : (int)((currentState.currentDirection.epsilon - Y0)/stepY + 1);
            Vector4D x = 1;
            azimut = X0;
            changefreq = false;
            while ((currentState.isActive == true) & (change == false))
            {
                SortedSet<Frequency> currentSet = getFreqSet();
                bool yChanged = false;
                foreach (Frequency f in currentSet)
                {
                    Y0 = Epsilon0;
                    
                    var watch = Stopwatch.StartNew(); //it's for control precision of time measure

                    currentState.currentFrequency = f;
                    frequencyChanged(currentState);

                    x.set(f, x.get(f) + 1);
                    azimut.set(f, azimut.get(f) + stepX);
                    currentState.currentDirection.azimut = azimut.get(f) + stepX;
                    if (x.get(f) == NX + 1)
                    {
                        x.set(f, 1);
                        azimut.set(f, X0);
                        currentState.currentDirection.azimut = X0;
                        if (!stopPressed && !yChanged)
                        {
                            currentState.currentDirection.epsilon += stepY;
                            y++;
                            if (y == NY + 1)
                            {
                                y = 1;
                                currentState.currentDirection.epsilon = Y0;
                            }
                            yChanged = true;
                        }
                    }

                    directionChanged(currentState);

                    watch.Stop();
                    long elapsedMs = watch.ElapsedMilliseconds;
                    long delta = Constants.RPN_DELAY - elapsedMs;
                    if (delta < 0)
                    {
                        Console.Beep();
                    }
                    if (!currentState.isActive) break; 
                    System.Threading.Thread.Sleep((int)delta);
                    if (changefreq || change || !currentState.isActive) break; 
                }

                if (changefreq) {
                    azimut = currentState.currentDirection.azimut;
                    x = stepX == 0 ? 1 : (int)((currentState.currentDirection.azimut - X0) / stepX + 1);
                    changefreq = false;
                }
            }
        }

        internal Mode getCurrentMode()
        {
            Mode toRet = currentState.currentMode;
            return toRet;
        }
    }
}