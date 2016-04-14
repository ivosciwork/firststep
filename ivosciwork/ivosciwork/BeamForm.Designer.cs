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
            this.LittleRPN = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.LittleRPN)).BeginInit();
            this.SuspendLayout();
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
            // BeamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(836, 469);
            this.Controls.Add(this.LittleRPN);
            this.Name = "BeamForm";
            this.Text = "Beam";
            this.Load += new System.EventHandler(this.BeamForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LittleRPN)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox LittleRPN;
    }
}