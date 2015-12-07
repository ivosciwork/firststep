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
        private int delay = 25; //ms
        private Thread myThread;
        private volatile bool running = true;
        private struct PicturePosition
        {//элементы отображения
            public Point xyi;

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
                {

                    HashSet<RPN.Frequency> frequencySet = rpn.getFreqSet();
                    foreach (RPN.Frequency f in frequencySet)
                    {
                        PicturePosition currentposition = calcPosition(f);
                        updatePosition(currentposition);

                    }
                }

                System.Threading.Thread.Sleep(delay);
            }
        }



        private PicturePosition calcPosition(RPN.Frequency f)
        {
            int y0 = 2; int x0 = 2;
            int x = x0 + (rpn.n % 3) * 142;
            int y = y0 + (rpn.n % 6) * 36;
            PicturePosition currentposition = new PicturePosition();
            currentposition.xyi = new Point(x, y);
            if ((int)f == 0) pictureBox1.BackColor = Color.Red;
            if ((int)f == 1) pictureBox1.BackColor = Color.Green;
            if ((int)f == 2) pictureBox1.BackColor = Color.Blue;
            if ((int)f == 3) pictureBox1.BackColor = Color.Yellow;
            return currentposition; //Hey, it is the last instruction in this function!
            //So, that's the next?
            pictureBox1.Width = 0;
            timer();


            label1.Text = f.ToString();
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
                this.label1.Location = currentPosition.xyi;
            }
        }

        private void timer()
        {
            int l = 1;
            while (l != rpn.delay)
            {
                pictureBox1.Width = (int)(l * 142 / rpn.delay);
                l++;
            }
        }
    }
}

