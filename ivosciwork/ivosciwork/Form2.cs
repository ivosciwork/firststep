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
    

    public partial class Form2 : Form
    {
        private RPN myRpn;
        int l = 1;
        public Form2( RPN rpn)
        {
        InitializeComponent();
            myRpn = rpn;
             
        }
        public void drem(int n,int f)
        {
            
            int  y0 = 2; int x0 = 2;
            int x = x0 + (n % 3) * 142;
            int y = y0 + (n % 6) * 36;
            pictureBox1.Location = new Point(x,y);
            pictureBox1.Width = 0;
            if (f == 0) pictureBox1.BackColor = Color.Red;
            if (f == 1) pictureBox1.BackColor = Color.Green;
            if (f == 2) pictureBox1.BackColor = Color.Blue;
            if (f == 3) pictureBox1.BackColor = Color.Yellow;
            label1.Location = new Point(x + 70, y + 16);
            label1.Text = "F" + f.ToString();
            //timer1.Enabled = true;
         
        }

        public void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.Width = (int)( l * 142 / myRpn.delay);
            l++;
            if (l == myRpn.delay)
            {
                //timer1.Enabled = false;
                l = 1;
            }
        }
    }
}
