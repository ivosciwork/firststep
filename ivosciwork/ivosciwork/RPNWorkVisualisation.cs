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
    public partial class RPNWorkVisualisation : Form
    {

        public RPNWorkVisualisation()
        {
            InitializeComponent();
        }

        public void changeScanningDirection(double x)
        {
            this.Beam.Left = matrixArea.Left + (int) (matrixArea.Width * x);
            this.Beam.Width = this.RPN.Left - this.Beam.Left;
        }
    }
}
