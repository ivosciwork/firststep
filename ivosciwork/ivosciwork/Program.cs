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
            BeamForm myBeamForm = new BeamForm(rpn);
            Form2 tim = new Form2(rpn);
            Form1 control = new Form1(rpn, tim);

            Thread rpnThread = new Thread( new ThreadStart( rpn.on ));

            //Application.SetCompatibleTextRenderingDefault(true);

            rpnThread.Start();

            Application.EnableVisualStyles();
            myBeamForm.Show();
            Application.Run( control );
        }
    }
}
