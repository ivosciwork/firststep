namespace ivosciwork
{
    partial class RPNWorkVisualisation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SpotLight = new System.Windows.Forms.PictureBox();
            this.RPN = new System.Windows.Forms.PictureBox();
            this.matrixArea = new System.Windows.Forms.PictureBox();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.upperBeamBorder = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lowerBeamBorder = new Microsoft.VisualBasic.PowerPacks.LineShape();
            ((System.ComponentModel.ISupportInitialize)(this.SpotLight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RPN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.matrixArea)).BeginInit();
            this.SuspendLayout();
            // 
            // SpotLight
            // 
            this.SpotLight.BackColor = System.Drawing.Color.Transparent;
            this.SpotLight.BackgroundImage = global::ivosciwork.Properties.Resources.circle;
            this.SpotLight.Image = global::ivosciwork.Properties.Resources.circle;
            this.SpotLight.Location = new System.Drawing.Point(34, 40);
            this.SpotLight.Name = "SpotLight";
            this.SpotLight.Size = new System.Drawing.Size(30, 30);
            this.SpotLight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.SpotLight.TabIndex = 3;
            this.SpotLight.TabStop = false;
            // 
            // RPN
            // 
            this.RPN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RPN.BackColor = System.Drawing.Color.Transparent;
            this.RPN.Image = global::ivosciwork.Properties.Resources.S300;
            this.RPN.Location = new System.Drawing.Point(674, 357);
            this.RPN.Name = "RPN";
            this.RPN.Size = new System.Drawing.Size(150, 100);
            this.RPN.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.RPN.TabIndex = 0;
            this.RPN.TabStop = false;
            // 
            // matrixArea
            // 
            this.matrixArea.Location = new System.Drawing.Point(34, 40);
            this.matrixArea.Name = "matrixArea";
            this.matrixArea.Size = new System.Drawing.Size(431, 417);
            this.matrixArea.TabIndex = 2;
            this.matrixArea.TabStop = false;
            this.matrixArea.Visible = false;
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lowerBeamBorder,
            this.upperBeamBorder});
            this.shapeContainer1.Size = new System.Drawing.Size(836, 469);
            this.shapeContainer1.TabIndex = 4;
            this.shapeContainer1.TabStop = false;
            // 
            // upperBeamBorder
            // 
            this.upperBeamBorder.Name = "upperBeamBorder";
            this.upperBeamBorder.X1 = 60;
            this.upperBeamBorder.X2 = 720;
            this.upperBeamBorder.Y1 = 46;
            this.upperBeamBorder.Y2 = 400;
            // 
            // lowerBeamBorder
            // 
            this.lowerBeamBorder.Name = "lowerBeamBorder";
            this.lowerBeamBorder.X1 = 46;
            this.lowerBeamBorder.X2 = 720;
            this.lowerBeamBorder.Y1 = 68;
            this.lowerBeamBorder.Y2 = 400;
            // 
            // RPNWorkVisualisation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 469);
            this.Controls.Add(this.SpotLight);
            this.Controls.Add(this.matrixArea);
            this.Controls.Add(this.shapeContainer1);
            this.Controls.Add(this.RPN);
            this.Name = "RPNWorkVisualisation";
            this.Text = "RPN";
            ((System.ComponentModel.ISupportInitialize)(this.SpotLight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RPN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.matrixArea)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox RPN;
        private System.Windows.Forms.PictureBox matrixArea;
        private System.Windows.Forms.PictureBox SpotLight;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape upperBeamBorder;
        private Microsoft.VisualBasic.PowerPacks.LineShape lowerBeamBorder;
    }
}