using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace ivosciwork
{


    public partial class Diagramm : Form
    {
        public int l = 1;
        public int n = 0;
        private Thread myThread;
        private volatile bool running = true;
        int y0 = 2;
        int x0 = 2;
        int x, y;
        private struct PictureLocation
        {
            public int x;
            public int y;
        }
        private struct PictureWight
        { 
            public int wight;
        }
        public Diagramm(RPN rpn)
        {
            InitializeComponent();
            rpn.frequencyChanged += frequencyChangedHandler;
            rpn.stateChanged += stateChangedHandler;
            this.myThread = new Thread(new ThreadStart(this.eventZaloop));
            myThread.IsBackground = true;
            myThread.Start();
        }

        private bool isStateChanged = false;
        private bool isRpnOn = false;
        private void stateChangedHandler(RPN.CompleteRPNState currentState)
        {
            isStateChanged = true;
            isRpnOn = currentState.isActive;
        }

        private bool isFrequencyChanged = false;
        private RPN.Frequency frequency = RPN.Frequency.F1;
        private void frequencyChangedHandler(RPN.CompleteRPNState currentState)
        {
            isFrequencyChanged = true;
            frequency = currentState.currentFrequency;
        }

        private void eventZaloop()
        {
            while (running)
            {
                if (isStateChanged)
                {
                    isStateChanged = false;
                    updateVisibility(isRpnOn);
                    if (!isRpnOn) {
                        n = 0;
                        l = 1;
                        PictureWight wight = calcWight(0);
                        updateWight(wight);
                    }
                }

                if (isFrequencyChanged) {
                    isFrequencyChanged = false;
                    pictureBox1.BackColor = Constants.getFreqColor(frequency);

                    x = x0 + (n % 3) * 142;
                    y = y0 + (n / 3) * 38;
                    PictureWight wight0 = calcWight(0);
                    updateWight(wight0);
                    PictureLocation loc = calcLocation(x, y);
                    updateLocation(loc);
                    var watch = Stopwatch.StartNew(); //it's for control precision of time measure
                    while (watch.ElapsedMilliseconds <= Constants.RPN_DELAY)
                    {
                        if (isFrequencyChanged || isStateChanged) { break; }
                        PictureWight wight = calcWight(watch.ElapsedMilliseconds);
                        updateWight(wight);
                    }
                    watch.Stop();
                    if (n == 17) { n = 0; }
                    else { n++; }
                }

                //if (tickHappened) {
                //    long sector = currentTick ...;
                //}

                //if (rpn.on)
                //{
                //    SortedSet<RPN.Frequency> frequencySet = rpn.getFreqSet();
                //    foreach (RPN.Frequency f in frequencySet)
                //    {
                //        if (!rpn.on) { break; }
                //        pictureBox1.BackColor = Constants.getFreqColor(f);
                //        if (rpn.on) { updateVisibility(true); }
                        
                //        x = x0 + (n % 3) * 142;
                //        y = y0 + (n / 3) * 38;
                //        PictureLocation loc = calcLocation(x, y);
                //        updateLocation(loc);
                //        l = 1;
                //        while (l != Constants.RPN_DELAY)
                //        {
                //            if (!rpn.on) { break;}
                //            PictureWight wight = calcWight(l);
                //            updateWight(wight);
                //            l++;
                //            System.Threading.Thread.Sleep(1);
                //        }
                //        if (n == 17 ) { n = 0; }
                //        else { n++; }
                //    }
                //}
                //else
                //{
                //    n = 0;
                //    l = 1;
                //    PictureWight wight = calcWight(0);
                //    updateWight(wight);

                //}
            }
        }  

        private PictureLocation calcLocation(int x, int y)
        {
            PictureLocation loc = new PictureLocation();
            loc.x = x;
            loc.y = y;
            return loc;
        }
        delegate void update(PictureLocation loc);
        private void updateLocation(PictureLocation loc)
        {
            if (this.InvokeRequired)
            {
                update d = new update(updateLocation);
                this.Invoke(d, new object[] { loc });
            }
            else
            {
                this.pictureBox1.Location = new Point(loc.x,loc.y);
            }
        }

        private PictureWight calcWight(long l)
        {
            PictureWight wight = new PictureWight();
            wight.wight = (int)(l * 142 / Constants.RPN_DELAY);
            return wight;
        }
        delegate void update1(PictureWight wight);
        private void updateWight(PictureWight wight)
        {
            if (this.InvokeRequired)
            {
                update1 d = new update1(updateWight);
                this.Invoke(d, new object[] { wight });
            }
            else
            {
                this.pictureBox1.Width = wight.wight;
            }
        }
        delegate void updateVisibilityCallBack(bool visible);

        private void updateVisibility(bool visible)
        {
            if (this.InvokeRequired)
            {
                updateVisibilityCallBack d = new updateVisibilityCallBack(updateVisibility);
                this.Invoke(d, new object[] { visible });
            }
            else
            {
                this.pictureBox1.Visible = visible;
            }



        } 
    }
}

