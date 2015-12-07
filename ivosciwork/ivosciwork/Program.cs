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
            //This method's call enables all graphic in the application
            Application.EnableVisualStyles();

            //Make RPN works
            RPN rpn = new RPN(); //Create an instance
            Thread rpnThread = new Thread(new ThreadStart(rpn.eventLoop)); //Create a thread for RPN
            rpnThread.Start(); //Turn it on; now we can call all methods of RPN

            //Create beamForm and make it visible
            BeamForm beamForm = new BeamForm(rpn);
            beamForm.Show();
            diagramm timediagramm = new diagramm(rpn);
           timediagramm.Show();

            
            ControlForm controlForm = new ControlForm(rpn);

            Application.Run( controlForm );
        }
    }
}
