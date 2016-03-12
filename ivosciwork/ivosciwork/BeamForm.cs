using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;

namespace ivosciwork
{
    public partial class BeamForm : Form
    {
        private RPN rpn;
        private Thread myThread;
        private volatile bool running = true;
        private bool visible = false;

        private struct BeamPosition {
            public Color color; 
            public Point spotLight;
            public Point upperBorder;
            public Point lowerBorder;
        }

        public BeamForm(RPN rpn)
        {
            InitializeComponent();
            this.rpn = rpn;
            this.myThread = new Thread(new ThreadStart( this.eventLoop ));
            myThread.Start();
            //this.Disposed += BeamForm_Disposed;
        }

        //private void BeamForm_Disposed(object sender, EventArgs e)
        //{
        //    running = false;
        //}

        public void eventLoop() {
            while (running)
            {
                if (rpn.on)
                {
                    if (visible == false)
                    {
                        updateVisibility(true);
                        visible = true;
                    }
                    SortedSet<RPN.Frequency> frequencySet = rpn.getFreqSet();
                    foreach (RPN.Frequency f in frequencySet) {
                        if (!rpn.on) { break; }
                        BeamPosition currentPosition = calcCurrentPosition(f);
                        updatePosition(currentPosition);
                        int l = 1;
                        while (l != Constants.RPN_DELAY)
                        {
                            if (!rpn.on) { break; }
                            l++;
                            System.Threading.Thread.Sleep(1);
                        }
                    }
                }
                else
                {
                    if (visible == true)
                    {
                        updateVisibility(false);
                        visible = false;
                    }
                }
            }
        }

        delegate void updatePositionCallBack( BeamPosition currentPosition);//

        private void updatePosition(BeamPosition currentPosition) {
            if (this.shapeContainer1.InvokeRequired) {
                updatePositionCallBack d = new updatePositionCallBack(updatePosition);
                this.Invoke(d, new object[] { currentPosition });
            } else {
                this.SpotLight.Location = currentPosition.spotLight;
                this.SpotLight.FillColor = currentPosition.color;
                this.upperBeamBorder.StartPoint = currentPosition.upperBorder;
                this.lowerBeamBorder.StartPoint = currentPosition.lowerBorder;
            }
        }

        delegate void updateVisibilityCallBack(bool visible);

        private void updateVisibility(bool visible) {
            if (this.InvokeRequired)
            {
                updateVisibilityCallBack d = new updateVisibilityCallBack(updateVisibility);
                this.Invoke(d, new object[] { visible });
            }
            else
            {
                this.SpotLight.Visible = visible;
                this.upperBeamBorder.Visible = visible;
                this.lowerBeamBorder.Visible = visible;
            }
        }

        private BeamPosition calcCurrentPosition( RPN.Frequency f ) {
            double currentAzimut = rpn.getAzimut().get(f);
            double currentEpsilon = rpn.getEpsilon();
            BeamPosition currentPosition = new BeamPosition();
            currentPosition.spotLight = mapPosition( new Point((int)currentAzimut, (int)currentEpsilon) );
            currentPosition.upperBorder = calcUpperBeamBorderPosition();
            currentPosition.lowerBorder = calcLowerBeamBorderPosition();
            currentPosition.color = Constants.getFreqColor(f);
            return currentPosition;
        }

        //This function determines how real azimut/epsilon will be shown on screen
        private Point mapPosition(Point realBeamPosition) {
            //This implements rectangular mapping
            Rectangle myShowRectangle = new Rectangle( 30, 20, 500, 300 );
            Point screenBeamPosition = new Point();
            screenBeamPosition.X = myShowRectangle.X + (int)(myShowRectangle.Width * realBeamPosition.X / 315.0);
            screenBeamPosition.Y = (myShowRectangle.Y + myShowRectangle.Height) - (int)(myShowRectangle.Height * realBeamPosition.Y / 70.0);

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
