using System;
using System.Collections.Generic;
using System.Linq;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            RPNWorkVisualisation rpn = new RPNWorkVisualisation();
            Form1 control = new Form1();
            control.setRPN(rpn);
            rpn.Show();
            Application.Run( control );
        }
    }
}
