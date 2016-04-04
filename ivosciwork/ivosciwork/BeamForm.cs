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
            this.SpotLight.Location = calcCurrentPosition().spotLight;
            this.upperBeamBorder.StartPoint = this.calcUpperBeamBorderPosition(this.SpotLight.Location);
            this.lowerBeamBorder.StartPoint = this.calcLowerBeamBorderPosition(this.SpotLight.Location);
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
            if (this.InvokeRequired) {
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
            currentPosition.upperBorder = calcUpperBeamBorderPosition(currentPosition.spotLight);
            currentPosition.lowerBorder = calcLowerBeamBorderPosition(currentPosition.spotLight);
            currentPosition.color = Constants.getFreqColor( frequency );
            return currentPosition;
        }

        //This function determines how real azimut/epsilon will be shown on screen
        private Point mapPosition(Point realBeamPosition) {
            return simpleRectangularMap(realBeamPosition);
        }

        private Point simpleRectangularMap(Point realBeamPosition) {
            //This implements rectangular mapping
            Size formSize = this.Size;
            System.Windows.Rect myRelativeRectangle = new System.Windows.Rect(0.1, 0.2, 0.8, 0.5);
            System.Windows.Rect myShowRectangle = new System.Windows.Rect(formSize.Width * myRelativeRectangle.X, formSize.Height * myRelativeRectangle.Y, formSize.Width * myRelativeRectangle.Width, formSize.Height * myRelativeRectangle.Height);
            Point screenBeamPosition = new Point();
            screenBeamPosition.X = (int)myShowRectangle.X + (int)(myShowRectangle.Width * realBeamPosition.X / 315.0);
            screenBeamPosition.Y = (int)(myShowRectangle.Y + myShowRectangle.Height) - (int)(myShowRectangle.Height * realBeamPosition.Y / 70.0);

            return screenBeamPosition;
        }

        private Point calcUpperBeamBorderPosition(Point spotLight)
        {
            int r = this.SpotLight.Width / 2;
            int a = this.upperBeamBorder.X2 - this.upperBeamBorder.X1;
            int b = this.upperBeamBorder.Y2 - this.upperBeamBorder.Y1;
            int c = (int)System.Math.Sqrt(System.Math.Pow(a, 2) + System.Math.Pow(b, 2));
            int x = spotLight.X + (int)(r * (1 + (double)b / c));
            int y = spotLight.Y + (int)(r * (1 - (double)a / c));
            return new Point(x,y);
        }

        private Point calcLowerBeamBorderPosition(Point spotLight)
        {
            int r = this.SpotLight.Width / 2;
            int a = this.upperBeamBorder.X2 - this.upperBeamBorder.X1;
            int b = this.upperBeamBorder.Y2 - this.upperBeamBorder.Y1;
            int c = (int)System.Math.Sqrt(System.Math.Pow(a, 2) + System.Math.Pow(b, 2));
            int x = spotLight.X + (int)(r * (1 - (double)b / c));
            int y = spotLight.Y + (int)(r * (1 + (double)a / c));
            return new Point(x, y);
        }

        private void BeamForm_Load(object sender, EventArgs e)
        {
            this.Left = 0;
            this.Top = Screen.PrimaryScreen.Bounds.Height / 2-30;
            this.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height / 2);
        }
    }
}
