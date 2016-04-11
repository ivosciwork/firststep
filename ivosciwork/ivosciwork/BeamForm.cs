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

        private struct BeamPosition {
            public Color color; 
            public Point spotLight;
            public Point upperBorder;
            public Point lowerBorder;
        }

        private struct CurrentState {
            public BeamPosition currentPosition;
            public RectangleShape sector;
            public Rectangle secPos;
        }

        private CurrentState state = new CurrentState();
        public BeamForm(RPN rpn)
        {
            InitializeComponent();

            this.sector.Width = this.Width / 6;
            sector.SendToBack();
            sector.Visible = false;
            SpotLight.BringToFront();
            upperBeamBorder.BringToFront();
            lowerBeamBorder.BringToFront();

            //RPNEvent handlers
            rpn.directionChanged += this.directionChangedHandler;
            rpn.stateChanged += this.stateChangedHandler;
            rpn.modeChanged += this.modeChangedHandler;
            rpn.frequencyChanged += this.frequencyChangedHandler;

            //resize event handler
            this.Resize += this.onResize;

            state.sector = Sector1x105;
            state.sector.Visible = false;
            state.secPos = new Rectangle();

            //thread for calculations
            this.myThread = new Thread(new ThreadStart( this.eventLoop ));
            myThread.IsBackground = true;
            myThread.Start();
        }

        private bool isModeChanged = false;
        private RPN.Mode mode = RPN.Mode.off;
        private void modeChangedHandler(RPN.CompleteRPNState currentState)
        {
            isModeChanged = true;
            mode = currentState.currentMode;
        }

        private void onResize(object sender, EventArgs e)
        {
            this.sector.Width = this.Width / 6;
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

        public void eventLoop() {
            while (running)
            {

                if (isModeChanged) {
                    isModeChanged = false;
                    if (mode != RPN.Mode.off)
                    {
                        if (mode == RPN.Mode.HX12)
                        {
                            state.sector = this.Sector4x12;
                        }
                        else
                        {
                            state.sector = this.Sector1x105;
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
                        state.currentPosition = calcCurrentPosition();
                        state.secPos = calcSectorPosition();
                    }
                    else
                    {
                        isFrequencyChanged = false;
                        state.currentPosition.color = Constants.getFreqColor(frequency);
                    }

                    updatePosition(state);
                }
            }
        }

        private Rectangle calcSectorPosition()
        {
            if (state.sector == this.Sector4x12)
            {
                double Epsilon0 = ControlForm.getEpsilon0();// Нехорошо так делать!!!
                Rectangle secPos = new Rectangle();
                secPos.Height = sector.Height / 9;
                secPos.Width = (int)(sector.Width * 0.13);
                secPos.X = sector.Location.X + sector.Width * 47 / 105;
                secPos.Y = (int)(sector.Location.Y + 0.78 * sector.Height - secPos.Height - Epsilon0 * 0.5 * sector.Height / 80 );
                return secPos;
            }
            else
            {
                Rectangle secPos = new Rectangle();
                secPos.Height =(int)( 0.41 * sector.Height );
                secPos.Width = sector.Width;
                secPos.X = sector.Location.X;
                secPos.Y = (int)( sector.Location.Y + 0.54 * sector.Height - beamDirection.epsilon * 0.5 * sector.Height / 80 );
                return secPos;
            }
        }

        delegate void updatePositionCallBack( CurrentState s);//

        private void updatePosition(CurrentState s) {
            if (this.InvokeRequired) {
                updatePositionCallBack d = new updatePositionCallBack(updatePosition);
                this.Invoke(d, new object[] { s });
            } else {
                this.SpotLight.Location = s.currentPosition.spotLight;
                this.SpotLight.FillColor = s.currentPosition.color;
                this.upperBeamBorder.StartPoint = s.currentPosition.upperBorder;
                this.lowerBeamBorder.StartPoint = s.currentPosition.lowerBorder;

                s.sector.Height = s.secPos.Height;
                s.sector.Width = s.secPos.Width;
                s.sector.Location = new Point(s.secPos.X,s.secPos.Y);
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
                if (visible)
                {
                    if (state.sector == this.Sector4x12)
                    {
                        this.Sector4x12.Visible = true;
                        this.Sector1x105.Visible = false;
                    }
                    else
                    {
                        this.Sector4x12.Visible = false;
                        this.Sector1x105.Visible = true;
                    }
                }
                else {
                    this.Sector4x12.Visible = false;
                    this.Sector1x105.Visible = false;
                }
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
            return parallelogramMap(realBeamPosition);
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

        private Point parallelogramMap(Point realBeamPosition)
        {
            //This implements parallelogram mapping
            Point screenBeamPosition = new Point();
            screenBeamPosition.X = (int)(sector.Location.X + sector.Width * realBeamPosition.X / 105.0);
            screenBeamPosition.Y = (int)(sector.Location.Y + 0.92 * sector.Height - sector.Height * 0.5 * realBeamPosition.Y / 80.0 - sector.Height * 0.39 * realBeamPosition.X / 105.0);

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
