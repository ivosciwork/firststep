using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ivosciwork
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            RPN rpn = new RPN();
            Thread rpnThread = new Thread( new ThreadStart( rpn.on ));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            RPNWorkVisualisation rpnv = new RPNWorkVisualisation();
            Form1 control = new Form1();
            control.setRPN(rpnv);
            rpnv.Show();
            Application.Run( control );
            rpnThread.Start();
        }
    }
}
