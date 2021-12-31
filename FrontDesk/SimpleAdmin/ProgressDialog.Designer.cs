namespace SimpleAdmin
{
    partial class ProgressDialog
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
            this.progBar = new MaterialSkin.Controls.MaterialProgressBar();
            this.TaskInfoLabel = new MaterialSkin.Controls.MaterialLabel();
            this.SuspendLayout();
            // 
            // progBar
            // 
            this.progBar.Depth = 0;
            this.progBar.Location = new System.Drawing.Point(15, 90);
            this.progBar.Minimum = 25;
            this.progBar.MouseState = MaterialSkin.MouseState.HOVER;
            this.progBar.Name = "progBar";
            this.progBar.Size = new System.Drawing.Size(250, 5);
            this.progBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progBar.TabIndex = 0;
            this.progBar.Value = 25;
            // 
            // TaskInfoLabel
            // 
            this.TaskInfoLabel.Depth = 0;
            this.TaskInfoLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TaskInfoLabel.Location = new System.Drawing.Point(5, 41);
            this.TaskInfoLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.TaskInfoLabel.Name = "TaskInfoLabel";
            this.TaskInfoLabel.Size = new System.Drawing.Size(272, 23);
            this.TaskInfoLabel.TabIndex = 1;
            this.TaskInfoLabel.Text = "Abriendo Juego en Pista 2 ...";
            this.TaskInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProgressDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.BackColor = System.Drawing.SystemColors.Highlight;
            this.BackColor = System.Drawing.Color.FromArgb(255, 0, 120, 215);
            this.ClientSize = new System.Drawing.Size(281, 110);
            this.ControlBox = false;
            this.Controls.Add(this.TaskInfoLabel);
            this.Controls.Add(this.progBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressDialog";
            this.Opacity = 0.95D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Actividad en progreso...";
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialProgressBar progBar;
        private MaterialSkin.Controls.MaterialLabel TaskInfoLabel;
    }
}