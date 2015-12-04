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
            this.components = new System.ComponentModel.Container();
            this.SpotLight = new System.Windows.Forms.PictureBox();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lowerBeamBorder = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.upperBeamBorder = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.BeamTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.SpotLight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.SpotLight.Visible = false;
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
            // lowerBeamBorder
            // 
            this.lowerBeamBorder.Name = "lowerBeamBorder";
            this.lowerBeamBorder.Visible = false;
            this.lowerBeamBorder.X1 = 46;
            this.lowerBeamBorder.X2 = 800;
            this.lowerBeamBorder.Y1 = 68;
            this.lowerBeamBorder.Y2 = 450;
            // 
            // upperBeamBorder
            // 
            this.upperBeamBorder.Name = "upperBeamBorder";
            this.upperBeamBorder.Visible = false;
            this.upperBeamBorder.X1 = 60;
            this.upperBeamBorder.X2 = 800;
            this.upperBeamBorder.Y1 = 46;
            this.upperBeamBorder.Y2 = 450;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.pictureBox1.Location = new System.Drawing.Point(800, 450);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 10);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // BeamTimer
            // 
            this.BeamTimer.Enabled = true;
            this.BeamTimer.Interval = 1;
            // 
            // BeamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 469);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.SpotLight);
            this.Controls.Add(this.shapeContainer1);
            this.Name = "BeamForm";
            this.Text = "Beam";
            ((System.ComponentModel.ISupportInitialize)(this.SpotLight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox SpotLight;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape upperBeamBorder;
        private Microsoft.VisualBasic.PowerPacks.LineShape lowerBeamBorder;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer BeamTimer;
    }
}