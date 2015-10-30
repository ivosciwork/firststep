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
            if ( (newPosition - this.SpotLight.Left) < matrixArea.Width / 100.0 ) {
                this.SpotLight.Left = matrixArea.Left + (int)(matrixArea.Width * x);
                int r = this.SpotLight.Width / 2;
                int a = this.upperBeamBorder.X2 - this.upperBeamBorder.X1;
                int b = this.upperBeamBorder.Y2 - this.upperBeamBorder.Y1;
                int c = (int)System.Math.Sqrt( System.Math.Pow(a,2) + System.Math.Pow(b,2) );
                this.upperBeamBorder.X1 = this.SpotLight.Left + (int)(r * (1 + (double) b / c ));
                this.upperBeamBorder.Y1 = this.SpotLight.Top + (int)(r * (1 - (double) a / c));
                this.lowerBeamBorder.X1 = this.SpotLight.Left + (int)(r * (1 - (double) b / c));
                this.lowerBeamBorder.Y1 = this.SpotLight.Top + (int)(r * (1 + (double) a / c));
            }
        }

        public void changeScanningAngle(double a)
        {
            this.SpotLight.Top = this.Height - this.RPN.Top - (int)(matrixArea.Width * System.Math.Tan(a / System.Math.PI));
        }
    }
}
