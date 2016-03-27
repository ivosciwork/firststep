using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing.Text;



namespace ivosciwork
{
    public partial class ControlForm : Form
    {
        private RPN myRpn;
        private Thread myThread;
        PictureBox[] Lines1 = new PictureBox[4];
        PictureBox[] Lines2 = new PictureBox[4];
        double Epsilon = 0;
        double Epsilon0 = 0;
        int maxWidth, mawWidthatalon;
        int LeftZone, LeftZoneatalon;          
        int RightZone, RightZoneatalon;
        int flag;
        int x0, x;
        int MetkaLeft;
        int raz;
        int azimut;
        int widthpolosa1 = 0, widthpolosa2 = 0, leftzonpolosa1 = 142, leftzonpolosa2 = 142;
        
        int width1 = 0;
        SortedSet<RPN.Frequency> frequencies;

        private struct CurrentState {
            public PictureBox currentLine;
            public int leftZone;
            public int wight;
            public bool vis;
        }
        private CurrentState current1 = new CurrentState();
        private CurrentState current2 = new CurrentState();
        public ControlForm(RPN rpn)
        {
            InitializeComponent();

            myRpn = rpn;
            myRpn.directionChanged += this.directionChangedHandler;
            myRpn.stateChanged += this.stateChangedHandler;
            myRpn.frequencyChanged += this.frequencyChangedHandler;
            this.pictureBox12.BringToFront();
            this.pictureBox13.BringToFront();
            this.pictureBox14.BringToFront();
            this.pictureBox15.BringToFront();
            Lines1[3] = pictureBox18;
            Lines1[2] = pictureBox20;
            Lines1[1] = pictureBox22;
            Lines1[0] = pictureBox24;
            Lines2[3] = pictureBox19;
            Lines2[2] = pictureBox21;
            Lines2[1] = pictureBox23;
            Lines2[0] = pictureBox25;
            
            this.myThread = new Thread(new ThreadStart(this.EventZaloop));
            myThread.IsBackground = true;
            myThread.Start();

        }

        private void ElementLocation()
        {
            pictureBox29.Location = new Point((int)((this.Width - 14) * 509 / 637), (int)((this.Height - 36) * 58 / 401));
            pictureBox29.Size = new Size((int)((this.Width - 7) * 88 / 637), (int)((this.Height - 36) * 31 / 401));
            pictureBox28.Location = new Point((int)((this.Width - 14) * 509 / 637), (int)((this.Height - 36) * 12 / 401));
            pictureBox28.Size = new Size((int)((this.Width - 7) * 88 / 637), (int)((this.Height - 36) * 31/ 401));
            pictureBox27.Location = new Point((int)((this.Width - 14) * 509 / 637), (int)((this.Height - 36) * 328 / 401));
            pictureBox27.Size = new Size((int)((this.Width - 7) * 29 / 637), (int)((this.Height - 36) * 30 / 401));
            pictureBox26.Location = new Point((int)((this.Width - 14) * 446 / 637), (int)((this.Height - 36) * 328 / 401));
            pictureBox26.Size = new Size((int)((this.Width - 7) * 29 / 637), (int)((this.Height - 36) * 30 / 401));
            pictureBox25.Location = new Point((int)((this.Width - 14) * leftzonpolosa2 / 637), (int)((this.Height - 36) * 195 / 401));
            pictureBox25.Size = new Size((int)((this.Width - 7) * widthpolosa2 / 637), (int)((this.Height - 29) * 35 / 401));
            pictureBox24.Location = new Point((int)((this.Width - 14) * leftzonpolosa1 / 637), (int)((this.Height - 36) * 195 / 401));
            pictureBox24.Size = new Size((int)((this.Width - 7) *widthpolosa1 / 637), (int)((this.Height - 29) * 35 / 401));
            pictureBox23.Location = new Point((int)((this.Width - 14) * leftzonpolosa2 / 637), (int)((this.Height - 36) * 152 / 401));
            pictureBox23.Size = new Size((int)((this.Width - 7) *  widthpolosa2 / 637), (int)((this.Height - 29) * 35 / 401));
            pictureBox22.Location = new Point((int)((this.Width - 14) * leftzonpolosa1 / 637), (int)((this.Height - 36) * 152 / 401));
            pictureBox22.Size = new Size((int)((this.Width - 7) * widthpolosa1 / 637), (int)((this.Height - 29) * 35 / 401));
            pictureBox21.Location = new Point((int)((this.Width - 14) * leftzonpolosa2 / 637), (int)((this.Height - 36) * 109 / 401));
            pictureBox21.Size = new Size((int)((this.Width - 7) * widthpolosa2 / 637), (int)((this.Height - 29) * 35 / 401));
            pictureBox20.Location = new Point((int)((this.Width - 14) * leftzonpolosa1 / 637), (int)((this.Height - 36) * 109 / 401));
            pictureBox20.Size = new Size((int)((this.Width - 7) * widthpolosa1 / 637), (int)((this.Height - 29) * 35 / 401));
            pictureBox19.Location = new Point((int)((this.Width - 14) * leftzonpolosa2 / 637), (int)((this.Height - 36) * 66 / 401));
            pictureBox19.Size = new Size((int)((this.Width - 7) * widthpolosa2 / 637), (int)((this.Height - 29) * 35 / 401));
            pictureBox18.Location = new Point((int)((this.Width - 14) * leftzonpolosa1 / 637), (int)((this.Height - 36) * 66 / 401));
            pictureBox18.Size = new Size((int)((this.Width - 7) * widthpolosa1 / 637), (int)((this.Height - 29) * 35 / 401));
            pictureBox17.Location = new Point((int)((this.Width - 14) * 222 / 637), (int)((this.Height - 36) * 336 / 401));
            pictureBox17.Size = new Size((int)((this.Width - 7) * 105 / 637), (int)((this.Height - 36) * 22 / 401));
            pictureBox16.Location = new Point((int)((this.Width - 14) * 72 / 637), (int)((this.Height - 36) * 308 / 401));
            pictureBox16.Size = new Size((int)((this.Width - 7) * 25 / 637), (int)((this.Height - 36) * 79 / 401));
            pictureBox15.Location = new Point((int)((this.Width - 14) * 270 / 637), (int)((this.Height - 36) * 195 / 401));
            pictureBox15.Size = new Size((int)((this.Width - 7) * 5 / 637), (int)((this.Height - 36) * 35 / 401));
            pictureBox14.Location = new Point((int)((this.Width - 14) * 270 / 637), (int)((this.Height - 36) * 152 / 401));
            pictureBox14.Size = new Size((int)((this.Width - 7) * 5 / 637), (int)((this.Height - 36) * 35 / 401));
            pictureBox13.Location = new Point((int)((this.Width - 14) * 270 / 637), (int)((this.Height - 36) * 109 / 401));
            pictureBox13.Size = new Size((int)((this.Width - 7) * 5 / 637), (int)((this.Height - 36) * 35 / 401));
            pictureBox12.Location = new Point((int)((this.Width - 14) * 270 / 637), (int)((this.Height - 36) * 66 / 401));
            pictureBox12.Size = new Size((int)((this.Width - 7) * 5 / 637), (int)((this.Height - 36) * 35 / 401));
            pictureBox11.Location = new Point((int)((this.Width - 14) * 142 / 637), (int)((this.Height - 36) * 195 / 401));
            pictureBox11.Size = new Size((int)((this.Width - 7) * 262 / 637), (int)((this.Height - 29) * 35 / 401));
            pictureBox10.Location = new Point((int)((this.Width - 14) * 142 / 637), (int)((this.Height - 36) * 152 / 401));
            pictureBox10.Size = new Size((int)((this.Width - 7) * 262 / 637), (int)((this.Height - 29) * 35 / 401));
            pictureBox9.Location = new Point((int)((this.Width - 14) * 142 / 637), (int)((this.Height - 36) * 109 / 401));
            pictureBox9.Size = new Size((int)((this.Width - 7) * 262 / 637), (int)((this.Height - 29) * 35 / 401));
            pictureBox8.Location = new Point((int)((this.Width - 14) * 142 / 637), (int)((this.Height - 36) * 66 / 401));
            pictureBox8.Size = new Size((int)((this.Width - 7) * 262 / 637), (int)((this.Height - 29) * 35 / 401));
            pictureBox7.Location = new Point((int)((this.Width - 14) * 19 / 637), (int)((this.Height - 36) * 195 / 401));
            pictureBox7.Size = new Size((int)((this.Width - 7) * 29 / 637), (int)((this.Height - 36) * 30 / 401));
            pictureBox6.Location = new Point((int)((this.Width - 14) * 19 / 637), (int)((this.Height - 36) * 152 / 401));
            pictureBox6.Size = new Size((int)((this.Width - 7) * 29 / 637), (int)((this.Height - 36) * 30 / 401));
            pictureBox5.Location = new Point((int)((this.Width - 14) * 19 / 637), (int)((this.Height - 36) * 111 / 401));
            pictureBox5.Size = new Size((int)((this.Width - 7) * 29 / 637), (int)((this.Height - 36) * 30 / 401));
            pictureBox4.Location = new Point((int)((this.Width - 14) * 19 / 637), (int)((this.Height - 36) * 66 / 401));
            pictureBox4.Size = new Size((int)((this.Width - 7) * 29 / 637), (int)((this.Height - 36) * 30 / 401));
            pictureBox3.Location = new Point((int)((this.Width - 14) * 549 / 637), (int)((this.Height - 36) * 265 / 401));
            pictureBox3.Size = new Size((int)((this.Width - 7) * 29 / 637), (int)((this.Height - 36) * 30 / 401));
            pictureBox2.Location = new Point((int)((this.Width - 14) * 553 / 637), (int)((this.Height - 36) * 200 / 401));
            pictureBox2.Size = new Size((int)((this.Width - 7) * 27 / 637), (int)((this.Height - 36) * 32 / 401));
            pictureBox1.Location = new Point((int)((this.Width - 14) * 498 / 637), (int)((this.Height - 36) * 183 / 401));
            pictureBox1.Size = new Size((int)((this.Width - 7) * 40 / 637), (int)((this.Height - 36) * 20 / 401));
            pictureBox30.Location = new Point((int)((this.Width - 14) * 509 / 637), (int)((this.Height - 36) * 162 / 401));
            pictureBox30.Size = new Size((int)((this.Width - 7) * 40 / 637), (int)((this.Height - 36) * 20 / 401));
            pictureBox31.Location = new Point((int)((this.Width - 14) * 549 / 637), (int)((this.Height - 36) * 155 / 401));
            pictureBox31.Size = new Size((int)((this.Width - 7) * 40 / 637), (int)((this.Height - 36) * 20 / 401));
            pictureBox32.Location = new Point((int)((this.Width - 14) * 585 / 637), (int)((this.Height - 36) * 169 / 401));
            pictureBox32.Size = new Size((int)((this.Width - 7) * 40 / 637), (int)((this.Height - 36) * 20 / 401));
            if (myRpn.getCurrentMode() == RPN.Mode.HX12)
            {
                maxWidth = (int)(pictureBox8.Width * 10 / 105);
                LeftZone = pictureBox8.Left + (int)(pictureBox8.Width * 31 / 70);
                RightZone = LeftZone + (int)(maxWidth * 6 / 5);
            }
            if (myRpn.getCurrentMode() == RPN.Mode.IX105 | myRpn.getCurrentMode() == RPN.Mode.IX105NP )
            {
                maxWidth = (int)(pictureBox8.Width * 4 / 5 - 1);
                LeftZone = pictureBox8.Left;
                RightZone = LeftZone + pictureBox8.Width;
            }
        }
        private void ControlForm_Resize(object sender, EventArgs e)
        {
            ElementLocation();
            Segment((int)(Epsilon0 * 10), pictureBox29);
            Segment((int)(Epsilon0 * 10), pictureBox28);

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
        private RPN.ScanningDirection beamDirection = new RPN.ScanningDirection(0, 0);
        private void directionChangedHandler(RPN.CompleteRPNState currentState)
        {
            isDirectionChanged = true;
            beamDirection = currentState.currentDirection;
        }
        private void pictureBox3_MouseDown(object sender, MouseEventArgs e) /* зеленая кнопка ON*/
        {
            Epsilon = Epsilon0;
            myRpn.changeEpsilon(Epsilon);
            Segment((int)(Epsilon0 * 10), pictureBox29);

            if (myRpn.on == false)
            {
                Segment((int)(Epsilon * 10), pictureBox29);
                Segment((int)(Epsilon * 10), pictureBox28);
            }
            myRpn.on = true;
            pictureBox3.Image = Properties.Resources.GREEN_BUTTON_DOWN;
            pictureBox12.BackColor = Color.Cyan;
            pictureBox13.BackColor = Color.Cyan;
            pictureBox14.BackColor = Color.Cyan;
            pictureBox15.BackColor = Color.Cyan;
            myRpn.turnOff();
            myRpn.turnOn();
            frequencies = myRpn.getFreqSet();
            Poloski(frequencies);
        }
        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox3.Image = Properties.Resources.GREEN_BUTTON_ON;
            pictureBox4.Image = Properties.Resources.GREEN_BUTTON;
            pictureBox5.Image = Properties.Resources.GREEN_BUTTON;
            pictureBox6.Image = Properties.Resources.GREEN_BUTTON;
            pictureBox7.Image = Properties.Resources.GREEN_BUTTON;
            pictureBox27.Image = Properties.Resources.GREEN_BUTTON_ON;
            pictureBox26.Image = Properties.Resources.red_button;
        }
        private void pictureBox1_Click(object sender, EventArgs e) /* 1*105НП */
        {
            pictureBox2.Image = Properties.Resources.sector1;
            maxWidth = (int)(pictureBox8.Width * 4 / 5 - 1);
            LeftZone = pictureBox8.Left;
            RightZone = LeftZone + pictureBox8.Width;
            mawWidthatalon = 209;
            LeftZoneatalon = 142;
            RightZoneatalon = 404;
            Epsilon0 = 0.3;
            myRpn.changeEpsilon(Epsilon0);
            myRpn.changeMode(RPN.Mode.IX105NP);
            raz = 1;
            for (int i = 0; i < 4; i++) Lines1[i].Width = 0;
            for (int i = 0; i < 4; i++) Lines2[i].Width = 0;
            if (myRpn.on == true)
            {   
                raz = 1;
                frequencies = myRpn.getFreqSet();
                Poloski(frequencies);
                pictureBox4.Image = Properties.Resources.GREEN_BUTTON;
                pictureBox5.Image = Properties.Resources.GREEN_BUTTON;
                pictureBox6.Image = Properties.Resources.GREEN_BUTTON;
                pictureBox7.Image = Properties.Resources.GREEN_BUTTON;
                pictureBox27.Image = Properties.Resources.GREEN_BUTTON_ON;
                pictureBox26.Image = Properties.Resources.red_button;
                Segment((int)(Epsilon0 * 10), pictureBox28);
                Segment((int)(Epsilon0 * 10), pictureBox29);
            }
        }

        private void pictureBox30_Click(object sender, EventArgs e) /* 1*105 */
        {
            pictureBox2.Image = Properties.Resources.sector2;
            maxWidth = (int)(pictureBox8.Width * 4 / 5 - 1);
            LeftZone = pictureBox8.Left;
            RightZone = LeftZone + pictureBox8.Width;
            mawWidthatalon = 209;
            LeftZoneatalon = 142;
            RightZoneatalon = 404;
            myRpn.changeMode(RPN.Mode.IX105);
            raz = 1;
            for (int i = 0; i < 4; i++) Lines1[i].Width = 0;
            for (int i = 0; i < 4; i++) Lines2[i].Width = 0; 
            if (myRpn.on == true)
            {
                raz = 1;
                frequencies = myRpn.getFreqSet();
                Poloski(frequencies);
                Segment((int)(Epsilon0 * 10), pictureBox29);
                Epsilon = Epsilon0;
                myRpn.changeEpsilon(Epsilon);
                Segment((int)(Epsilon * 10), pictureBox28);
                pictureBox4.Image = Properties.Resources.GREEN_BUTTON;
                pictureBox5.Image = Properties.Resources.GREEN_BUTTON;
                pictureBox6.Image = Properties.Resources.GREEN_BUTTON;
                pictureBox7.Image = Properties.Resources.GREEN_BUTTON;
                pictureBox27.Image = Properties.Resources.GREEN_BUTTON_ON;
                pictureBox26.Image = Properties.Resources.red_button;
            }
        }

        private void pictureBox31_Click(object sender, EventArgs e)/* 4*12 */
        {
            pictureBox2.Image = Properties.Resources.sector3;
            maxWidth = (int)(pictureBox8.Width * 10 / 105);
            LeftZone = pictureBox8.Left + (int)(pictureBox8.Width * 31 / 70);
            RightZone = LeftZone + (int)(maxWidth * 6 / 5);
            mawWidthatalon = 25;
            LeftZoneatalon = 258;
            RightZoneatalon = 288;
            myRpn.changeMode(RPN.Mode.HX12);
            raz = 1;
            for (int i = 0; i < 4; i++) Lines1[i].Width = 0;
            for (int i = 0; i < 4; i++) Lines2[i].Width = 0;
            if (myRpn.on == true)
            {
                raz = 1;
                frequencies = myRpn.getFreqSet();
                Poloski(frequencies);
                Segment((int)(Epsilon0 * 10), pictureBox29);
                Epsilon = Epsilon0;
                myRpn.changeEpsilon(Epsilon);
                Segment((int)(Epsilon * 10), pictureBox28);
                pictureBox4.Image = Properties.Resources.GREEN_BUTTON;
                pictureBox5.Image = Properties.Resources.GREEN_BUTTON;
                pictureBox6.Image = Properties.Resources.GREEN_BUTTON;
                pictureBox7.Image = Properties.Resources.GREEN_BUTTON;
                pictureBox27.Image = Properties.Resources.GREEN_BUTTON_ON;
                pictureBox26.Image = Properties.Resources.red_button;
            }
        }

        private void pictureBox32_Click(object sender, EventArgs e)/* ВЫКЛ */
        {
            pictureBox2.Image = Properties.Resources.sector4;
            myRpn.changeMode(RPN.Mode.off);
            Epsilon0 = 0;
            myRpn.changeEpsilon(Epsilon0);
            Segment((int)(Epsilon * 10), pictureBox29);
            Segment((int)(Epsilon * 10), pictureBox28);
            pictureBox3.Image = Properties.Resources.GREEN_BUTTON;
            pictureBox4.Image = Properties.Resources.GREEN_BUTTON;
            pictureBox5.Image = Properties.Resources.GREEN_BUTTON;
            pictureBox6.Image = Properties.Resources.GREEN_BUTTON;
            pictureBox7.Image = Properties.Resources.GREEN_BUTTON;
            pictureBox27.Image = Properties.Resources.GREEN_BUTTON;
            pictureBox26.Image = Properties.Resources.red_button;
            raz = 1;
            for (int i = 0; i < 4; i++) Lines1[i].Visible = false;
            for (int i = 0; i < 4; i++) Lines2[i].Visible = false;
        }

       
        private void pictureBox16_MouseDown(object sender, MouseEventArgs e) /* задание Е0 */
        {
            if ((myRpn.getCurrentMode() != RPN.Mode.IX105NP) && (myRpn.getCurrentMode() != RPN.Mode.off))
            {
                flag = 1;
            }
        }

        private void pictureBox16_MouseUp(object sender, MouseEventArgs e)
        {
            Epsilon0 =  Epsilon0 + (double)(x0 - x) / 10;
            myRpn.changeEpsilon(Epsilon0);
            flag = 0;

        }

        private void pictureBox16_MouseMove(object sender, MouseEventArgs e)
        {
            if (flag == 1)
            {
                x0 = Cursor.Position.Y;
                flag = 2;
            }
            if (flag == 2)
            {
                x = Cursor.Position.Y;
                if ((Epsilon0 + ((double)(x0 - x) / 10) >= -10) & (Epsilon0 + ((double)(x0 - x) / 10) <= 70))
                {
                    Segment((int)((Epsilon0 + ((double)(x0 - x) / 10)) * 10), pictureBox28);
                    Segment((int)((Epsilon0 + ((double)(x0 - x) / 10)) * 10), pictureBox29);
                }
                

            }
        }

        private void pictureBox17_MouseDown(object sender, MouseEventArgs e) /* передвижение метки */
        {
            if ((myRpn.getCurrentMode() != RPN.Mode.off))
            {
                flag = 1;
            }
        }

        private void pictureBox17_MouseUp(object sender, MouseEventArgs e)
        {
            MetkaLeft = MetkaLeft + (int)((x - x0));
            flag = 0;
        }
        private void pictureBox17_MouseMove(object sender, MouseEventArgs e)
        {
            if (flag == 1)
            {
                x0 = Cursor.Position.X;
                MetkaLeft = pictureBox12.Left;
                flag = 2;
            }
            if (flag == 2)
            {
                x = Cursor.Position.X;
                
                    if (((MetkaLeft + (int)((x - x0))) >= pictureBox8.Left) & ((MetkaLeft + pictureBox12.Width + (int)((x - x0))) <= pictureBox8.Right))
                {

                    pictureBox12.Left = MetkaLeft + (int)((x - x0));
                    pictureBox13.Left = MetkaLeft + (int)((x - x0));
                    pictureBox14.Left = MetkaLeft + (int)((x - x0));
                    pictureBox15.Left = MetkaLeft + (int)((x - x0));
                }

                
            }
        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e) /* зеленая кнопка F4 */
        {
            pictureBox4.Image = Properties.Resources.GREEN_BUTTON_DOWN;
            myRpn.setFrequencies(RPN.Frequency.F4);
            frequencies = myRpn.getFreqSet();
            Poloski(frequencies);
        }

        private void pictureBox4_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox4.Image = Properties.Resources.GREEN_BUTTON_ON;
            pictureBox5.Image = Properties.Resources.GREEN_BUTTON;
            pictureBox6.Image = Properties.Resources.GREEN_BUTTON;
            pictureBox7.Image = Properties.Resources.GREEN_BUTTON;
        }

        private void pictureBox5_MouseDown(object sender, MouseEventArgs e) /* зеленая кнопка F3 */
        {
            pictureBox5.Image = Properties.Resources.GREEN_BUTTON_DOWN;
            if (myRpn.getCurrentMode() != RPN.Mode.IX105NP) myRpn.setFrequencies(RPN.Frequency.F3);
            frequencies = myRpn.getFreqSet();
            Poloski(frequencies);
        }

        private void pictureBox5_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox4.Image = Properties.Resources.GREEN_BUTTON;
            pictureBox5.Image = Properties.Resources.GREEN_BUTTON_ON;
            pictureBox6.Image = Properties.Resources.GREEN_BUTTON;
            pictureBox7.Image = Properties.Resources.GREEN_BUTTON;
        }

        private void pictureBox6_MouseDown(object sender, MouseEventArgs e) /* зеленая кнопка F2 */
        {
            pictureBox6.Image = Properties.Resources.GREEN_BUTTON_DOWN;
            myRpn.setFrequencies(RPN.Frequency.F2);
            frequencies = myRpn.getFreqSet();
            Poloski(frequencies);
        }

        private void pictureBox6_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox4.Image = Properties.Resources.GREEN_BUTTON;
            pictureBox5.Image = Properties.Resources.GREEN_BUTTON;
            pictureBox6.Image = Properties.Resources.GREEN_BUTTON_ON;
            pictureBox7.Image = Properties.Resources.GREEN_BUTTON;
        }

        private void pictureBox7_MouseDown(object sender, MouseEventArgs e) /* зеленая кнопка F1 */
        {
            pictureBox7.Image = Properties.Resources.GREEN_BUTTON_DOWN;
            myRpn.setFrequencies(RPN.Frequency.F1);
            frequencies = myRpn.getFreqSet();
            Poloski(frequencies);
        }

        private void pictureBox7_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox4.Image = Properties.Resources.GREEN_BUTTON;
            pictureBox5.Image = Properties.Resources.GREEN_BUTTON;
            pictureBox6.Image = Properties.Resources.GREEN_BUTTON;
            pictureBox7.Image = Properties.Resources.GREEN_BUTTON_ON;
        }

        

        private void pictureBox26_MouseDown(object sender, MouseEventArgs e) /*красная кнопка СТОП ЕПСИЛОН*/
        {
            pictureBox26.Image = Properties.Resources.red_button_down;
            myRpn.OnStopButtonState();
        }

        private void pictureBox26_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox26.Image = Properties.Resources.red_button_on;
            pictureBox27.Image = Properties.Resources.GREEN_BUTTON;
        }

        private void pictureBox27_MouseDown(object sender, MouseEventArgs e) /*зеленая кнопка ПУСК ЕПСИЛОН*/
        {
            pictureBox27.Image = Properties.Resources.GREEN_BUTTON_DOWN;
            myRpn.OffStopButtonState();
        }
        private void pictureBox27_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox27.Image = Properties.Resources.GREEN_BUTTON_ON;
            pictureBox26.Image = Properties.Resources.red_button;
        }

        private void Poloski(SortedSet<RPN.Frequency> frequency) /* показывает полосы которые бегут */
        {
           for (int i = 0; i < 4; i++) Lines1[i].Visible = false;
             for (int i = 0; i < 4; i++) Lines2[i].Visible = false;
            foreach (int f in frequency)
             {   
                 if (f == 0) Lines1[0].Visible = true;
                 if (f == 1) Lines1[1].Visible = true;
                 if (f == 2) Lines1[2].Visible = true;
                 if (f == 3) Lines1[3].Visible = true;

             }
        }

       

        private void Segment (int eps, PictureBox pic) /* вывод эпсилон на экран */ 
        {
            int del = 100;
            Graphics g1 = pic.CreateGraphics();
            if (myRpn.getCurrentMode() == RPN.Mode.off)
            {
                for (int i = 0; i < 4; i++) g1.DrawImage(Properties.Resources.pusto, (int)(pic.Width/4 * i), 0,(int)(pic.Width/4),pic.Height);
            }
            else
            {
                if (eps < 0) g1.DrawImage(Properties.Resources.minus, 0, 0, (int)(pic.Width / 4), pic.Height);
                else g1.DrawImage(Properties.Resources.nol, 0, 0, (int)(pic.Width / 4), pic.Height);
                for (int i = 1; i < 4; i++)
                {
                    if ((Math.Abs(eps) / del) % 10 == 0) g1.DrawImage(Properties.Resources.nol, (int)(pic.Width / 4 * i), 0, (int)(pic.Width / 4), pic.Height);
                    if ((Math.Abs(eps) / del) % 10 == 1) g1.DrawImage(Properties.Resources.odin, (int)(pic.Width / 4 * i), 0, (int)(pic.Width / 4), pic.Height);
                    if ((Math.Abs(eps) / del) % 10 == 2) g1.DrawImage(Properties.Resources.dva, (int)(pic.Width / 4 * i), 0, (int)(pic.Width / 4), pic.Height);
                    if ((Math.Abs(eps) / del) % 10 == 3) g1.DrawImage(Properties.Resources.tri, (int)(pic.Width / 4 * i), 0, (int)(pic.Width / 4), pic.Height);
                    if ((Math.Abs(eps) / del) % 10 == 4) g1.DrawImage(Properties.Resources.chet, (int)(pic.Width / 4 * i), 0, (int)(pic.Width / 4), pic.Height);
                    if ((Math.Abs(eps) / del) % 10 == 5) g1.DrawImage(Properties.Resources.pyt, (int)(pic.Width / 4 * i), 0, (int)(pic.Width / 4), pic.Height);
                    if ((Math.Abs(eps) / del) % 10 == 6) g1.DrawImage(Properties.Resources.shest, (int)(pic.Width / 4 * i), 0, (int)(pic.Width / 4), pic.Height);
                    if ((Math.Abs(eps) / del) % 10 == 7) g1.DrawImage(Properties.Resources.sem, (int)(pic.Width / 4 * i), 0, (int)(pic.Width / 4), pic.Height);
                    if ((Math.Abs(eps) / del) % 10 == 8) g1.DrawImage(Properties.Resources.vos, (int)(pic.Width / 4 * i), 0, (int)(pic.Width / 4), pic.Height);
                    if ((Math.Abs(eps) / del) % 10 == 9) g1.DrawImage(Properties.Resources.dev, (int)(pic.Width / 4 * i), 0, (int)(pic.Width / 4), pic.Height);
                    del = del / 10;
                }
                g1.DrawImage(Properties.Resources.tochka, (int)(pic.Width *3/4-pic.Width/64), (int)(pic.Height*7/8), (int)(pic.Width / 32), (int)(pic.Height / 8));
            }
        }
        private void EventZaloop()
        {
            while (true)
            { 
                double azimutgrad;
                if (isStateChanged)
                {
                    isStateChanged = false;
                
                }

                if (isDirectionChanged || isFrequencyChanged)
                {
                    isDirectionChanged = false;
                    isFrequencyChanged = false;

                    Epsilon = beamDirection.epsilon;
                    Segment((int)(Epsilon * 10), pictureBox28);
                    Segment((int)(Epsilon0 * 10), pictureBox29);
                    azimutgrad = beamDirection.azimut;
                    double azimutatolon = 262 * azimutgrad / 105;
                    azimut = (int)((this.Width - 7) * azimutatolon / 637);
                    int f = (int)frequency;
                    current1.currentLine = Lines1[f];
                    current2.currentLine = Lines2[f];
                    current1.vis = true;
                    if (raz == 2) current2.vis = true;
                    else current2.vis = false;
                   /* 
                   if (myRpn.getCurrentMode() == RPN.Mode.HX12)
                    {
                        Lines1[f].Image = Properties.Resources.polosa12;
                        Lines2[f].Image = Properties.Resources.polosa12;
                    }
                    else
                    {
                        Lines1[f].Image = Properties.Resources.polosa105;
                        Lines2[f].Image = Properties.Resources.polosa105;
                    }
                    */
                   // Lines1[f].SizeMode = PictureBoxSizeMode.CenterImage;
                    if (azimut < maxWidth)
                    {
                        
                        current1.leftZone = LeftZone;
                        current1.wight = azimut;
                        current2.wight = maxWidth - azimut;
                        current2.leftZone = RightZone - current2.wight;
                        leftzonpolosa1 = LeftZoneatalon;
                        widthpolosa1 = (int)azimutatolon;
                        widthpolosa2 = mawWidthatalon - (int)azimutatolon;
                        leftzonpolosa2 = RightZoneatalon - widthpolosa2;
                        
                    }
                    else
                    {
                        raz = 2;
                        current1.wight= maxWidth;
                        current1.leftZone = LeftZone + azimut - maxWidth;
                        current2.wight = 0;
                        leftzonpolosa1 = LeftZoneatalon + (int)azimutatolon - mawWidthatalon;
                        widthpolosa1 = mawWidthatalon;
                        widthpolosa2 = 0;
                    }
                    azimut = current1.leftZone;
                    width1 = current1.wight;

                    updateState(current1, current2);
                    
                }
            }
        }

       delegate void updateStateCallBack(CurrentState currentPosition1, CurrentState currentPosition2);//

       

        private void updateState(CurrentState current1, CurrentState current2)
        {
            if (this.InvokeRequired)
            {
                updateStateCallBack d = new updateStateCallBack(updateState);
                this.Invoke(d, new object[] { current1,current2 });
            }
            else
            {
                current2.currentLine.Visible = current2.vis;
                Graphics g1 = current1.currentLine.CreateGraphics();
                if (current1.wight <= (int)(maxWidth / 15)) g1.DrawImage(Properties.Resources.polright, 0, 0, current1.wight, current1.currentLine.Height);
                else
                {
                    g1.DrawImage(Properties.Resources.polright, (current1.wight - (int)(maxWidth / 10) + 1), 0, (int)(maxWidth / 15), current1.currentLine.Height);
                    g1.DrawImage(Properties.Resources.polleft, 0, 0, (current1.wight - (int)(maxWidth / 15)), current1.currentLine.Height);
                }
                g1 = current2.currentLine.CreateGraphics();
                if (current2.wight <= (int)(maxWidth * 14 / 15)) g1.DrawImage(Properties.Resources.polleft, 0, 0, current2.wight + 1, current2.currentLine.Height);
                else
                {
                    g1.DrawImage(Properties.Resources.polright, (int)(maxWidth * 14 / 15) + 1, 0, current2.wight - (int)(maxWidth *14 / 15), current2.currentLine.Height);
                    g1.DrawImage(Properties.Resources.polleft, 0, 0, (int)(maxWidth * 14 / 15), current2.currentLine.Height);
                }
                current1.currentLine.Left = current1.leftZone;
                current1.currentLine.Width = current1.wight;
                current1.currentLine.Visible = current1.vis;
                current2.currentLine.Width = current2.wight;
                current2.currentLine.Left = current2.leftZone;
                

            }
            
        }
       
        
    }
}








