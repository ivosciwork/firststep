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
            this.RPN = new System.Windows.Forms.PictureBox();
            this.Beam = new System.Windows.Forms.PictureBox();
            this.matrixArea = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.RPN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Beam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.matrixArea)).BeginInit();
            this.SuspendLayout();
            // 
            // RPN
            // 
            this.RPN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RPN.Image = global::ivosciwork.Properties.Resources.S300;
            this.RPN.Location = new System.Drawing.Point(674, 357);
            this.RPN.Name = "RPN";
            this.RPN.Size = new System.Drawing.Size(150, 100);
            this.RPN.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.RPN.TabIndex = 0;
            this.RPN.TabStop = false;
            // 
            // Beam
            // 
            this.Beam.Image = global::ivosciwork.Properties.Resources.Beam;
            this.Beam.Location = new System.Drawing.Point(34, 40);
            this.Beam.Name = "Beam";
            this.Beam.Size = new System.Drawing.Size(634, 320);
            this.Beam.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Beam.TabIndex = 1;
            this.Beam.TabStop = false;
            // 
            // matrixArea
            // 
            this.matrixArea.Location = new System.Drawing.Point(34, 40);
            this.matrixArea.Name = "matrixArea";
            this.matrixArea.Size = new System.Drawing.Size(431, 187);
            this.matrixArea.TabIndex = 2;
            this.matrixArea.TabStop = false;
            // 
            // RPNWorkVisualisation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 469);
            this.Controls.Add(this.RPN);
            this.Controls.Add(this.Beam);
            this.Controls.Add(this.matrixArea);
            this.Name = "RPNWorkVisualisation";
            this.Text = "RPN";
            ((System.ComponentModel.ISupportInitialize)(this.RPN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Beam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.matrixArea)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox RPN;
        private System.Windows.Forms.PictureBox Beam;
        private System.Windows.Forms.PictureBox matrixArea;
    }
}