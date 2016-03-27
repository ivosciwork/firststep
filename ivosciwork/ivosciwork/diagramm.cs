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
        int x, y,y0=17,x0=22;
        private struct PictureLocation
        {
            public int x;
            public int y;
        }
        
        private void ElementLocation(object sender, EventArgs e)
        {
          x0 = (int)((this.Width) * 22 / 450);
          y0 =(int)((this.Height) * 17 / 277);
          x = x0 + (n % 3) *(this.Width ) * 137 / 450 + ((n % 3) / 2);
          y = y0 + (n / 3) * (this.Height ) * 37 / 277;
            PictureLocation loc = calcLocation(x, y);
            updateLocation(loc);
            pictureBox1.Size = new Size(0, (int)((this.Height ) * 42 / 277));
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
                    if (!isRpnOn)
                    {
                        n = 0;
                        l = 1;
                        PictureWight wight = calcWight(0);
                        updateWight(wight);
                    }
                }

                if (isFrequencyChanged)
                {
                    isFrequencyChanged = false;
                    pictureBox1.BackgroundImage = Constants.getFreqImage(frequency);

                    x = x0 + (n % 3) *(this.Width ) * 137 / 450 + ((n % 3) / 2);
                    y = y0 + (n / 3) * (this.Height ) * 37 / 277;
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
                this.pictureBox1.Location = new Point(loc.x, loc.y);
            }
        }

        private PictureWight calcWight(long l)
        {
            PictureWight wight = new PictureWight();
            wight.wight = (int)(l * (this.Width) * 137 / 450 / Constants.RPN_DELAY);
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

