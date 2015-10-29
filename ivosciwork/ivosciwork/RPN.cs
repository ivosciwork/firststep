using System.Collections.Generic;

namespace ivosciwork
{
    class RPN
    {
        public enum Mode { IX105NP, IX105, HX12, off };
        private Mode currentMode;

        public void changeMode(Mode m) {
            currentMode = m;
        }

        private double azimut = 0;
        private double epsilon;
        public enum Frequency { F1, F2, F3, F4 };
        private HashSet<Frequency> frequencySet = new HashSet<Frequency>();
        private int delay = 1;
        private bool running = false;
        private bool stopPressed = false;

        public void changeEpsilon( double e ) {
            if (currentMode != Mode.IX105NP)
            {
                epsilon = e;
            }
        }

        public double getEpsilon() {
            return epsilon;
        }

        public double getAzimut() {
            return azimut;
        }

        public HashSet<Frequency> getFreqSet() {
            return frequencySet; 
        }

        public bool changeStopButtonState() {
            stopPressed = !stopPressed;
            return stopPressed;
        }

        public RPN() {
            setFrequencies( Frequency.F1, Frequency.F2, Frequency.F3, Frequency.F4);
        }

        private void setFrequencies( params Frequency[] f) {
            frequencySet.Clear();
            foreach (Frequency fr in f) {
                frequencySet.Add(fr);
            }
        }

        public void changeFrequency(Frequency f) {
            setFrequencies(f);
        }

        public void turnOn() {
            running = true;
        } 

        public void on() {
            while (true)
            {
                if (running == true)
                {
                    switch (currentMode)
                    {
                        case Mode.IX105NP:
                            {
                                setFrequencies(Frequency.F1, Frequency.F2, Frequency.F4);
                                eventLoop(1.0 / 3.0, 0, 0, 0.3, 315, 1);
                                break;
                            }
                        case Mode.IX105:
                            {
                                setFrequencies(Frequency.F1, Frequency.F2, Frequency.F3, Frequency.F4);
                                eventLoop(1.0 / 3.0, 0, 0, epsilon, 315, 1);
                                break;
                            }
                        case Mode.HX12:
                            {
                                setFrequencies(Frequency.F1, Frequency.F2, Frequency.F3, Frequency.F4);
                                eventLoop(1.0 / 3.0, 1.0 / 3.0, 106, epsilon, 36, 12);
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

        private void eventLoop( double stepX, double stepY, double X0, double Y0, int NX, int NY ) {
            while (running) {
                epsilon = Y0;
                for (int y = 0; y < NY; y++) {
                    azimut = X0;
                    do {
                        for (int x = 0; x < NX; x++) {
                            azimut += stepX;
                            System.Threading.Thread.Sleep(delay * frequencySet.Count);
                        }
                    } while ( stopPressed );
                    epsilon += stepY;
                }
            }
        }
    }
}
