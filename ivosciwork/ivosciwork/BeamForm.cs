using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;
using System;
using Microsoft.VisualBasic.PowerPacks;

namespace ivosciwork
{
    public partial class BeamForm : Form
    {
        private Thread myThread;
        private volatile bool running = true;
        private Image sector1x105 = new Bitmap(ivosciwork.Properties.Resources.Sector1x105);
        private Image sector4x12 = new Bitmap(ivosciwork.Properties.Resources.Sector4x12);
        private Graphics g;

        private struct CurrentState {
            public Color color;
            public Point spotLight;
            public Point upperBorder;
            public Point lowerBorder;

            public Image sector;
            public Point leftUpper;
            public Point leftDown;
            public Point rightUpper;
        }

        private CurrentState state = new CurrentState();
        public BeamForm(RPN rpn)
        {
            InitializeComponent();

            //this.sector.Width = this.Width / 6;
            //sector.SendToBack();
            //sector.Visible = false;
            SpotLight.BringToFront();

            state.sector = this.sector1x105;

            this.SpotLight.Size = new System.Drawing.Size(20, 20);

            //RPNEvent handlers
            rpn.directionChanged += this.directionChangedHandler;
            rpn.stateChanged += this.stateChangedHandler;
            rpn.modeChanged += this.modeChangedHandler;
            rpn.frequencyChanged += this.frequencyChangedHandler;

            //resize event handler
            this.Resize += this.onResize;

            //state.sector = Sector1x105;
            //state.sector.Visible = false;
            //state.secPos = new Rectangle();

            //thread for calculations
            this.myThread = new Thread(new ThreadStart( this.eventLoop ));
            myThread.IsBackground = true;
            myThread.Start();
        }

        private bool isModeChanged = false;
        private RPN.Mode mode = RPN.Mode.off;
        private void modeChangedHandler(RPN.CompleteRPNState currentState)
        {
            mode = currentState.currentMode;
            isRpnOn = mode == RPN.Mode.off ? false : isRpnOn;
            isModeChanged = true;
        }

        private double upperLimit;
        private double downLimit;
        private double leftLimit;
        private double rightLimit;
        private double delta;

        private double degreeHeight;
        private double degree;

        private void onResize(object sender, EventArgs e)
        {
            g = this.CreateGraphics();
            //this.sector.Width = this.Width / 6;
            upperLimit = (int)(this.Size.Height * 0);
            downLimit = (int)(this.Size.Height * 0.8);
            leftLimit = (int)(this.Size.Width * 0.1);
            rightLimit = (int)(this.Size.Width * 0.5);
            delta = (int)(this.Size.Height * 0.3);

            degreeHeight = (int)((downLimit - delta - upperLimit) / 30.0);
            degree = (int)((downLimit - delta - upperLimit) / 81.0);

            this.SpotLight.Size = new System.Drawing.Size((int)degreeHeight, (int)degreeHeight);
            this.SpotLight.Location = getCurrentState().spotLight;
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

        public void eventLoop() {
            while (running)
            {

                if (isModeChanged) {
                    isModeChanged = false;
                    if (mode != RPN.Mode.off)
                    {
                        if (mode == RPN.Mode.HX12)
                        {
                            state.sector = this.sector4x12;
                        }
                        else
                        {
                            state.sector = this.sector1x105;
                        }
                        updateVisibility(isRpnOn);
                    }
                }

                if (isStateChanged)
                {
                    isStateChanged = false;
                    updateVisibility(isRpnOn);
                }

                if (isDirectionChanged || isFrequencyChanged)
                {
                    if (isDirectionChanged)
                    {
                        isDirectionChanged = false;
                        isFrequencyChanged = false;
                        state = getCurrentState();
                    }
                    else
                    {
                        isFrequencyChanged = false;
                        state.color = Constants.getFreqColor(frequency);
                    }

                    updatePosition(state);
                }
            }
        }

        //private Rectangle calcSectorPosition()
        //{
        //    if (state.sector == this.Sector4x12)
        //    {
        //        double Epsilon0 = ControlForm.getEpsilon0();// Нехорошо так делать!!!
        //        Rectangle secPos = new Rectangle();
        //        secPos.Height = sector.Height / 9;
        //        secPos.Width = (int)(sector.Width * 0.13);
        //        secPos.X = sector.Location.X + sector.Width * 47 / 105;
        //        secPos.Y = (int)(sector.Location.Y + 0.78 * sector.Height - secPos.Height - Epsilon0 * 0.5 * sector.Height / 80 );
        //        return secPos;
        //    }
        //    else
        //    {
        //        Rectangle secPos = new Rectangle();
        //        secPos.Height =(int)( 0.41 * sector.Height );
        //        secPos.Width = sector.Width;
        //        secPos.X = sector.Location.X;
        //        secPos.Y = (int)( sector.Location.Y + 0.54 * sector.Height - beamDirection.epsilon * 0.5 * sector.Height / 80 );
        //        return secPos;
        //    }
        //}

        delegate void updatePositionCallBack( CurrentState s);//

        private void updatePosition(CurrentState s) {
            if (this.InvokeRequired) {
                updatePositionCallBack d = new updatePositionCallBack(updatePosition);
                this.Invoke(d, new object[] { s });
            } else {

                Point[] destinationPoints = {
                state.leftUpper,   // destination for upper-left point of original
                state.rightUpper,  // destination for upper-right point of original
                state.leftDown};  // destination for lower-left point of original

                // Draw the image mapped to the parallelogram.
                if (isRpnOn)
                {
                    g.Clear(BackColor);
                    g.DrawImage(state.sector, destinationPoints);
                    g.DrawLine(new Pen(Color.Black, 1), LittleRPN.Location, s.upperBorder);
                    g.DrawLine(new Pen(Color.Black, 1), LittleRPN.Location, s.lowerBorder);
                    System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(s.color);

                    g.FillEllipse(myBrush, new Rectangle(s.spotLight, new System.Drawing.Size((int)degreeHeight, (int)degreeHeight)));
                }

                //this.SpotLight.Location = s.spotLight;
                //this.SpotLight.FillColor = s.color;

                //s.sector.Height = s.secPos.Height;
                //s.sector.Width = s.secPos.Width;
                //s.sector.Location = new Point(s.secPos.X,s.secPos.Y);
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

                if (!visible) {
                    g.Clear(this.BackColor);
                }
            }
        }

        private CurrentState getCurrentState() {
            double currentAzimut = beamDirection.azimut;
            double currentEpsilon = beamDirection.epsilon;
            CurrentState currentPosition = new CurrentState();

            currentPosition.sector = state.sector;
            Point[] sectorPos = mapSectorPosition(currentEpsilon);

            currentPosition.leftUpper = sectorPos[0];
            currentPosition.rightUpper = sectorPos[1];
            currentPosition.leftDown = sectorPos[2];

            currentPosition.spotLight = mapPosition( currentAzimut, currentEpsilon);
            currentPosition.upperBorder = calcUpperBeamBorderPosition(currentPosition.spotLight);
            currentPosition.lowerBorder = calcLowerBeamBorderPosition(currentPosition.spotLight);
            currentPosition.color = Constants.getFreqColor( frequency );
            return currentPosition;
        }

        private Point[] mapSectorPosition(double currentEpsilon)
        {
            Point[] secPos = new Point[3];

            if (state.sector == sector1x105)
            {
                secPos[0].Y = (int)(downLimit - (11 + currentEpsilon) * degree);
                secPos[1].Y = (int)(secPos[0].Y - delta);
                secPos[2].Y = (int)(secPos[0].Y + degreeHeight);
                secPos[0].X = (int)leftLimit;
                secPos[1].X = (int)rightLimit;
                secPos[2].X = (int)leftLimit;
            }
            else
            {
                secPos[2].Y = (int)((downLimit - (11 + ControlForm.getEpsilon0()) * degree) - delta * 46 / 105.0 + degreeHeight);
                secPos[0].Y = (int)(secPos[2].Y - degree * 6.5);
                secPos[1].Y = (int)(secPos[0].Y - delta * 14 / 105.0);
                secPos[0].X = (int)(leftLimit + (rightLimit - leftLimit) * 46 / 105.0);
                secPos[1].X = (int)(leftLimit + (rightLimit - leftLimit) * 60 / 105.0);
                secPos[2].X = (int)(leftLimit + (rightLimit - leftLimit) * 46 / 105.0);
            }
            return secPos;
        }

        //This function determines how real azimut/epsilon will be shown on screen
        private Point mapPosition(double az, double eps) {
            return parallelogramMap(az,eps);
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

        private Point parallelogramMap(double az, double eps)
        {
            //This implements parallelogram mapping
            Point screenBeamPosition = new Point();
            double leftY = (int)(downLimit - (11 + eps) * degree);
            double rightY = leftY - delta;
            screenBeamPosition.X = (int)(leftLimit + (rightLimit - leftLimit) * (az - 1) / 105.0);
            screenBeamPosition.Y = (int)(leftY - degreeHeight * (delta / (rightLimit - leftLimit))/2 + (rightY - leftY) * (az - 1) / 105.0);

            return screenBeamPosition;
        }

        private Point calcUpperBeamBorderPosition(Point spotLight)
        {
            int r = this.SpotLight.Width / 2;
            int a = this.LittleRPN.Location.X - this.SpotLight.Location.X;
            int b = this.LittleRPN.Location.Y - this.SpotLight.Location.Y;
            int c = (int)System.Math.Sqrt(System.Math.Pow(a, 2) + System.Math.Pow(b, 2));
            int x = spotLight.X + (int)(r * (1 + (double)b / c));
            int y = spotLight.Y + (int)(r * (1 - (double)a / c));
            return new Point(x,y);
        }

        private Point calcLowerBeamBorderPosition(Point spotLight)
        {
            int r = this.SpotLight.Width / 2;
            int a = this.LittleRPN.Location.X - this.SpotLight.Location.X;
            int b = this.LittleRPN.Location.Y - this.SpotLight.Location.Y;
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
