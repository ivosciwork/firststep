using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ivosciwork
{
    public partial class Form1 : Form
    {
        double E = -10; /* текущее Е */
        double E0 = -10; /* начальное Е */
        int n = 0; /* номер режима*/
        int f = 0; /* зажатой кнопки*/
        int x0, x; /* координата мыши*/
        int flag = 0; 
        int rl = 306;
        int rr = 311;
        int r0 = 5;
        int[] l = new int[4];
        int[] m = new int[4];
        int[] q = new int[4];
        int[] array = new int[4];
        int[] array1 = new int[4];
        int y = 0;
        


        public Form1()
        {
            InitializeComponent();
            label6.ForeColor = Color.LightGreen;
            label7.ForeColor = Color.LightGreen;
            label6.Font = new Font("Arial", 30);
            label7.Font = new Font("Arial", 30);
            label6.Text = Convert.ToString(Math.Round(E, 1));
            label7.Text = Convert.ToString(Math.Round(E0, 1));
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e) /* зеленая кнопка */
        {
            pictureBox3.Image = Properties.Resources.GREEN_BUTTON_DOWN;
            f = 0;
            E = E0;
            label6.Text = Convert.ToString(Math.Round(E, 1));
            if ((n == 1 | n == 2) & timer1.Enabled == true) timer1.Enabled = false;
            if ((n == 1 | n == 2) & timer1.Enabled == false) timer1.Enabled = true;
            if (n == 3 & timer2.Enabled == true) timer2.Enabled = false;
            if (n == 3 & timer2.Enabled == false) timer2.Enabled = true;
            for (int i=0; i<=3; i++) array[i] = 0;
            for (int i=0; i<=3; i++) array1[i] = 0;
            for (int i = 0; i <= 3; i++) q[i] = 0;
            for (int i = 0; i <= 3; i++)
            {
                if (n == 1 | n == 2)
                {
                    l[i] = 150;
                    pictureBox18.Left = 150;
                    pictureBox19.Left = 150;
                    pictureBox20.Left = 150;
                    pictureBox21.Left = 150;
                    pictureBox22.Left = 150;
                    pictureBox23.Left = 150;
                    pictureBox24.Left = 150;
                    pictureBox25.Left = 150;
                    m[i] = 150;
                }
                if (n == 3)
                {
                    l[i] = 265;
                    pictureBox18.Left = 265;
                    pictureBox19.Left = 265;
                    pictureBox20.Left = 265;
                    pictureBox21.Left = 265;
                    pictureBox22.Left = 265;
                    pictureBox23.Left = 265;
                    pictureBox24.Left = 265;
                    pictureBox25.Left = 265;
                    m[i] = 265;
                }
            }
            if (timer1.Enabled == false | timer2.Enabled == false)
            {
                pictureBox18.BackColor = Color.MidnightBlue;
                pictureBox19.BackColor = Color.MidnightBlue;
                pictureBox20.BackColor = Color.MidnightBlue;
                pictureBox21.BackColor = Color.MidnightBlue;
                pictureBox22.BackColor = Color.MidnightBlue;
                pictureBox23.BackColor = Color.MidnightBlue;
                pictureBox24.BackColor = Color.MidnightBlue;
                pictureBox25.BackColor = Color.MidnightBlue;
            }
            if (timer1.Enabled == true | timer2.Enabled == true)
            {
                pictureBox18.BackColor = Color.Green;
                pictureBox19.BackColor = Color.Green;
                pictureBox20.BackColor = Color.Green;
                pictureBox21.BackColor = Color.Green;
                pictureBox22.BackColor = Color.Green;
                pictureBox23.BackColor = Color.Green;
                pictureBox24.BackColor = Color.Green;
                pictureBox25.BackColor = Color.Green;
            }
        }
        
        private void pictureBox3_MouseUp(object sender, MouseEventArgs e) 
        {
            pictureBox3.Image = Properties.Resources.GREEN_BUTTON;
        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e) /* зеленая кнопка */
        {
            pictureBox4.Image = Properties.Resources.GREEN_BUTTON_DOWN;
            f = 4;
            pictureBox18.BackColor = Color.Green;
            pictureBox19.BackColor = Color.Green;
            pictureBox20.BackColor = Color.MidnightBlue;
            pictureBox21.BackColor = Color.MidnightBlue;
            pictureBox22.BackColor = Color.MidnightBlue;
            pictureBox23.BackColor = Color.MidnightBlue;
            pictureBox24.BackColor = Color.MidnightBlue;
            pictureBox25.BackColor = Color.MidnightBlue;
         }

        private void pictureBox4_MouseUp(object sender, MouseEventArgs e) 
        {
            pictureBox4.Image = Properties.Resources.GREEN_BUTTON;
        }

        private void pictureBox5_MouseDown(object sender, MouseEventArgs e) /* зеленая кнопка */
        {
            pictureBox5.Image = Properties.Resources.GREEN_BUTTON_DOWN;
            f = 3;
            pictureBox18.BackColor = Color.MidnightBlue;
            pictureBox19.BackColor = Color.MidnightBlue;
            pictureBox20.BackColor = Color.Green;
            pictureBox21.BackColor = Color.Green;
            pictureBox22.BackColor = Color.MidnightBlue;
            pictureBox23.BackColor = Color.MidnightBlue;
            pictureBox24.BackColor = Color.MidnightBlue;
            pictureBox25.BackColor = Color.MidnightBlue;
        }

        private void pictureBox5_MouseUp(object sender, MouseEventArgs e) 
        {
            pictureBox5.Image = Properties.Resources.GREEN_BUTTON;
        }

        private void pictureBox6_MouseDown(object sender, MouseEventArgs e) /* зеленая кнопка */
        {
            pictureBox6.Image = Properties.Resources.GREEN_BUTTON_DOWN;
            f = 2;
            pictureBox18.BackColor = Color.MidnightBlue;
            pictureBox19.BackColor = Color.MidnightBlue;
            pictureBox20.BackColor = Color.MidnightBlue;
            pictureBox21.BackColor = Color.MidnightBlue;
            pictureBox22.BackColor = Color.Green;
            pictureBox23.BackColor = Color.Green;
            pictureBox24.BackColor = Color.MidnightBlue;
            pictureBox25.BackColor = Color.MidnightBlue;
        }

        private void pictureBox6_MouseUp(object sender, MouseEventArgs e) 
        {
            pictureBox6.Image = Properties.Resources.GREEN_BUTTON;
        }

        private void pictureBox7_MouseDown(object sender, MouseEventArgs e) /* зеленая кнопка */
        {
            pictureBox7.Image = Properties.Resources.GREEN_BUTTON_DOWN;
            f = 1;
            pictureBox18.BackColor = Color.MidnightBlue;
            pictureBox19.BackColor = Color.MidnightBlue;
            pictureBox20.BackColor = Color.MidnightBlue;
            pictureBox21.BackColor = Color.MidnightBlue;
            pictureBox22.BackColor = Color.MidnightBlue;
            pictureBox23.BackColor = Color.MidnightBlue;
            pictureBox24.BackColor = Color.Green;
            pictureBox25.BackColor = Color.Green;
        }

        private void pictureBox7_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox7.Image = Properties.Resources.GREEN_BUTTON;
        }

        private void label1_Click(object sender, EventArgs e) /* 1*105НП */
        {
            pictureBox2.Image = Properties.Resources.sector1;
            f = 0;
            n = 1;
            E0 = 0.3;
            label7.Text = Convert.ToString(Math.Round(E0, 1));
            pictureBox12.BackColor = Color.Cyan;
            pictureBox13.BackColor = Color.Cyan;
            pictureBox14.BackColor = Color.Cyan;
            pictureBox15.BackColor = Color.Cyan;
            timer1.Enabled = false;
            timer2.Enabled = false;
            pictureBox18.BackColor = Color.MidnightBlue;
            pictureBox19.BackColor = Color.MidnightBlue;
            pictureBox18.Left = 150;
            pictureBox19.Left = 150;
         }
        
        private void label2_Click(object sender, EventArgs e) /* 1*105 */
        {
            pictureBox2.Image = Properties.Resources.sector2;
            f = 0;
            n = 2;
            pictureBox12.BackColor = Color.Cyan;
            pictureBox13.BackColor = Color.Cyan;
            pictureBox14.BackColor = Color.Cyan;
            pictureBox15.BackColor = Color.Cyan;
            timer1.Enabled = false;
            timer2.Enabled = false;
            pictureBox18.BackColor = Color.MidnightBlue;
            pictureBox19.BackColor = Color.MidnightBlue;
            pictureBox18.Left = 150;
            pictureBox19.Left = 150;
        }

        private void label3_Click(object sender, EventArgs e) /* 4*12 */
        {
            pictureBox2.Image = Properties.Resources.sector3;
            f = 0;
            n = 3;
            pictureBox12.BackColor = Color.Cyan;
            pictureBox13.BackColor = Color.Cyan;
            pictureBox14.BackColor = Color.Cyan;
            pictureBox15.BackColor = Color.Cyan;
            timer1.Enabled = false;
            timer2.Enabled = false;
            pictureBox18.BackColor = Color.MidnightBlue;
            pictureBox19.BackColor = Color.MidnightBlue;
            pictureBox18.Left = 255;
            pictureBox19.Left = 255;
        }
       
        private void label4_Click(object sender, EventArgs e) /* ВЫКЛ */
        {
            pictureBox2.Image = Properties.Resources.sector4;
            f = 0;
            n = 0;
            pictureBox12.BackColor = Color.MidnightBlue;
            pictureBox13.BackColor = Color.MidnightBlue;
            pictureBox14.BackColor = Color.MidnightBlue;
            pictureBox15.BackColor = Color.MidnightBlue;
            pictureBox12.Left = 306;
            pictureBox13.Left = 306;
            pictureBox14.Left = 306;
            pictureBox15.Left = 306;
            rl = 306;
            rr = 311;
            r0 = 5;
            timer1.Enabled = false;
            timer2.Enabled = false;

        }
        
        private void pictureBox16_MouseDown(object sender, MouseEventArgs e) /* задание Е0 */
        {
            flag = 1;
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
            if (flag ==2)
            {
                x = Cursor.Position.Y;
                if ((E0 + ((double)(x0 - x) / 20) >= -10) & (E0 + ((double)(x0 - x) / 20) <= 70))
                {
                    E0 = E0 + ((double)(x0 - x) / 20);
                    label7.Text = Convert.ToString(Math.Round(E0, 1));
                }
                if ( Math.Abs(x0-x) > 10)  flag = 1;
            }


        }

        private void pictureBox17_MouseDown(object sender, MouseEventArgs e) /* передвижение метки */
        {
            flag = 1;
        }

        private void pictureBox17_MouseUp(object sender, MouseEventArgs e)
        {
            flag = 0;
            r0 = 5; 
            pictureBox12.Width = r0;
            pictureBox13.Width = r0;
            pictureBox14.Width = r0;
            pictureBox15.Width = r0;
        }

        private void pictureBox17_MouseMove(object sender, MouseEventArgs e)
        {
            if (flag == 1)
            {
                x0 = Cursor.Position.X;
                flag = 2;
                rl = pictureBox12.Left;
                rr = pictureBox12.Right;
            }
            if (flag ==2)
            {
                x = Cursor.Position.X;
                if (Math.Abs(x0 - x) > 2) flag = 1;
                if (((rl + (x - x0)) >= 150) & ((rr + (x - x0)) <= 465))
                {
                    pictureBox12.Left = rl;
                    pictureBox13.Left = rl;
                    pictureBox14.Left = rl;
                    pictureBox15.Left = rl;
                    rl = rl + (x - x0);
                    rr = rl + r0;
                    if  ((r0 <= 30) & (r0>=5) ) 
                        {
                            r0 = r0 + Math.Abs(x0 - x) ;
                            pictureBox12.Width = r0;
                            pictureBox13.Width = r0;
                            pictureBox14.Width = r0;
                            pictureBox15.Width = r0;
                          
                        }
                  } 
              }
          }

        private void timer1_Tick(object sender, EventArgs e) /* движение режима 1*105(НП) */
        {

            if ((f == 0 & y == 3) | f == 4)
            {
                if (array[y] < 105 & (q[y] == 0 | q[y] == 2))
                {
                    array[y] += 1;
                    pictureBox18.Width = array[y];
                    if (q[y] == 2)
                    {
                        array1[y] -= 1;
                        pictureBox19.Width = array1[y];
                        pictureBox19.Left = m[y];
                        m[y] += 1;
                    }
                }
                else if (l[y] <= 360 & (q[y] == 0 | q[y] == 2) & array[y] == 105)
                {
                    pictureBox18.Left = l[y];
                    l[y] += 1;
                }
                else if (array1[y] < 105 & q[y] == 1)
                {
                    array1[y] += 1;
                    pictureBox19.Width = array1[y];
                    array[y] -= 1;
                    pictureBox18.Width = array[y];
                    pictureBox18.Left = l[y];
                    l[y] += 1;

                }
                else if (m[y] <= 360 & q[y] == 1 & array1[y] == 105)
                {
                    pictureBox19.Left = m[y];
                    m[y] += 1;
                }
                if (l[y] == 360)
                {
                    q[y] = 1;
                    m[y] = 150;
                    pictureBox19.Left = m[y];
                }
                else if (m[y] == 360)
                {
                    q[y] = 2;
                    l[y] = 150;
                    pictureBox18.Left = l[y];
                }
            }


            if ((f == 0 & y == 2) | f == 3)
            {
                if (array[y] < 105 & (q[y] == 0 | q[y] == 2))
                {
                    array[y] += 1;
                    pictureBox20.Width = array[y];
                    if (q[y] == 2)
                    {
                        array1[y] -= 1;
                        pictureBox21.Width = array1[y];
                        pictureBox21.Left = m[y];
                        m[y] += 1;
                    }
                }
                else if (l[y] <= 360 & (q[y] == 0 | q[y] == 2) & array[y] == 105)
                {
                    pictureBox20.Left = l[y];
                    l[y] += 1;
                }
                else if (array1[y] < 105 & q[y] == 1)
                {
                    array1[y] += 1;
                    pictureBox21.Width = array1[y];
                    array[y] -= 1;
                    pictureBox20.Width = array[y];
                    pictureBox20.Left = l[y];
                    l[y] += 1;

                }
                else if (m[y] <= 360 & q[y] == 1 & array1[y] == 105)
                {
                    pictureBox21.Left = m[y];
                    m[y] += 1;
                }
                if (l[y] == 360)
                {
                    q[y] = 1;
                    m[y] = 150;
                    pictureBox21.Left = m[y];
                }
                else if (m[y] == 360)
                {
                    q[y] = 2;
                    l[y] = 150;
                    pictureBox20.Left = l[y];
                 }
            }

            if ((f == 0 & y == 1) | f == 2)
            {
                if (array[y] < 105 & (q[y] == 0 | q[y] == 2))
                {
                    array[y] += 1;
                    pictureBox22.Width = array[y];
                    if (q[y] == 2)
                    {
                        array1[y] -= 1;
                        pictureBox23.Width = array1[y];
                        pictureBox23.Left = m[y];
                        m[y] += 1;
                    }
                }
                else if (l[y] <= 360 & (q[y] == 0 | q[y] == 2) & array[y] == 105)
                {
                    pictureBox22.Left = l[y];
                    l[y] += 1;
                }
                else if (array1[y] < 105 & q[y] == 1)
                {
                    array1[y] += 1;
                    pictureBox23.Width = array1[y];
                    array[y] -= 1;
                    pictureBox22.Width = array[y];
                    pictureBox22.Left = l[y];
                    l[y] += 1;

                }
                else if (m[y] <= 360 & q[y] == 1 & array1[y] == 105)
                {
                    pictureBox23.Left = m[y];
                    m[y] += 1;
                }
                if (l[y] == 360)
                {
                    q[y] = 1;
                    m[y] = 150;
                   pictureBox23.Left = m[y];
                 }
                else if (m[y] == 360)
                {
                    q[y] = 2;
                    l[y] = 150;
                    pictureBox22.Left = l[y];
                }
            }

            if ((f == 0 & y == 0) | f == 1)
            {
                if (array[y] < 105 & (q[y] == 0 | q[y] == 2))
                {
                    array[y] += 1;
                    pictureBox24.Width = array[y];
                    if (q[y] == 2)
                    {
                        array1[y] -= 1;
                        pictureBox25.Width = array1[y];
                        pictureBox25.Left = m[y];
                        m[y] += 1;
                    }
                }
                else if (l[y] <= 360 & (q[y] == 0 | q[y] == 2) & array[y] == 105)
                {
                    pictureBox24.Left = l[y];
                    l[y] += 1;
                }
                else if (array1[y] < 105 & q[y] == 1)
                {
                    array1[y] += 1;
                    pictureBox25.Width = array1[y];
                    array[y] -= 1;
                    pictureBox24.Width = array[y];
                    pictureBox24.Left = l[y];
                    l[y] += 1;

                }
                else if (m[y] <= 360 & q[y] == 1 & array1[y] == 105)
                {
                    pictureBox25.Left = m[y];
                    m[y] += 1;
                }
                if (l[y] == 360)
                {
                    q[y] = 1;
                    m[y] = 150;
                    pictureBox25.Left = m[y];
                }
                else if (m[y] == 360)
                {
                    q[y] = 2;
                    l[y] = 150;
                    pictureBox24.Left = l[y];
                }

            }
            if (y < 3) y += 1;
            else y = 0;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if ((f == 0 & y == 3) | f == 4)
            {
                if (array[y] < 35 & (q[y] == 0 | q[y] == 2))
                {
                    array[y] += 1;
                    pictureBox18.Width = array[y];
                    if (q[y] == 2)
                    {
                        array1[y] -= 1;
                        pictureBox19.Width = array1[y];
                        pictureBox19.Left = m[y];
                        m[y] += 1;
                    }
                }
                else if (l[y] <= 335 & (q[y] == 0 | q[y] == 2) & array[y] == 35)
                {
                    pictureBox18.Left = l[y];
                    l[y] += 1;
                }
                else if (array1[y] < 35 & q[y] == 1)
                {
                    array1[y] += 1;
                    pictureBox19.Width = array1[y];
                    array[y] -= 1;
                    pictureBox18.Width = array[y];
                    pictureBox18.Left = l[y];
                    l[y] += 1;

                }
                else if (m[y] <= 335 & q[y] == 1 & array1[y] == 35)
                {
                    pictureBox19.Left = m[y];
                    m[y] += 1;
                }
                if (l[y] == 335)
                {
                    q[y] = 1;
                    m[y] = 265;
                    pictureBox19.Left = m[y];
                }
                else if (m[y] == 335)
                {
                    q[y] = 2;
                    l[y] = 265;
                    pictureBox18.Left = l[y];
                }
            }


            if ((f == 0 & y == 2) | f == 3)
            {
                if (array[y] < 35 & (q[y] == 0 | q[y] == 2))
                {
                    array[y] += 1;
                    pictureBox20.Width = array[y];
                    if (q[y] == 2)
                    {
                        array1[y] -= 1;
                        pictureBox21.Width = array1[y];
                        pictureBox21.Left = m[y];
                        m[y] += 1;
                    }
                }
                else if (l[y] <= 335 & (q[y] == 0 | q[y] == 2) & array[y] == 35)
                {
                    pictureBox20.Left = l[y];
                    l[y] += 1;
                }
                else if (array1[y] < 35 & q[y] == 1)
                {
                    array1[y] += 1;
                    pictureBox21.Width = array1[y];
                    array[y] -= 1;
                    pictureBox20.Width = array[y];
                    pictureBox20.Left = l[y];
                    l[y] += 1;

                }
                else if (m[y] <= 335 & q[y] == 1 & array1[y] == 35)
                {
                    pictureBox21.Left = m[y];
                    m[y] += 1;
                }
                if (l[y] == 335)
                {
                    q[y] = 1;
                    m[y] = 265;
                    pictureBox21.Left = m[y];
                }
                else if (m[y] == 335)
                {
                    q[y] = 2;
                    l[y] = 265;
                    pictureBox20.Left = l[y];
                }
            }

            if ((f == 0 & y == 1) | f == 2)
            {
                if (array[y] < 35 & (q[y] == 0 | q[y] == 2))
                {
                    array[y] += 1;
                    pictureBox22.Width = array[y];
                    if (q[y] == 2)
                    {
                        array1[y] -= 1;
                        pictureBox23.Width = array1[y];
                        pictureBox23.Left = m[y];
                        m[y] += 1;
                    }
                }
                else if (l[y] <= 335 & (q[y] == 0 | q[y] == 2) & array[y] == 35)
                {
                    pictureBox22.Left = l[y];
                    l[y] += 1;
                }
                else if (array1[y] < 35 & q[y] == 1)
                {
                    array1[y] += 1;
                    pictureBox23.Width = array1[y];
                    array[y] -= 1;
                    pictureBox22.Width = array[y];
                    pictureBox22.Left = l[y];
                    l[y] += 1;

                }
                else if (m[y] <= 335 & q[y] == 1 & array1[y] == 35)
                {
                    pictureBox23.Left = m[y];
                    m[y] += 1;
                }
                if (l[y] == 335)
                {
                    q[y] = 1;
                    m[y] = 265;
                    pictureBox23.Left = m[y];
                }
                else if (m[y] == 335)
                {
                    q[y] = 2;
                    l[y] = 265;
                    pictureBox22.Left = l[y];
                }
            }

            if ((f == 0 & y == 0) | f == 1)
            {
                if (array[y] < 35 & (q[y] == 0 | q[y] == 2))
                {
                    array[y] += 1;
                    pictureBox24.Width = array[y];
                    if (q[y] == 2)
                    {
                        array1[y] -= 1;
                        pictureBox25.Width = array1[y];
                        pictureBox25.Left = m[y];
                        m[y] += 1;
                    }
                }
                else if (l[y] <= 335 & (q[y] == 0 | q[y] == 2) & array[y] == 35)
                {
                    pictureBox24.Left = l[y];
                    l[y] += 1;
                }
                else if (array1[y] < 35 & q[y] == 1)
                {
                    array1[y] += 1;
                    pictureBox25.Width = array1[y];
                    array[y] -= 1;
                    pictureBox24.Width = array[y];
                    pictureBox24.Left = l[y];
                    l[y] += 1;

                }
                else if (m[y] <= 335 & q[y] == 1 & array1[y] == 35)
                {
                    pictureBox25.Left = m[y];
                    m[y] += 1;
                }
                if (l[y] == 335)
                {
                    q[y] = 1;
                    m[y] = 265;
                    pictureBox25.Left = m[y];
                }
                else if (m[y] == 335)
                {
                    q[y] = 2;
                    l[y] = 265;
                    pictureBox24.Left = l[y];
                }

            }
            if (y < 3) y += 1;
            else y = 0;
            if (f != 0)
            {
                if (m[f - 1] == 336 | l[f - 1] == 336)
                {
                    if (E - E0 > 3.8) E = E0;
                    else E += 0.33;
                    label6.Text = Convert.ToString(Math.Round(E, 1));
                }
            }
            if (f == 0 & (m[3] == 336 | l[3] == 336))
            {
                if (E - E0 > 3.8) E = E0;
                else E += 0.33;
                label6.Text = Convert.ToString(Math.Round(E, 1));
            }
       }
   
    }
}