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
        public int n = 1;
        private int delay = 1; //ms
        private Thread myThread;
        private volatile bool running = true;
        private struct PicturePosition
        {
            public Point xyi;
            public int windth;

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
                bool isWorking = (rpn.getCurrentMode() != RPN.Mode.off);
                if (isWorking)
                { if (rpn.on) { updateVisibility(true); }
                    SortedSet<RPN.Frequency> frequencySet = rpn.getFreqSet();
                    foreach (RPN.Frequency f in frequencySet)
                    {
                        if (n == 18) { n = 1; }
                        else { n++; }
                        l = 1;
                        while (l != rpn.delay)
                        {
                            PicturePosition currentposition = calcPosition(f,l);
                            updatePosition(currentposition);
                            l++;
                        }


                    }
                }
                else
                {
                    updateVisibility(false);
                }

                System.Threading.Thread.Sleep(delay);
            }
        }



        private PicturePosition calcPosition(RPN.Frequency f,int l)
        {
            int y0 = 2; int x0 = 2; int width = 0;
            int x = x0 + (n % 3) * 142;
            int y = y0 + (n % 6) * 36;
            PicturePosition currentposition = new PicturePosition();
            currentposition.xyi = new Point(x, y);
            pictureBox1.BackColor = Constants.getFreqColor(f);
 
            width = (int)(l * 142 / rpn.delay);
               
            
            currentposition.windth = width;
            return currentposition; 
            
                //Hey, it is the last instruction in this function!
            //So, that's the next?
           
           
        }
        delegate void update(PicturePosition currentPosition);
        private void updatePosition(PicturePosition currentPosition)
        {
            if (this.InvokeRequired)
            {
                update d = new update(updatePosition);
                this.Invoke(d, new object[] { currentPosition });
            }
            else
            {
                this.pictureBox1.Location = currentPosition.xyi;
                this.pictureBox1.Width = currentPosition.windth;
                this.label1.Location = currentPosition.xyi;
                
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
                this.pictureBox1.Visible= visible;
               }
        }
    }
}

