using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;


namespace ivosciwork
{


    public partial class Diagramm : Form
    {

        private RPN rpn;
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
            this.rpn = rpn;
            this.myThread = new Thread(new ThreadStart(this.eventZaloop));
            myThread.Start();

        }

        private void eventZaloop()
        {
            while (running)
            {
                if (rpn.on)
                {
                    SortedSet<RPN.Frequency> frequencySet = rpn.getFreqSet();
                    foreach (RPN.Frequency f in frequencySet)
                    {
                        if (!rpn.on) { break; }
                        if ((int)f == 0) pictureBox1.BackColor = Color.Red;
                        if ((int)f == 1) pictureBox1.BackColor = Color.Green;
                        if ((int)f == 2) pictureBox1.BackColor = Color.Blue;
                        if ((int)f == 3) pictureBox1.BackColor = Color.Yellow;
                        if (rpn.on) { updateVisibility(true); }
                        
                            x = x0 + (n % 3) * 142;
                            y = y0 + (n / 3) * 38;
                            PictureLocation loc = calcLocation(x, y);
                            updateLocation(loc);
                            l = 1;
                            while (l != rpn.delay)
                            {   
                                if (!rpn.on) { break;}
                                PictureWight wight = calcWight(l);
                                updateWight(wight);
                                l++;
                                System.Threading.Thread.Sleep(1);

                            }
                            if (n == 17 ) { n = 0; }
                            else { n++; }

                        


                    }

                }
                else
                {
                    n = 0;
                    l = 1;
                    PictureWight wight = calcWight(0);
                    updateWight(wight);

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
                this.pictureBox1.Location = new Point(loc.x,loc.y);
            }
        }

        private PictureWight calcWight(int l)
        {
            PictureWight wight = new PictureWight();
            wight.wight = (int)(l * 142 / rpn.delay);
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

