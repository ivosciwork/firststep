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
        Label[] tex1 = new Label[13];
        Label[] tex2 = new Label[7];
        private struct PictureLocation
        {
            public int x;
            public int y;
        }
        private void Diagramm_Load(object sender, EventArgs e)
        {
            this.Left = Screen.PrimaryScreen.Bounds.Height * 3 / 4;
            this.Top = 0;
            this.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width - Screen.PrimaryScreen.Bounds.Height * 3 / 4, Screen.PrimaryScreen.Bounds.Height / 2 - 30);
            ElementLocation();
        }
        private void ElementLocation()
        {
            calcdiagram();
            PictureLocation loc = calcLocation(x, y);
            updateLocation(loc);
            pictureBox1.Height = Lines2[2].Location.Y - Lines2[1].Location.Y;
            pictureBox21.Height = Lines2[2].Location.Y - Lines2[1].Location.Y;
            pictureBox22.Height = Lines2[2].Location.Y - Lines2[1].Location.Y;
        }
        private void Diagram_Resize(object sender, EventArgs e)
        {
            ElementLocation();
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
            Lines2[6] = pictureBox19; tex1[1] = label2;
            tex1[2] = label8;
            tex1[3] = label9;
            tex1[4] = label10;
            tex1[5] = label11;
            tex1[6] = label12;
            tex1[7] = label13;
            tex1[8] = label15;
            tex1[9] = label16;
            tex1[10] = label17;
            tex1[11] = label18;
            tex1[12] = label19;
            tex2[1] = label14;
            tex2[2] = label3;
            tex2[3] = label4;
            tex2[4] = label5;
            tex2[5] = label6;
            tex2[6] = label7;

            /* Rectangle rectAll = this.RectangleToClient(this.Bounds);
             Rectangle rectClient = this.ClientRectangle;
             int Top = rectClient.Top - rectAll.Top;
             int Left = rectClient.Left - rectAll.Left;
             int Right = rectAll.Right - rectClient.Right;
             int Botton = rectAll.Bottom - rectClient.Bottom;*/

            for (int i = 1; i <= 12; i++)
            {
                Lines1[i].Width = 2;
                Lines1[i].Height = this.Height - 36;
                Lines1[i].Location = new Point((int)((this.Width - 14) * 42 / 786 + (i - 1) * (this.Width - 14) * 62 / 786), 0);
                tex1[i].Location = new Point(Lines1[i].Location.X + 10, 1);
            }
            for (int i = 1; i <= 6; i++)
            {
                Lines2[i].Width = this.Width - 19;
                Lines2[i].Height = 2;
                Lines2[i].Location = new Point(0, (int)((this.Height - 36) * 22 / 364 + (i - 1) * (this.Height - 36) * 57 / 364));
                tex2[i].Location = new Point(1, Lines2[i].Location.Y + 10);

            }
            pictureBox20.Location = new Point(pictureBox2.Location.X + 2, pictureBox14.Location.Y + 2);
            pictureBox20.Width = this.Width - 19 - pictureBox2.Location.X - 2;
            pictureBox20.Height = this.Height - 36 - pictureBox14.Location.Y - 2;
            
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
                        long wight = calcWight(0);
                        updateWight(wight);
                    }
                }

                if (isFrequencyChanged)
                {
                    isFrequencyChanged = false;


                    x = Lines1[4 * (n % 3) + 1].Location.X + 2;
                    y = Lines2[(n / 3) + 1].Location.Y + 2;
                    long a = ((this.Width - 19) - (int)((this.Width - 19) * 33 / 800)) / 3;
                    long wight0 = calcWight(0);
                    updateWight(wight0);
                    updateWight2(wight0);
                    updateWight3(wight0);
                    PictureLocation loc = calcLocation(x, y);
                    updateLocation(loc);
                    var watch = Stopwatch.StartNew(); //it's for control precision of time measure
                    while (watch.ElapsedMilliseconds <= Constants.RPN_DELAY)
                    {
                        if (isFrequencyChanged || isStateChanged) { break; }
                        long wight = calcWight(watch.ElapsedMilliseconds);
                        if (wight > a*0.45/5.8)
                        {
                            updateWight3((long)(a * 0.25 / 5.8));
                            updateWight2((long)(a * 0.45 / 5.8));
                           
                            updateWight(wight);
                           
                        }
                        if ((wight < a * 0.45 / 5.8)&&(wight < a *0.25/5.8))
                        {
                            updateWight3((long)(a * 0.25 / 5.8));
                            updateWight2(wight);
                           
                        }
                        if (wight < a * 0.25 / 5.8)
                        {
                            updateWight3(wight);
                        }
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
                long a = ((this.Width - 19) - (int)((this.Width - 19) * 33 / 800)) / 3;
                this.pictureBox1.Location = new Point(loc.x + (int)(a * 0.45 / 5.8), loc.y);
                this.pictureBox21.Location = new Point(loc.x, loc.y);
                this.pictureBox22.Location = new Point(loc.x + (int)(a * 0.25 / 5.8), loc.y);
            }
        }

        private long calcWight(long l)
        {
            long wight ;
            wight = (int)(l*(5.35/5.8) * (((this.Width - 19) - (int)(this.Width - 19) * 33 / 800) / 3) /Constants.RPN_DELAY);
            pictureBox1.BackgroundImage = Constants.getFreqImage(frequency);
            return wight;
        }
        delegate void update1(long wight);
        private void updateWight(long wight)
        {
            if (this.InvokeRequired)
            {
                update1 d = new update1(updateWight);
                this.Invoke(d, new object[] { wight });
            }
            else
            {
                this.pictureBox1.Width = (int)wight;
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
                this.pictureBox21.Visible = visible;
                this.pictureBox22.Visible = visible;
            }
        }

        delegate void update2(long wight);
        private void updateWight2(long wight)
        {
            if (this.InvokeRequired)
            {
                update2 d = new update2(updateWight);
                this.Invoke(d, new object[] { wight });
            }
            else
            {
                this.pictureBox21.Width = (int)wight;
                this.pictureBox21.Height = Lines2[2].Location.Y - Lines2[1].Location.Y;

            }
        }
        delegate void update3(long wight);
        private void updateWight3(long wight)
        {
            if (this.InvokeRequired)
            {
                update3 d = new update3(updateWight3);
                this.Invoke(d, new object[] { wight });
            }
            else
            {
                this.pictureBox22.Width = (int)wight;
                this.pictureBox22.Height = Lines2[2].Location.Y - Lines2[1].Location.Y;

            }
        }
    }
}

