using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;

namespace ivosciwork
{
    public partial class BeamForm : Form
    {
        private RPN rpn;
        private int delay = 100; //ms
        private Thread myThread;
        private volatile bool running = true;

        private struct BeamPosition {
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
                bool isWorking = (rpn.getCurrentMode() != RPN.Mode.off);
                if (isWorking)
                {
                    updateVisibility(true);
                    HashSet<RPN.Frequency> frequencySet = rpn.getFreqSet();
                    foreach (RPN.Frequency f in frequencySet) {
                        BeamPosition currentPosition = calcCurrentPosition(f);
                        updatePosition(currentPosition);
                    }
                }
                else
                {
                    updateVisibility(false);
                }
                System.Threading.Thread.Sleep(delay);
            }
        }

        delegate void updatePositionCallBack( BeamPosition currentPosition );//

        private void updatePosition(BeamPosition currentPosition) {
            if (this.InvokeRequired) {
                updatePositionCallBack d = new updatePositionCallBack(updatePosition);
                this.Invoke(d, new object[] { currentPosition });
            } else {//иначе напрямую вызываем
                this.SpotLight.Location = currentPosition.spotLight;
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
