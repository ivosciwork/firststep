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
        int x, y;
        PictureBox[] Lines1 = new PictureBox[13];
        PictureBox[] Lines2 = new PictureBox[7];
           private struct PictureLocation
        {
            public int x;
            public int y;
        }
        
        private void ElementLocation()
        {
            /* x0 = (int)((this.Width) * 22 / 450);
             y0 =(int)((this.Height) * 17 / 277);
             x = x0 + (n % 3) *(this.Width ) * 140 / 450 + ((n % 3) / 2);
             y = y0 + (n / 3) * (this.Height ) * 37 / 277;*/
            calcdiagram();
            PictureLocation loc = calcLocation(x, y);
            updateLocation(loc);
            pictureBox1.Height = Lines2[2].Location.Y - Lines2[1].Location.Y;
        }
        private void Diagram_Resize(object sender, EventArgs e)
        {
            ElementLocation();
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
            this.pictureBox1.BringToFront();
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

        private void calcdiagram()
        {
            Lines1[1] = pictureBox2;
            Lines1[2] = pictureBox3;
            Lines1[3] = pictureBox4;
            Lines1[4] = pictureBox5;
            Lines1[5] = pictureBox6;
            Lines1[6] = pictureBox7;
            Lines1[7] = pictureBox8;
            Lines1[8] = pictureBox9;
            Lines1[9] = pictureBox10;
            Lines1[10] = pictureBox11;
            Lines1[11] = pictureBox12;
            Lines1[12] = pictureBox13;
            Lines2[1] = pictureBox14;
            Lines2[2] = pictureBox15;
            Lines2[3] = pictureBox16;
            Lines2[4] = pictureBox17;
            Lines2[5] = pictureBox18;
            Lines2[6] = pictureBox19;

           /* Rectangle rectAll = this.RectangleToClient(this.Bounds);
            Rectangle rectClient = this.ClientRectangle;
            int Top = rectClient.Top - rectAll.Top;
            int Left = rectClient.Left - rectAll.Left;
            int Right = rectAll.Right - rectClient.Right;
            int Botton = rectAll.Bottom - rectClient.Bottom;*/

            for (int i = 1; i <= 12; i++)
            {
                Lines1[i].Width = 2;
                Lines1[i].Height = this.Height-42;
                Lines1[i].Location = new Point((int)((this.Width-19) * 33 / 800 + (i - 1) * ((this.Width-19) - (int)(this.Width -19)* 33 / 800) / 12),0);

            }
            for (int i = 1; i < 7; i++)
            {
                Lines2[i].Width = this.Width - 19;
                Lines2[i].Height = 2;
                Lines2[i].Location = new Point(0, (int)((this.Height-42) * 21 / 400 + (i - 1) * ((this.Height-42) - (int)(this.Height -42)* 42 / 800) / 6));

            }
        }
        private void eventZaloop()
        {
            calcdiagram();
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

                    //x = x0 + (n % 3)  * 137 ;
                    x = Lines1[4*(n % 3)+1 ].Location.X+2;
                    //y = y0 + (n / 3) * (this.Height-36 ) * 37 / 277;
                    y = Lines2[(n / 3) + 1].Location.Y+2;
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
            wight.wight = (int)(l * (((this.Width - 19) - (int)(this.Width - 19) * 33 / 800) /3) / Constants.RPN_DELAY);
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
                this.pictureBox1.Height = Lines2[2].Location.Y - Lines2[1].Location.Y;

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

