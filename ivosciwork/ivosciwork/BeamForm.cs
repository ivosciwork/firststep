using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;
using System;

namespace ivosciwork
{
    public partial class BeamForm : Form
    {
        private Thread myThread;
        private volatile bool running = true;

        private struct BeamPosition {
            public Color color; 
            public Point spotLight;
            public Point upperBorder;
            public Point lowerBorder;
        }

        public BeamForm(RPN rpn)
        {
            InitializeComponent();

            //RPNEvent handlers
            rpn.directionChanged += this.directionChangedHandler;
            rpn.stateChanged += this.stateChangedHandler;
            rpn.frequencyChanged += this.frequencyChangedHandler;

            //resize event handler
            this.Resize += this.onResize;

            //thread for calculations
            this.myThread = new Thread(new ThreadStart( this.eventLoop ));
            myThread.IsBackground = true;
            myThread.Start();
        }

        private void onResize(object sender, EventArgs e)
        {
            this.upperBeamBorder.StartPoint = this.calcUpperBeamBorderPosition();
            this.lowerBeamBorder.StartPoint = this.calcLowerBeamBorderPosition();
        }

        private bool isFrequencyChanged = false;
        private RPN.Frequency frequency = RPN.Frequency.F1; 
        private void frequencyChangedHandler(RPN.CompleteRPNState currentState)
        {
            isFrequencyChanged = true;
            frequency = currentState.currentFrequency;
        }

        private bool isStateChanged = false;
        private bool isRpnOn = false;
        private void stateChangedHandler(RPN.CompleteRPNState currentState)
        {
            isStateChanged = true;
            isRpnOn = currentState.isActive;
        }

        private bool isDirectionChanged = false;
        private RPN.ScanningDirection beamDirection = new RPN.ScanningDirection(0,0);
        private void directionChangedHandler(RPN.CompleteRPNState currentState)
        {
            isDirectionChanged = true;
            beamDirection = currentState.currentDirection;
        }

        private BeamPosition currentPosition = new BeamPosition();
        public void eventLoop() {
            while (running)
            {
                if (isStateChanged) {
                    isStateChanged = false;
                    updateVisibility(isRpnOn);
                }

                if (isDirectionChanged || isFrequencyChanged)
                {
                    if (isDirectionChanged)
                    {
                        isDirectionChanged = false;
                        isFrequencyChanged = false;
                        currentPosition = calcCurrentPosition();
                    }
                    else
                    {
                        isFrequencyChanged = false;
                        currentPosition.color = Constants.getFreqColor(frequency);
                    }

                    updatePosition(currentPosition);
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

        private BeamPosition calcCurrentPosition() {
            double currentAzimut = beamDirection.azimut;
            double currentEpsilon = beamDirection.epsilon;
            BeamPosition currentPosition = new BeamPosition();
            currentPosition.spotLight = mapPosition( new Point((int)currentAzimut, (int)currentEpsilon) );
            currentPosition.upperBorder = calcUpperBeamBorderPosition();
            currentPosition.lowerBorder = calcLowerBeamBorderPosition();
            currentPosition.color = Constants.getFreqColor( frequency );
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

        private void BeamForm_Load(object sender, EventArgs e)
        {
            this.Left = 0;
            this.Top = Screen.PrimaryScreen.Bounds.Height / 2-30;
            this.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height / 2);
            this.upperBeamBorder.StartPoint = this.calcUpperBeamBorderPosition();
            this.lowerBeamBorder.StartPoint = this.calcLowerBeamBorderPosition();
        }
    }
}
