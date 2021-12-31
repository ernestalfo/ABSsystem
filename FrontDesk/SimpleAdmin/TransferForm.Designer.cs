namespace SimpleAdmin
{
    partial class TransferForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransferForm));
            this.LanesTargetCBox = new MaterialSkin.Controls.MaterialComboBox();
            this.DesactivateChckB = new MaterialSkin.Controls.MaterialCheckbox();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.AceptBttn = new MaterialSkin.Controls.MaterialButton();
            this.CancelBttn = new MaterialSkin.Controls.MaterialButton();
            this.SuspendLayout();
            // 
            // LanesTargetCBox
            // 
            this.LanesTargetCBox.AutoResize = false;
            this.LanesTargetCBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.LanesTargetCBox.Depth = 0;
            this.LanesTargetCBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.LanesTargetCBox.DropDownHeight = 174;
            this.LanesTargetCBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LanesTargetCBox.DropDownWidth = 121;
            this.LanesTargetCBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.LanesTargetCBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LanesTargetCBox.FormattingEnabled = true;
            this.LanesTargetCBox.IntegralHeight = false;
            this.LanesTargetCBox.ItemHeight = 43;
            this.LanesTargetCBox.Location = new System.Drawing.Point(69, 12);
            this.LanesTargetCBox.MaxDropDownItems = 4;
            this.LanesTargetCBox.MouseState = MaterialSkin.MouseState.OUT;
            this.LanesTargetCBox.Name = "LanesTargetCBox";
            this.LanesTargetCBox.Size = new System.Drawing.Size(121, 49);
            this.LanesTargetCBox.TabIndex = 1;
            this.LanesTargetCBox.SelectedIndexChanged += new System.EventHandler(this.LanesTargetCBox_SelectedIndexChanged);
            // 
            // DesactivateChckB
            // 
            this.DesactivateChckB.AutoSize = true;
            this.DesactivateChckB.Checked = true;
            this.DesactivateChckB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DesactivateChckB.Depth = 0;
            this.DesactivateChckB.Location = new System.Drawing.Point(225, 24);
            this.DesactivateChckB.Margin = new System.Windows.Forms.Padding(0);
            this.DesactivateChckB.MouseLocation = new System.Drawing.Point(-1, -1);
            this.DesactivateChckB.MouseState = MaterialSkin.MouseState.HOVER;
            this.DesactivateChckB.Name = "DesactivateChckB";
            this.DesactivateChckB.Ripple = true;
            this.DesactivateChckB.Size = new System.Drawing.Size(197, 37);
            this.DesactivateChckB.TabIndex = 2;
            this.DesactivateChckB.Text = "Desactivar pista actual";
            this.DesactivateChckB.UseVisualStyleBackColor = true;
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.Location = new System.Drawing.Point(16, 33);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(42, 19);
            this.materialLabel1.TabIndex = 3;
            this.materialLabel1.Text = "Hacia";
            // 
            // AceptBttn
            // 
            this.AceptBttn.AutoSize = false;
            this.AceptBttn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AceptBttn.Depth = 0;
            this.AceptBttn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.AceptBttn.DrawShadows = true;
            this.AceptBttn.HighEmphasis = true;
            this.AceptBttn.Icon = null;
            this.AceptBttn.Location = new System.Drawing.Point(53, 93);
            this.AceptBttn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.AceptBttn.MouseState = MaterialSkin.MouseState.HOVER;
            this.AceptBttn.Name = "AceptBttn";
            this.AceptBttn.Size = new System.Drawing.Size(145, 36);
            this.AceptBttn.TabIndex = 4;
            this.AceptBttn.Text = "Aceptar";
            this.AceptBttn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.AceptBttn.UseAccentColor = true;
            this.AceptBttn.UseVisualStyleBackColor = true;
            // 
            // CancelBttn
            // 
            this.CancelBttn.AutoSize = false;
            this.CancelBttn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelBttn.Depth = 0;
            this.CancelBttn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBttn.DrawShadows = true;
            this.CancelBttn.HighEmphasis = true;
            this.CancelBttn.Icon = null;
            this.CancelBttn.Location = new System.Drawing.Point(234, 93);
            this.CancelBttn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.CancelBttn.MouseState = MaterialSkin.MouseState.HOVER;
            this.CancelBttn.Name = "CancelBttn";
            this.CancelBttn.Size = new System.Drawing.Size(145, 36);
            this.CancelBttn.TabIndex = 5;
            this.CancelBttn.Text = "Cancelar";
            this.CancelBttn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.CancelBttn.UseAccentColor = true;
            this.CancelBttn.UseVisualStyleBackColor = true;
            // 
            // TransferForm
            // 
            this.AcceptButton = this.AceptBttn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBttn;
            this.ClientSize = new System.Drawing.Size(440, 144);
            this.Controls.Add(this.CancelBttn);
            this.Controls.Add(this.AceptBttn);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.DesactivateChckB);
            this.Controls.Add(this.LanesTargetCBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TransferForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transferir juego...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialComboBox LanesTargetCBox;
        private MaterialSkin.Controls.MaterialCheckbox DesactivateChckB;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialButton AceptBttn;
        private MaterialSkin.Controls.MaterialButton CancelBttn;
    }
}