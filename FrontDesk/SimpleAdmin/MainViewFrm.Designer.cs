namespace SimpleAdmin
{
    partial class MainViewFrm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainViewFrm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.DeActBttn = new MaterialSkin.Controls.MaterialButton();
            this.serverAddressLabel = new MaterialSkin.Controls.MaterialLabel();
            this.TransferGameBttn = new MaterialSkin.Controls.MaterialButton();
            this.CancelGameBttn = new MaterialSkin.Controls.MaterialButton();
            this.OpenGameBttn = new MaterialSkin.Controls.MaterialButton();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.CardsBoard = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LaneDetailsLabel = new MaterialSkin.Controls.MaterialLabel();
            this.label36 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.CloseShiftBtnn = new MaterialSkin.Controls.MaterialButton();
            this.OpenShiftBttn = new MaterialSkin.Controls.MaterialButton();
            this.dateLabel = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox17 = new System.Windows.Forms.PictureBox();
            this.broculosDrawing1 = new SimpleAdmin.BroculosDrawing();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox17)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.51371F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.88889F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.11111F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1366, 768);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.11765F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.88235F));
            this.tableLayoutPanel2.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 101);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 664F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1360, 664);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.DeActBttn);
            this.panel3.Controls.Add(this.serverAddressLabel);
            this.panel3.Controls.Add(this.TransferGameBttn);
            this.panel3.Controls.Add(this.CancelGameBttn);
            this.panel3.Controls.Add(this.OpenGameBttn);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(739, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(618, 658);
            this.panel3.TabIndex = 2;
            // 
            // DeActBttn
            // 
            this.DeActBttn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.DeActBttn.AutoSize = false;
            this.DeActBttn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DeActBttn.Depth = 0;
            this.DeActBttn.DrawShadows = true;
            this.DeActBttn.Enabled = false;
            this.DeActBttn.HighEmphasis = true;
            this.DeActBttn.Icon = null;
            this.DeActBttn.Location = new System.Drawing.Point(237, 371);
            this.DeActBttn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.DeActBttn.MouseState = MaterialSkin.MouseState.HOVER;
            this.DeActBttn.Name = "DeActBttn";
            this.DeActBttn.Size = new System.Drawing.Size(179, 42);
            this.DeActBttn.TabIndex = 11;
            this.DeActBttn.Text = "Desactivar Pista";
            this.DeActBttn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.DeActBttn.UseAccentColor = false;
            this.DeActBttn.UseVisualStyleBackColor = true;
            this.DeActBttn.Click += new System.EventHandler(this.DeActBttn_Click);
            // 
            // serverAddressLabel
            // 
            this.serverAddressLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.serverAddressLabel.Depth = 0;
            this.serverAddressLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.serverAddressLabel.Location = new System.Drawing.Point(150, 595);
            this.serverAddressLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.serverAddressLabel.Name = "serverAddressLabel";
            this.serverAddressLabel.Size = new System.Drawing.Size(400, 40);
            this.serverAddressLabel.TabIndex = 10;
            this.serverAddressLabel.Text = "This Server IP: ";
            this.serverAddressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TransferGameBttn
            // 
            this.TransferGameBttn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TransferGameBttn.AutoSize = false;
            this.TransferGameBttn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.TransferGameBttn.Depth = 0;
            this.TransferGameBttn.DrawShadows = true;
            this.TransferGameBttn.Enabled = false;
            this.TransferGameBttn.HighEmphasis = true;
            this.TransferGameBttn.Icon = null;
            this.TransferGameBttn.Location = new System.Drawing.Point(237, 226);
            this.TransferGameBttn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.TransferGameBttn.MouseState = MaterialSkin.MouseState.HOVER;
            this.TransferGameBttn.Name = "TransferGameBttn";
            this.TransferGameBttn.Size = new System.Drawing.Size(179, 42);
            this.TransferGameBttn.TabIndex = 9;
            this.TransferGameBttn.Text = "Transferir Juego";
            this.TransferGameBttn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.TransferGameBttn.UseAccentColor = false;
            this.TransferGameBttn.UseVisualStyleBackColor = true;
            this.TransferGameBttn.Click += new System.EventHandler(this.TransferBttn_Click);
            // 
            // CancelGameBttn
            // 
            this.CancelGameBttn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CancelGameBttn.AutoSize = false;
            this.CancelGameBttn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelGameBttn.Depth = 0;
            this.CancelGameBttn.DrawShadows = true;
            this.CancelGameBttn.Enabled = false;
            this.CancelGameBttn.HighEmphasis = true;
            this.CancelGameBttn.Icon = null;
            this.CancelGameBttn.Location = new System.Drawing.Point(237, 297);
            this.CancelGameBttn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.CancelGameBttn.MouseState = MaterialSkin.MouseState.HOVER;
            this.CancelGameBttn.Name = "CancelGameBttn";
            this.CancelGameBttn.Size = new System.Drawing.Size(179, 42);
            this.CancelGameBttn.TabIndex = 8;
            this.CancelGameBttn.Text = "Cancelar Juego";
            this.CancelGameBttn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.CancelGameBttn.UseAccentColor = false;
            this.CancelGameBttn.UseVisualStyleBackColor = true;
            this.CancelGameBttn.Click += new System.EventHandler(this.CancelGameBttn_Click);
            // 
            // OpenGameBttn
            // 
            this.OpenGameBttn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.OpenGameBttn.AutoSize = false;
            this.OpenGameBttn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.OpenGameBttn.Depth = 0;
            this.OpenGameBttn.DrawShadows = true;
            this.OpenGameBttn.Enabled = false;
            this.OpenGameBttn.HighEmphasis = true;
            this.OpenGameBttn.Icon = null;
            this.OpenGameBttn.Location = new System.Drawing.Point(237, 152);
            this.OpenGameBttn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.OpenGameBttn.MouseState = MaterialSkin.MouseState.HOVER;
            this.OpenGameBttn.Name = "OpenGameBttn";
            this.OpenGameBttn.Size = new System.Drawing.Size(179, 42);
            this.OpenGameBttn.TabIndex = 7;
            this.OpenGameBttn.Text = "Abrir Juego";
            this.OpenGameBttn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.OpenGameBttn.UseAccentColor = false;
            this.OpenGameBttn.UseVisualStyleBackColor = true;
            this.OpenGameBttn.Click += new System.EventHandler(this.OpenGameBttn_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.CardsBoard, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79.77528F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.22472F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(736, 664);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // CardsBoard
            // 
            this.CardsBoard.AutoScroll = true;
            this.CardsBoard.BackColor = System.Drawing.SystemColors.Control;
            this.CardsBoard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CardsBoard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CardsBoard.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CardsBoard.Location = new System.Drawing.Point(0, 0);
            this.CardsBoard.Margin = new System.Windows.Forms.Padding(0);
            this.CardsBoard.Name = "CardsBoard";
            this.CardsBoard.Size = new System.Drawing.Size(736, 529);
            this.CardsBoard.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LaneDetailsLabel);
            this.groupBox1.Controls.Add(this.label36);
            this.groupBox1.Controls.Add(this.label44);
            this.groupBox1.Controls.Add(this.label38);
            this.groupBox1.Controls.Add(this.label42);
            this.groupBox1.Controls.Add(this.label40);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 532);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(730, 129);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Detalles de la Pista 4 ";
            // 
            // LaneDetailsLabel
            // 
            this.LaneDetailsLabel.Depth = 0;
            this.LaneDetailsLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.LaneDetailsLabel.HighEmphasis = true;
            this.LaneDetailsLabel.Location = new System.Drawing.Point(11, 0);
            this.LaneDetailsLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.LaneDetailsLabel.Name = "LaneDetailsLabel";
            this.LaneDetailsLabel.Size = new System.Drawing.Size(140, 23);
            this.LaneDetailsLabel.TabIndex = 4;
            this.LaneDetailsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(656, -53);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(50, 16);
            this.label36.TabIndex = 77;
            this.label36.Text = "Pista 5";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(56, -53);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(50, 16);
            this.label44.TabIndex = 65;
            this.label44.Text = "Pista 1";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(494, -53);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(50, 16);
            this.label38.TabIndex = 74;
            this.label38.Text = "Pista 4";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(197, -53);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(50, 16);
            this.label42.TabIndex = 68;
            this.label42.Text = "Pista 2";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(339, -53);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(50, 16);
            this.label40.TabIndex = 71;
            this.label40.Text = "Pista 3";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.panel2.Controls.Add(this.CloseShiftBtnn);
            this.panel2.Controls.Add(this.OpenShiftBttn);
            this.panel2.Controls.Add(this.dateLabel);
            this.panel2.Controls.Add(this.timeLabel);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.pictureBox17);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1360, 92);
            this.panel2.TabIndex = 1;
            // 
            // CloseShiftBtnn
            // 
            this.CloseShiftBtnn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseShiftBtnn.AutoSize = false;
            this.CloseShiftBtnn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CloseShiftBtnn.Depth = 0;
            this.CloseShiftBtnn.DrawShadows = true;
            this.CloseShiftBtnn.Enabled = false;
            this.CloseShiftBtnn.HighEmphasis = true;
            this.CloseShiftBtnn.Icon = null;
            this.CloseShiftBtnn.Location = new System.Drawing.Point(1104, 46);
            this.CloseShiftBtnn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.CloseShiftBtnn.MouseState = MaterialSkin.MouseState.HOVER;
            this.CloseShiftBtnn.Name = "CloseShiftBtnn";
            this.CloseShiftBtnn.Size = new System.Drawing.Size(120, 36);
            this.CloseShiftBtnn.TabIndex = 8;
            this.CloseShiftBtnn.Text = "Cerrar Turno";
            this.CloseShiftBtnn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.CloseShiftBtnn.UseAccentColor = false;
            this.CloseShiftBtnn.UseVisualStyleBackColor = true;
            // 
            // OpenShiftBttn
            // 
            this.OpenShiftBttn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenShiftBttn.AutoSize = false;
            this.OpenShiftBttn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.OpenShiftBttn.Depth = 0;
            this.OpenShiftBttn.DrawShadows = true;
            this.OpenShiftBttn.HighEmphasis = true;
            this.OpenShiftBttn.Icon = null;
            this.OpenShiftBttn.Location = new System.Drawing.Point(975, 46);
            this.OpenShiftBttn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.OpenShiftBttn.MouseState = MaterialSkin.MouseState.HOVER;
            this.OpenShiftBttn.Name = "OpenShiftBttn";
            this.OpenShiftBttn.Size = new System.Drawing.Size(120, 36);
            this.OpenShiftBttn.TabIndex = 7;
            this.OpenShiftBttn.Text = "Abrir Turno";
            this.OpenShiftBttn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.OpenShiftBttn.UseAccentColor = false;
            this.OpenShiftBttn.UseVisualStyleBackColor = true;
            // 
            // dateLabel
            // 
            this.dateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateLabel.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateLabel.ForeColor = System.Drawing.Color.Black;
            this.dateLabel.Location = new System.Drawing.Point(984, 9);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(360, 23);
            this.dateLabel.TabIndex = 4;
            this.dateLabel.Text = "miércoles, 28 de diciembre de 2021";
            this.dateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timeLabel
            // 
            this.timeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.timeLabel.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabel.ForeColor = System.Drawing.Color.Black;
            this.timeLabel.Location = new System.Drawing.Point(1232, 53);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(120, 27);
            this.timeLabel.TabIndex = 3;
            this.timeLabel.Text = "12:48 PM";
            this.timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(188, 68);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(27, 15);
            this.label12.TabIndex = 2;
            this.label12.Text = "MVP";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(105, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(200, 37);
            this.label6.TabIndex = 1;
            this.label6.Text = "BolManager";
            // 
            // pictureBox17
            // 
            this.pictureBox17.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox17.Image")));
            this.pictureBox17.Location = new System.Drawing.Point(3, 3);
            this.pictureBox17.Name = "pictureBox17";
            this.pictureBox17.Size = new System.Drawing.Size(96, 81);
            this.pictureBox17.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox17.TabIndex = 0;
            this.pictureBox17.TabStop = false;
            // 
            // broculosDrawing1
            // 
            this.broculosDrawing1.Location = new System.Drawing.Point(541, 260);
            this.broculosDrawing1.Name = "broculosDrawing1";
            this.broculosDrawing1.Size = new System.Drawing.Size(8, 9);
            this.broculosDrawing1.TabIndex = 11;
            // 
            // MainViewFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1366, 768);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainViewFrm";
            this.Text = "BolControl";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainViewFrm_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox17)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox17;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Panel CardsBoard;
        private MaterialSkin.Controls.MaterialLabel LaneDetailsLabel;
        private BroculosDrawing broculosDrawing1;
        private MaterialSkin.Controls.MaterialButton CancelGameBttn;
        private MaterialSkin.Controls.MaterialButton OpenGameBttn;
        private MaterialSkin.Controls.MaterialButton TransferGameBttn;
        private MaterialSkin.Controls.MaterialButton CloseShiftBtnn;
        private MaterialSkin.Controls.MaterialButton OpenShiftBttn;
        private MaterialSkin.Controls.MaterialLabel serverAddressLabel;
        private MaterialSkin.Controls.MaterialButton DeActBttn;
    }
}

