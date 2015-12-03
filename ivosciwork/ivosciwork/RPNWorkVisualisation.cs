using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System;

namespace ivosciwork
{
    public partial class BeamForm : Form
    {
        private RPN rpn;
        private int delay = 1; //ms
        private Thread myThread;

        public BeamForm(RPN rpn)
        {
            InitializeComponent();
            this.rpn = rpn;
            this.myThread = new Thread(new ThreadStart( this.eventLoop));
            myThread.Start();
        }

        public void eventLoop() {
            bool isWorking = (rpn.getCurrentMode() != RPN.Mode.off);
            if (isWorking)
            {
                this.SpotLight.Visible = true;
                this.upperBeamBorder.Visible = true;
                this.lowerBeamBorder.Visible = true;

                double currentAzimut = rpn.getAzimut();
                double currentEpsilon = rpn.getEpsilon();
                Point currentPosition = new Point((int)currentAzimut, (int)currentEpsilon);
                this.SpotLight.Location = mapPosition(currentPosition);
                this.upperBeamBorder.StartPoint = calcUpperBeamBorderPosition();
                this.lowerBeamBorder.StartPoint = calcLowerBeamBorderPosition();
            }
            else
            {
                this.SpotLight.Visible = false;
                this.upperBeamBorder.Visible = false;
                this.lowerBeamBorder.Visible = false;
            }
            System.Threading.Thread.Sleep(delay);
        }

        private Point mapPosition(Point realBeamPosition) {
            //This implements rectangular mapping
            Rectangle myShowRectangle = new Rectangle( 30, 20, 500, 300 );
            Point screenBeamPosition = new Point();
            screenBeamPosition.X = myShowRectangle.X + myShowRectangle.Width * (int) (realBeamPosition.X / 315.0);
            screenBeamPosition.Y = (myShowRectangle.Y + myShowRectangle.Height) - myShowRectangle.Height * (int)(realBeamPosition.Y / 70.0);

            return screenBeamPosition;
        }

        private Point calcUpperBeamBorderPosition()
        {
            int r = this.SpotLight.Width / 2;
            int a = this.upperBeamBorder.X2 - this.upperBeamBorder.X1;
            int b = this.upperBeamBorder.Y2 - this.upperBeamBorder.Y1;
            int c = (int)System.Math.Sqrt(System.Math.Pow(a, 2) + System.Math.Pow(b, 2));
            int x = this.SpotLight.Left + (int)(r * (1 + (double)b / c));
            int y = this.SpotLight.Top + (int)(r * (1 - (double)a / c));
            return new Point(x,y);
        }

        private Point calcLowerBeamBorderPosition()
        {
            int r = this.SpotLight.Width / 2;
            int a = this.upperBeamBorder.X2 - this.upperBeamBorder.X1;
            int b = this.upperBeamBorder.Y2 - this.upperBeamBorder.Y1;
            int c = (int)System.Math.Sqrt(System.Math.Pow(a, 2) + System.Math.Pow(b, 2));
            int x = this.SpotLight.Left + (int)(r * (1 - (double)b / c));
            int y = this.SpotLight.Top + (int)(r * (1 + (double)a / c));
            return new Point(x, y);
        }
    }
}
