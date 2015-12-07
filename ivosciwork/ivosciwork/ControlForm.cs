using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Text;



namespace ivosciwork
{
    public partial class ControlForm : Form
    {
       private RPN myRpn;
     
        PictureBox[] Lines1 = new PictureBox[4];
        PictureBox[] Lines2 = new PictureBox[4];
        double Epsilon = 0;
        double Epsilon0 = 0;
        int maxWidth;
        int LeftZone;
        int RightZone;
        int flag;
        int x0, x;
        int MetkaLeft;
        int[] n = new int[4];
        int raz;
        int schet;

        public ControlForm(RPN rpn)
        {
            InitializeComponent();
           
            myRpn = rpn;
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
            for (int i = 0; i < 4; i++) n[i] = 1;
            timer1.Interval = myRpn.delay;
        
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e) /* зеленая кнопка ON*/
        {
           
            Epsilon = Epsilon0;
            myRpn.changeEpsilon(Epsilon);
            Segment((int)(Epsilon0 * 10), pictureBox29);

            if (timer1.Enabled == false) 
            {
                Segment((int)(Epsilon * 10), pictureBox29); 
                Segment((int)(Epsilon * 10), pictureBox28);
                schet = 1;
            }
            pictureBox3.Image = Properties.Resources.GREEN_BUTTON_DOWN;
            Epsilon = myRpn.getEpsilon();
            pictureBox12.BackColor = Color.Cyan;
            pictureBox13.BackColor = Color.Cyan;
            pictureBox14.BackColor = Color.Cyan;
            pictureBox15.BackColor = Color.Cyan;
            myRpn.turnOff();
            myRpn.turnOn();
            timer1.Enabled = true;
           
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


        private void label1_Click(object sender, EventArgs e) /* 1*105НП */
        {
            pictureBox2.Image = Properties.Resources.sector1;
            maxWidth = (int)(pictureBox8.Width *4/5-1);
            LeftZone = pictureBox8.Left;
            RightZone = LeftZone + pictureBox8.Width;
            for (int i = 0; i < 4; i++) n[i] = 1;
            Epsilon0 = 0.3;
            myRpn.changeMode(RPN.Mode.IX105NP);
            raz = 1;
            if (timer1.Enabled == true)
            {
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


        private void label2_Click(object sender, EventArgs e) /* 1*105 */
        {
            pictureBox2.Image = Properties.Resources.sector2;
            maxWidth = (int)(pictureBox8.Width *4/5-1);
            LeftZone = pictureBox8.Left;
            RightZone = LeftZone + pictureBox8.Width;
            for (int i = 0; i < 4; i++) n[i] = 1;
            myRpn.changeMode(RPN.Mode.IX105);
            raz = 1;
            if (timer1.Enabled == true)
            {
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


        private void label3_Click(object sender, EventArgs e) /* 4*12 */
        {
            pictureBox2.Image = Properties.Resources.sector3;
            maxWidth = (int)(pictureBox8.Width * 10 / 105);
            LeftZone = pictureBox8.Left + (int)(pictureBox8.Width * 31 / 70);
            RightZone = LeftZone + (int)(maxWidth*6/5);
            for (int i = 0; i < 4; i++) n[i] = 1;
            myRpn.changeMode(RPN.Mode.HX12);
            raz = 1;
            if (timer1.Enabled == true)
            {
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


        private void label4_Click(object sender, EventArgs e) /* ВЫКЛ */
        {
            pictureBox2.Image = Properties.Resources.sector4;
            myRpn.changeMode(RPN.Mode.off);
            timer1.Enabled = false;
            for (int i = 0; i < 4; i++) Lines1[i].Visible = false;
            for (int i = 0; i < 4; i++) Lines2[i].Visible = false;
            Epsilon = 0;
            myRpn.changeEpsilon(Epsilon);
            Segment((int)(Epsilon * 10), pictureBox29);
            Segment((int)(Epsilon * 10), pictureBox28);
            pictureBox3.Image = Properties.Resources.GREEN_BUTTON;
            pictureBox4.Image = Properties.Resources.GREEN_BUTTON;
            pictureBox5.Image = Properties.Resources.GREEN_BUTTON;
            pictureBox6.Image = Properties.Resources.GREEN_BUTTON;
            pictureBox7.Image = Properties.Resources.GREEN_BUTTON;
            pictureBox27.Image = Properties.Resources.GREEN_BUTTON;
            pictureBox26.Image = Properties.Resources.red_button;
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
                if ((Epsilon0 + ((double)(x0 - x) / 100) >= -10) & (Epsilon0 + ((double)(x0 - x) / 100) <= 70))
                {
                    Epsilon0 = Epsilon0 + ((double)(x0 - x) / 100);
                    Epsilon = Epsilon0;
                    
                }
                
                if (Math.Abs(x0 - x) > 10) flag = 1;
            }
            System.Threading.Thread.Sleep(1);
            Segment((int)(Epsilon0 * 10), pictureBox28);
            Segment((int)(Epsilon0 * 10), pictureBox29);
            myRpn.changeEpsilon(Epsilon);
        }


        
        private void timer1_Tick(object sender, EventArgs e)
        {
            
                HashSet<RPN.Frequency> frequencies = myRpn.getFreqSet();
                Epsilon = myRpn.getEpsilon();
                Segment((int)(Epsilon * 10), pictureBox28);
                Poloski(frequencies);
                foreach (RPN.Frequency q in frequencies)
                {
                double azimutgrad = myRpn.getAzimut().get(q);
                int azimut = (int)(azimutgrad);
                int f = (int)q;
                
                schet++;
                if (schet == 18) schet = 1;
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
                Lines1[f].SizeMode = PictureBoxSizeMode.CenterImage;
               
               
               
                    if (raz == 2) Lines2[f].Visible = true;
                    if (azimut < maxWidth)
                    {
                        Lines1[f].Width = azimut;
                        Lines1[f].Left = LeftZone;
                        Lines2[f].Width = maxWidth - azimut;
                        Lines2[f].Left = RightZone - Lines2[f].Width;
                    }
                    else
                    {
                        raz = 2;
                        Lines1[f].Width = maxWidth;
                        Lines1[f].Left = LeftZone + azimut - maxWidth;
                        Lines2[f].Width = 0;
                    }

                System.Threading.Thread.Sleep(myRpn.delay);
                }
        }
        private void pictureBox17_MouseDown(object sender, MouseEventArgs e) /* передвижение метки */
        {
            flag = 1;
        }

        private void pictureBox17_MouseUp(object sender, MouseEventArgs e)
        {
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
                if (((MetkaLeft + (int)((x - x0)/5)) >= 150) & ((MetkaLeft + pictureBox12.Width + (int)((x - x0)/5 )) <= 465))
                {
                    pictureBox12.Left = MetkaLeft;
                    pictureBox13.Left = MetkaLeft;
                    pictureBox14.Left = MetkaLeft;
                    pictureBox15.Left = MetkaLeft;
                    MetkaLeft = MetkaLeft + (int)((x - x0)/5 );
                   
                }
                if (Math.Abs((x-x0)/5)>20) flag = 1;
            }
        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e) /* зеленая кнопка F4 */
        {
            pictureBox4.Image = Properties.Resources.GREEN_BUTTON_DOWN;
            myRpn.changeFrequency(RPN.Frequency.F4);

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
            if (myRpn.getCurrentMode() != RPN.Mode.IX105NP) myRpn.changeFrequency(RPN.Frequency.F3);
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
            myRpn.changeFrequency(RPN.Frequency.F2);
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
            myRpn.changeFrequency(RPN.Frequency.F1);
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

        private void Poloski(HashSet<RPN.Frequency> frequency) /* показывает полосы которые бегут */
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
                for (int i = 0; i < 4; i++) g1.DrawImage(Properties.Resources.pusto, new Point(1 + 28 * i, 1));
            }
            else
            {
                if (eps < 0) g1.DrawImage(Properties.Resources.minus, new Point(1, 1));
                else g1.DrawImage(Properties.Resources.nol, new Point(1, 1));
                for (int i = 1; i < 4; i++)
                {
                    if ((Math.Abs(eps) / del) % 10 == 0) g1.DrawImage(Properties.Resources.nol, new Point(1 + 28 * i, 1));
                    if ((Math.Abs(eps) / del) % 10 == 1) g1.DrawImage(Properties.Resources.odin, new Point(1 + 28 * i, 1));
                    if ((Math.Abs(eps) / del) % 10 == 2) g1.DrawImage(Properties.Resources.dva, new Point(1 + 28 * i, 1));
                    if ((Math.Abs(eps) / del) % 10 == 3) g1.DrawImage(Properties.Resources.tri, new Point(1 + 28 * i, 1));
                    if ((Math.Abs(eps) / del) % 10 == 4) g1.DrawImage(Properties.Resources.chet, new Point(1 + 28 * i, 1));
                    if ((Math.Abs(eps) / del) % 10 == 5) g1.DrawImage(Properties.Resources.pyt, new Point(1 + 28 * i, 1));
                    if ((Math.Abs(eps) / del) % 10 == 6) g1.DrawImage(Properties.Resources.shest, new Point(1 + 28 * i, 1));
                    if ((Math.Abs(eps) / del) % 10 == 7) g1.DrawImage(Properties.Resources.sem, new Point(1 + 28 * i, 1));
                    if ((Math.Abs(eps) / del) % 10 == 8) g1.DrawImage(Properties.Resources.vos, new Point(1 + 28 * i, 1));
                    if ((Math.Abs(eps) / del) % 10 == 9) g1.DrawImage(Properties.Resources.dev, new Point(1 + 28 * i, 1));
                    del = del / 10;
                }
                g1.DrawImage(Properties.Resources.tochka, new Point(81, 35));
            }
        }
    }
}







