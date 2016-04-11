namespace ivosciwork
{
    partial class BeamForm
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
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.SpotLight = new Microsoft.VisualBasic.PowerPacks.OvalShape();
            this.lowerBeamBorder = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.upperBeamBorder = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.LittleRPN = new System.Windows.Forms.PictureBox();
            this.Sector4x12 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.Sector1x105 = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.sector = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            ((System.ComponentModel.ISupportInitialize)(this.LittleRPN)).BeginInit();
            this.SuspendLayout();
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.Sector4x12,
            this.Sector1x105,
            this.sector,
            this.SpotLight,
            this.lowerBeamBorder,
            this.upperBeamBorder});
            this.shapeContainer1.Size = new System.Drawing.Size(836, 469);
            this.shapeContainer1.TabIndex = 4;
            this.shapeContainer1.TabStop = false;
            // 
            // SpotLight
            // 
            this.SpotLight.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            this.SpotLight.Location = new System.Drawing.Point(30, 37);
            this.SpotLight.Name = "SpotLight";
            this.SpotLight.Size = new System.Drawing.Size(10, 10);
            this.SpotLight.Visible = false;
            // 
            // lowerBeamBorder
            // 
            this.lowerBeamBorder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lowerBeamBorder.Name = "lowerBeamBorder";
            this.lowerBeamBorder.Visible = false;
            this.lowerBeamBorder.X1 = 46;
            this.lowerBeamBorder.X2 = 800;
            this.lowerBeamBorder.Y1 = 68;
            this.lowerBeamBorder.Y2 = 450;
            // 
            // upperBeamBorder
            // 
            this.upperBeamBorder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.upperBeamBorder.Name = "upperBeamBorder";
            this.upperBeamBorder.Visible = false;
            this.upperBeamBorder.X1 = 60;
            this.upperBeamBorder.X2 = 800;
            this.upperBeamBorder.Y1 = 46;
            this.upperBeamBorder.Y2 = 450;
            // 
            // LittleRPN
            // 
            this.LittleRPN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LittleRPN.BackColor = System.Drawing.Color.White;
            this.LittleRPN.Image = global::ivosciwork.Properties.Resources.rpn;
            this.LittleRPN.Location = new System.Drawing.Point(800, 450);
            this.LittleRPN.Name = "LittleRPN";
            this.LittleRPN.Size = new System.Drawing.Size(20, 20);
            this.LittleRPN.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.LittleRPN.TabIndex = 5;
            this.LittleRPN.TabStop = false;
            // 
            // Sector4x12
            // 
            this.Sector4x12.BackgroundImage = global::ivosciwork.Properties.Resources.Sector4x12;
            this.Sector4x12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Sector4x12.Location = new System.Drawing.Point(377, 243);
            this.Sector4x12.Name = "Sector4x12";
            this.Sector4x12.Size = new System.Drawing.Size(76, 45);
            this.Sector4x12.Visible = false;
            // 
            // Sector1x105
            // 
            this.Sector1x105.BackgroundImage = global::ivosciwork.Properties.Resources.Sector1x105;
            this.Sector1x105.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Sector1x105.Location = new System.Drawing.Point(25, 200);
            this.Sector1x105.Name = "Sector1x105";
            this.Sector1x105.Size = new System.Drawing.Size(780, 215);
            this.Sector1x105.Visible = false;
            // 
            // sector
            // 
            this.sector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.sector.BackColor = System.Drawing.Color.Transparent;
            this.sector.BackgroundImage = global::ivosciwork.Properties.Resources.Sector;
            this.sector.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sector.BorderColor = System.Drawing.Color.Transparent;
            this.sector.Location = new System.Drawing.Point(22, 27);
            this.sector.Name = "sector";
            this.sector.Size = new System.Drawing.Size(786, 414);
            // 
            // BeamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(836, 469);
            this.Controls.Add(this.LittleRPN);
            this.Controls.Add(this.shapeContainer1);
            this.Name = "BeamForm";
            this.Text = "Beam";
            this.Load += new System.EventHandler(this.BeamForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LittleRPN)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape upperBeamBorder;
        private Microsoft.VisualBasic.PowerPacks.LineShape lowerBeamBorder;
        private System.Windows.Forms.PictureBox LittleRPN;
        private Microsoft.VisualBasic.PowerPacks.OvalShape SpotLight;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape sector;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape Sector1x105;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape Sector4x12;
    }
}