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
            int newPosition = matrixArea.Left + (int)(matrixArea.Width * x);
            if ( (newPosition - this.Beam.Left) < matrixArea.Width / 100.0 ) {
                this.Beam.Left = matrixArea.Left + (int)(matrixArea.Width * x);
                this.Beam.Width = this.RPN.Left - this.Beam.Left;
            }
        }

        public void changeScanningAngle(double a)
        {
            if (a > 0)
            {
                this.Beam.Top = this.Height -this.RPN.Top - (int)(matrixArea.Width * System.Math.Tan(a / System.Math.PI));
                this.Beam.Height = this.RPN.Top - this.Beam.Top;
            }
            else if (a < 0)
            {
            }
        }
    }
}
