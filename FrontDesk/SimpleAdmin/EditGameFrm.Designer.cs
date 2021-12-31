namespace SimpleAdmin
{
    partial class EditGameFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditGameFrm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.AddPlyBttn = new MaterialSkin.Controls.MaterialButton();
            this.PrevPlyBttn = new MaterialSkin.Controls.MaterialButton();
            this.materialTextBox3 = new MaterialSkin.Controls.MaterialTextBox();
            this.materialTextBox2 = new MaterialSkin.Controls.MaterialTextBox();
            this.NoTapCBox = new MaterialSkin.Controls.MaterialComboBox();
            this.TypePlyCBox = new MaterialSkin.Controls.MaterialComboBox();
            this.BumpersChckB = new MaterialSkin.Controls.MaterialCheckbox();
            this.PlyNameTB = new MaterialSkin.Controls.MaterialTextBox();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.PlayersList = new MaterialSkin.Controls.MaterialListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.TeamNameTB = new MaterialSkin.Controls.MaterialTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ByLinesCB = new MaterialSkin.Controls.MaterialComboBox();
            this.ByTimeCB = new MaterialSkin.Controls.MaterialComboBox();
            this.ByLinesRB = new MaterialSkin.Controls.MaterialRadioButton();
            this.ByTimeRB = new MaterialSkin.Controls.MaterialRadioButton();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.CorrectScoreBttn = new MaterialSkin.Controls.MaterialButton();
            this.materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            this.MovDownPlyBttn = new MaterialSkin.Controls.MaterialButton();
            this.MovUpPlyBttn = new MaterialSkin.Controls.MaterialButton();
            this.PausePlyBttn = new MaterialSkin.Controls.MaterialButton();
            this.DelPlyBttn = new MaterialSkin.Controls.MaterialButton();
            this.materialSwitch1 = new MaterialSkin.Controls.MaterialSwitch();
            this.ScoringMethodCB = new MaterialSkin.Controls.MaterialComboBox();
            this.materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            this.AceptBttn = new MaterialSkin.Controls.MaterialButton();
            this.CancelBttn = new MaterialSkin.Controls.MaterialButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.AddPlyBttn);
            this.groupBox1.Controls.Add(this.PrevPlyBttn);
            this.groupBox1.Controls.Add(this.materialTextBox3);
            this.groupBox1.Controls.Add(this.materialTextBox2);
            this.groupBox1.Controls.Add(this.NoTapCBox);
            this.groupBox1.Controls.Add(this.TypePlyCBox);
            this.groupBox1.Controls.Add(this.BumpersChckB);
            this.groupBox1.Controls.Add(this.PlyNameTB);
            this.groupBox1.Controls.Add(this.materialLabel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(498, 241);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "                                ";
            // 
            // AddPlyBttn
            // 
            this.AddPlyBttn.AutoSize = false;
            this.AddPlyBttn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AddPlyBttn.Depth = 0;
            this.AddPlyBttn.DrawShadows = true;
            this.AddPlyBttn.Enabled = false;
            this.AddPlyBttn.HighEmphasis = true;
            this.AddPlyBttn.Icon = null;
            this.AddPlyBttn.Location = new System.Drawing.Point(265, 187);
            this.AddPlyBttn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.AddPlyBttn.MouseState = MaterialSkin.MouseState.HOVER;
            this.AddPlyBttn.Name = "AddPlyBttn";
            this.AddPlyBttn.Size = new System.Drawing.Size(148, 36);
            this.AddPlyBttn.TabIndex = 10;
            this.AddPlyBttn.Text = "Añadir Otro...";
            this.AddPlyBttn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.AddPlyBttn.UseAccentColor = false;
            this.AddPlyBttn.UseVisualStyleBackColor = true;
            this.AddPlyBttn.Click += new System.EventHandler(this.AddPlyBttn_Click);
            // 
            // PrevPlyBttn
            // 
            this.PrevPlyBttn.AutoSize = false;
            this.PrevPlyBttn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PrevPlyBttn.Depth = 0;
            this.PrevPlyBttn.DrawShadows = true;
            this.PrevPlyBttn.Enabled = false;
            this.PrevPlyBttn.HighEmphasis = true;
            this.PrevPlyBttn.Icon = null;
            this.PrevPlyBttn.Location = new System.Drawing.Point(78, 187);
            this.PrevPlyBttn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.PrevPlyBttn.MouseState = MaterialSkin.MouseState.HOVER;
            this.PrevPlyBttn.Name = "PrevPlyBttn";
            this.PrevPlyBttn.Size = new System.Drawing.Size(148, 36);
            this.PrevPlyBttn.TabIndex = 9;
            this.PrevPlyBttn.Text = "Anterior";
            this.PrevPlyBttn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.PrevPlyBttn.UseAccentColor = false;
            this.PrevPlyBttn.UseVisualStyleBackColor = true;
            this.PrevPlyBttn.Click += new System.EventHandler(this.PrevPlyBttn_Click);
            // 
            // materialTextBox3
            // 
            this.materialTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.materialTextBox3.Depth = 0;
            this.materialTextBox3.Enabled = false;
            this.materialTextBox3.Font = new System.Drawing.Font("Roboto", 12F);
            this.materialTextBox3.Hint = "Blind AVG";
            this.materialTextBox3.Location = new System.Drawing.Point(159, 117);
            this.materialTextBox3.MaxLength = 50;
            this.materialTextBox3.MouseState = MaterialSkin.MouseState.OUT;
            this.materialTextBox3.Multiline = false;
            this.materialTextBox3.Name = "materialTextBox3";
            this.materialTextBox3.Size = new System.Drawing.Size(100, 50);
            this.materialTextBox3.TabIndex = 8;
            this.materialTextBox3.Text = "";
            // 
            // materialTextBox2
            // 
            this.materialTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.materialTextBox2.Depth = 0;
            this.materialTextBox2.Enabled = false;
            this.materialTextBox2.Font = new System.Drawing.Font("Roboto", 12F);
            this.materialTextBox2.Hint = "Handicap";
            this.materialTextBox2.Location = new System.Drawing.Point(265, 117);
            this.materialTextBox2.MaxLength = 50;
            this.materialTextBox2.MouseState = MaterialSkin.MouseState.OUT;
            this.materialTextBox2.Multiline = false;
            this.materialTextBox2.Name = "materialTextBox2";
            this.materialTextBox2.Size = new System.Drawing.Size(100, 50);
            this.materialTextBox2.TabIndex = 7;
            this.materialTextBox2.Text = "";
            // 
            // NoTapCBox
            // 
            this.NoTapCBox.AutoResize = false;
            this.NoTapCBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.NoTapCBox.Depth = 0;
            this.NoTapCBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.NoTapCBox.DropDownHeight = 174;
            this.NoTapCBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NoTapCBox.DropDownWidth = 121;
            this.NoTapCBox.Enabled = false;
            this.NoTapCBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.NoTapCBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.NoTapCBox.FormattingEnabled = true;
            this.NoTapCBox.Hint = "No Tap";
            this.NoTapCBox.IntegralHeight = false;
            this.NoTapCBox.ItemHeight = 43;
            this.NoTapCBox.Items.AddRange(new object[] {
            "Ninguno",
            "No Tap 9",
            "No Tap 8",
            "No Tap 7",
            "No Tap 6",
            "No Tap 5"});
            this.NoTapCBox.Location = new System.Drawing.Point(235, 43);
            this.NoTapCBox.MaxDropDownItems = 4;
            this.NoTapCBox.MouseState = MaterialSkin.MouseState.OUT;
            this.NoTapCBox.Name = "NoTapCBox";
            this.NoTapCBox.Size = new System.Drawing.Size(121, 49);
            this.NoTapCBox.TabIndex = 6;
            // 
            // TypePlyCBox
            // 
            this.TypePlyCBox.AutoResize = false;
            this.TypePlyCBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.TypePlyCBox.Depth = 0;
            this.TypePlyCBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.TypePlyCBox.DropDownHeight = 174;
            this.TypePlyCBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TypePlyCBox.DropDownWidth = 121;
            this.TypePlyCBox.Enabled = false;
            this.TypePlyCBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.TypePlyCBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.TypePlyCBox.FormattingEnabled = true;
            this.TypePlyCBox.Hint = "Tipo";
            this.TypePlyCBox.IntegralHeight = false;
            this.TypePlyCBox.ItemHeight = 43;
            this.TypePlyCBox.Items.AddRange(new object[] {
            "Normal",
            "Blind",
            "Pacer"});
            this.TypePlyCBox.Location = new System.Drawing.Point(22, 116);
            this.TypePlyCBox.MaxDropDownItems = 4;
            this.TypePlyCBox.MouseState = MaterialSkin.MouseState.OUT;
            this.TypePlyCBox.Name = "TypePlyCBox";
            this.TypePlyCBox.Size = new System.Drawing.Size(121, 49);
            this.TypePlyCBox.TabIndex = 5;
            // 
            // BumpersChckB
            // 
            this.BumpersChckB.AutoSize = true;
            this.BumpersChckB.Depth = 0;
            this.BumpersChckB.Location = new System.Drawing.Point(386, 44);
            this.BumpersChckB.Margin = new System.Windows.Forms.Padding(0);
            this.BumpersChckB.MouseLocation = new System.Drawing.Point(-1, -1);
            this.BumpersChckB.MouseState = MaterialSkin.MouseState.HOVER;
            this.BumpersChckB.Name = "BumpersChckB";
            this.BumpersChckB.Ripple = true;
            this.BumpersChckB.Size = new System.Drawing.Size(98, 37);
            this.BumpersChckB.TabIndex = 4;
            this.BumpersChckB.Text = "Bumpers";
            this.BumpersChckB.UseVisualStyleBackColor = true;
            // 
            // PlyNameTB
            // 
            this.PlyNameTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PlyNameTB.Depth = 0;
            this.PlyNameTB.Font = new System.Drawing.Font("Roboto", 12F);
            this.PlyNameTB.Hint = "Nombre";
            this.PlyNameTB.Location = new System.Drawing.Point(22, 44);
            this.PlyNameTB.MaxLength = 50;
            this.PlyNameTB.MouseState = MaterialSkin.MouseState.OUT;
            this.PlyNameTB.Multiline = false;
            this.PlyNameTB.Name = "PlyNameTB";
            this.PlyNameTB.Size = new System.Drawing.Size(190, 50);
            this.PlyNameTB.TabIndex = 2;
            this.PlyNameTB.Text = "";
            this.PlyNameTB.TextChanged += new System.EventHandler(this.PlyNameTB_TextChanged);
            this.PlyNameTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PlyNameTB_KeyDown);
            this.PlyNameTB.KeyUp += new System.Windows.Forms.KeyEventHandler(this.PlyNameTB_KeyUp);
            // 
            // materialLabel1
            // 
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.Location = new System.Drawing.Point(19, 0);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(65, 19);
            this.materialLabel1.TabIndex = 0;
            this.materialLabel1.Text = "Jugador: ";
            this.materialLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PlayersList
            // 
            this.PlayersList.AutoArrange = false;
            this.PlayersList.AutoSizeTable = false;
            this.PlayersList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.PlayersList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PlayersList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.PlayersList.Depth = 0;
            this.PlayersList.Font = new System.Drawing.Font("Microsoft Sans Serif", 34F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.PlayersList.FullRowSelect = true;
            this.PlayersList.HideSelection = false;
            this.PlayersList.Location = new System.Drawing.Point(12, 373);
            this.PlayersList.MaximumSize = new System.Drawing.Size(500, 700);
            this.PlayersList.MinimumSize = new System.Drawing.Size(500, 100);
            this.PlayersList.MouseLocation = new System.Drawing.Point(-1, -1);
            this.PlayersList.MouseState = MaterialSkin.MouseState.OUT;
            this.PlayersList.Name = "PlayersList";
            this.PlayersList.OwnerDraw = true;
            this.PlayersList.Size = new System.Drawing.Size(500, 335);
            this.PlayersList.TabIndex = 1;
            this.PlayersList.UseCompatibleStateImageBehavior = false;
            this.PlayersList.View = System.Windows.Forms.View.Details;
            this.PlayersList.SelectedIndexChanged += new System.EventHandler(this.PlayersList_SelectedIndexChanged);
            this.PlayersList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PlayersList_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Nombre";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "estado";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "HCP";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "NoTap";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 100;
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel2.Location = new System.Drawing.Point(155, 342);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(169, 19);
            this.materialLabel2.TabIndex = 2;
            this.materialLabel2.Text = "Jugadores en el equipo:";
            // 
            // TeamNameTB
            // 
            this.TeamNameTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TeamNameTB.Depth = 0;
            this.TeamNameTB.Font = new System.Drawing.Font("Roboto", 12F);
            this.TeamNameTB.Hint = "Nombre del Equipo";
            this.TeamNameTB.Location = new System.Drawing.Point(117, 278);
            this.TeamNameTB.MaxLength = 50;
            this.TeamNameTB.MouseState = MaterialSkin.MouseState.OUT;
            this.TeamNameTB.Multiline = false;
            this.TeamNameTB.Name = "TeamNameTB";
            this.TeamNameTB.Size = new System.Drawing.Size(274, 50);
            this.TeamNameTB.TabIndex = 3;
            this.TeamNameTB.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ByLinesCB);
            this.groupBox2.Controls.Add(this.ByTimeCB);
            this.groupBox2.Controls.Add(this.ByLinesRB);
            this.groupBox2.Controls.Add(this.ByTimeRB);
            this.groupBox2.Controls.Add(this.materialLabel3);
            this.groupBox2.Location = new System.Drawing.Point(565, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(291, 126);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "                                            ";
            // 
            // ByLinesCB
            // 
            this.ByLinesCB.AutoResize = false;
            this.ByLinesCB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ByLinesCB.Depth = 0;
            this.ByLinesCB.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ByLinesCB.DropDownHeight = 118;
            this.ByLinesCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ByLinesCB.DropDownWidth = 121;
            this.ByLinesCB.Enabled = false;
            this.ByLinesCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.ByLinesCB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ByLinesCB.FormattingEnabled = true;
            this.ByLinesCB.IntegralHeight = false;
            this.ByLinesCB.ItemHeight = 29;
            this.ByLinesCB.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "∞"});
            this.ByLinesCB.Location = new System.Drawing.Point(151, 67);
            this.ByLinesCB.MaxDropDownItems = 4;
            this.ByLinesCB.MouseState = MaterialSkin.MouseState.OUT;
            this.ByLinesCB.Name = "ByLinesCB";
            this.ByLinesCB.Size = new System.Drawing.Size(121, 35);
            this.ByLinesCB.TabIndex = 9;
            this.ByLinesCB.UseTallSize = false;
            // 
            // ByTimeCB
            // 
            this.ByTimeCB.AutoResize = false;
            this.ByTimeCB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ByTimeCB.Depth = 0;
            this.ByTimeCB.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ByTimeCB.DropDownHeight = 118;
            this.ByTimeCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ByTimeCB.DropDownWidth = 121;
            this.ByTimeCB.Enabled = false;
            this.ByTimeCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.ByTimeCB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ByTimeCB.FormattingEnabled = true;
            this.ByTimeCB.IntegralHeight = false;
            this.ByTimeCB.ItemHeight = 29;
            this.ByTimeCB.Items.AddRange(new object[] {
            "1h",
            "2h",
            "3h",
            "4h",
            "5h",
            "6h",
            "7h",
            "8h",
            "9h",
            "10h"});
            this.ByTimeCB.Location = new System.Drawing.Point(151, 24);
            this.ByTimeCB.MaxDropDownItems = 4;
            this.ByTimeCB.MouseState = MaterialSkin.MouseState.OUT;
            this.ByTimeCB.Name = "ByTimeCB";
            this.ByTimeCB.Size = new System.Drawing.Size(121, 35);
            this.ByTimeCB.TabIndex = 8;
            this.ByTimeCB.UseTallSize = false;
            // 
            // ByLinesRB
            // 
            this.ByLinesRB.AutoSize = true;
            this.ByLinesRB.Depth = 0;
            this.ByLinesRB.Location = new System.Drawing.Point(10, 67);
            this.ByLinesRB.Margin = new System.Windows.Forms.Padding(0);
            this.ByLinesRB.MouseLocation = new System.Drawing.Point(-1, -1);
            this.ByLinesRB.MouseState = MaterialSkin.MouseState.HOVER;
            this.ByLinesRB.Name = "ByLinesRB";
            this.ByLinesRB.Ripple = true;
            this.ByLinesRB.Size = new System.Drawing.Size(109, 37);
            this.ByLinesRB.TabIndex = 7;
            this.ByLinesRB.TabStop = true;
            this.ByLinesRB.Text = "por Líneas";
            this.ByLinesRB.UseVisualStyleBackColor = true;
            // 
            // ByTimeRB
            // 
            this.ByTimeRB.AutoSize = true;
            this.ByTimeRB.Depth = 0;
            this.ByTimeRB.Enabled = false;
            this.ByTimeRB.Location = new System.Drawing.Point(10, 24);
            this.ByTimeRB.Margin = new System.Windows.Forms.Padding(0);
            this.ByTimeRB.MouseLocation = new System.Drawing.Point(-1, -1);
            this.ByTimeRB.MouseState = MaterialSkin.MouseState.HOVER;
            this.ByTimeRB.Name = "ByTimeRB";
            this.ByTimeRB.Ripple = true;
            this.ByTimeRB.Size = new System.Drawing.Size(116, 37);
            this.ByTimeRB.TabIndex = 6;
            this.ByTimeRB.TabStop = true;
            this.ByTimeRB.Text = "por Tiempo";
            this.ByTimeRB.UseVisualStyleBackColor = true;
            // 
            // materialLabel3
            // 
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel3.Location = new System.Drawing.Point(16, 1);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(100, 23);
            this.materialLabel3.TabIndex = 5;
            this.materialLabel3.Text = "Tipo de renta";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CorrectScoreBttn);
            this.groupBox3.Controls.Add(this.materialLabel4);
            this.groupBox3.Controls.Add(this.MovDownPlyBttn);
            this.groupBox3.Controls.Add(this.MovUpPlyBttn);
            this.groupBox3.Controls.Add(this.PausePlyBttn);
            this.groupBox3.Controls.Add(this.DelPlyBttn);
            this.groupBox3.Location = new System.Drawing.Point(535, 373);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(396, 286);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "                                                                                 " +
    "                     ";
            // 
            // CorrectScoreBttn
            // 
            this.CorrectScoreBttn.AutoSize = false;
            this.CorrectScoreBttn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CorrectScoreBttn.Depth = 0;
            this.CorrectScoreBttn.DrawShadows = true;
            this.CorrectScoreBttn.HighEmphasis = true;
            this.CorrectScoreBttn.Icon = null;
            this.CorrectScoreBttn.Location = new System.Drawing.Point(117, 178);
            this.CorrectScoreBttn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.CorrectScoreBttn.MouseState = MaterialSkin.MouseState.HOVER;
            this.CorrectScoreBttn.Name = "CorrectScoreBttn";
            this.CorrectScoreBttn.Size = new System.Drawing.Size(158, 36);
            this.CorrectScoreBttn.TabIndex = 18;
            this.CorrectScoreBttn.Text = "Puntuación";
            this.CorrectScoreBttn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.CorrectScoreBttn.UseAccentColor = false;
            this.CorrectScoreBttn.UseVisualStyleBackColor = true;
            this.CorrectScoreBttn.Click += new System.EventHandler(this.CorrectScoreBttn_Click);
            // 
            // materialLabel4
            // 
            this.materialLabel4.AutoSize = true;
            this.materialLabel4.Depth = 0;
            this.materialLabel4.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel4.Location = new System.Drawing.Point(21, 0);
            this.materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            this.materialLabel4.Size = new System.Drawing.Size(284, 19);
            this.materialLabel4.TabIndex = 17;
            this.materialLabel4.Text = "Acciones sobre los jugadores del equipo";
            // 
            // MovDownPlyBttn
            // 
            this.MovDownPlyBttn.AutoSize = false;
            this.MovDownPlyBttn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MovDownPlyBttn.Depth = 0;
            this.MovDownPlyBttn.DrawShadows = true;
            this.MovDownPlyBttn.Enabled = false;
            this.MovDownPlyBttn.HighEmphasis = true;
            this.MovDownPlyBttn.Icon = null;
            this.MovDownPlyBttn.Location = new System.Drawing.Point(209, 117);
            this.MovDownPlyBttn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.MovDownPlyBttn.MouseState = MaterialSkin.MouseState.HOVER;
            this.MovDownPlyBttn.Name = "MovDownPlyBttn";
            this.MovDownPlyBttn.Size = new System.Drawing.Size(158, 36);
            this.MovDownPlyBttn.TabIndex = 16;
            this.MovDownPlyBttn.Text = "MOVER ABJ.";
            this.MovDownPlyBttn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.MovDownPlyBttn.UseAccentColor = false;
            this.MovDownPlyBttn.UseVisualStyleBackColor = true;
            this.MovDownPlyBttn.Click += new System.EventHandler(this.MovDownPlyBttn_Click);
            // 
            // MovUpPlyBttn
            // 
            this.MovUpPlyBttn.AutoSize = false;
            this.MovUpPlyBttn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MovUpPlyBttn.Depth = 0;
            this.MovUpPlyBttn.DrawShadows = true;
            this.MovUpPlyBttn.Enabled = false;
            this.MovUpPlyBttn.HighEmphasis = true;
            this.MovUpPlyBttn.Icon = null;
            this.MovUpPlyBttn.Location = new System.Drawing.Point(30, 117);
            this.MovUpPlyBttn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.MovUpPlyBttn.MouseState = MaterialSkin.MouseState.HOVER;
            this.MovUpPlyBttn.Name = "MovUpPlyBttn";
            this.MovUpPlyBttn.Size = new System.Drawing.Size(158, 36);
            this.MovUpPlyBttn.TabIndex = 15;
            this.MovUpPlyBttn.Text = "Mover Arr.";
            this.MovUpPlyBttn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.MovUpPlyBttn.UseAccentColor = false;
            this.MovUpPlyBttn.UseVisualStyleBackColor = true;
            this.MovUpPlyBttn.Click += new System.EventHandler(this.MovUpPlyBttn_Click);
            // 
            // PausePlyBttn
            // 
            this.PausePlyBttn.AutoSize = false;
            this.PausePlyBttn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PausePlyBttn.Depth = 0;
            this.PausePlyBttn.DrawShadows = true;
            this.PausePlyBttn.Enabled = false;
            this.PausePlyBttn.HighEmphasis = true;
            this.PausePlyBttn.Icon = null;
            this.PausePlyBttn.Location = new System.Drawing.Point(30, 55);
            this.PausePlyBttn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.PausePlyBttn.MouseState = MaterialSkin.MouseState.HOVER;
            this.PausePlyBttn.Name = "PausePlyBttn";
            this.PausePlyBttn.Size = new System.Drawing.Size(158, 36);
            this.PausePlyBttn.TabIndex = 13;
            this.PausePlyBttn.Text = "Pausar";
            this.PausePlyBttn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.PausePlyBttn.UseAccentColor = false;
            this.PausePlyBttn.UseVisualStyleBackColor = true;
            // 
            // DelPlyBttn
            // 
            this.DelPlyBttn.AutoSize = false;
            this.DelPlyBttn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DelPlyBttn.Depth = 0;
            this.DelPlyBttn.DrawShadows = true;
            this.DelPlyBttn.Enabled = false;
            this.DelPlyBttn.HighEmphasis = true;
            this.DelPlyBttn.Icon = null;
            this.DelPlyBttn.Location = new System.Drawing.Point(211, 55);
            this.DelPlyBttn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.DelPlyBttn.MouseState = MaterialSkin.MouseState.HOVER;
            this.DelPlyBttn.Name = "DelPlyBttn";
            this.DelPlyBttn.Size = new System.Drawing.Size(158, 36);
            this.DelPlyBttn.TabIndex = 12;
            this.DelPlyBttn.Text = "Borrar";
            this.DelPlyBttn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.DelPlyBttn.UseAccentColor = false;
            this.DelPlyBttn.UseVisualStyleBackColor = true;
            this.DelPlyBttn.Click += new System.EventHandler(this.DelPlyBttn_Click);
            // 
            // materialSwitch1
            // 
            this.materialSwitch1.AutoSize = true;
            this.materialSwitch1.Depth = 0;
            this.materialSwitch1.Location = new System.Drawing.Point(559, 159);
            this.materialSwitch1.Margin = new System.Windows.Forms.Padding(0);
            this.materialSwitch1.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialSwitch1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialSwitch1.Name = "materialSwitch1";
            this.materialSwitch1.Ripple = true;
            this.materialSwitch1.Size = new System.Drawing.Size(134, 37);
            this.materialSwitch1.TabIndex = 12;
            this.materialSwitch1.Text = "Modo Liga";
            this.materialSwitch1.UseVisualStyleBackColor = true;
            // 
            // ScoringMethodCB
            // 
            this.ScoringMethodCB.AutoResize = false;
            this.ScoringMethodCB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ScoringMethodCB.Depth = 0;
            this.ScoringMethodCB.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ScoringMethodCB.DropDownHeight = 174;
            this.ScoringMethodCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ScoringMethodCB.DropDownWidth = 121;
            this.ScoringMethodCB.Enabled = false;
            this.ScoringMethodCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.ScoringMethodCB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ScoringMethodCB.FormattingEnabled = true;
            this.ScoringMethodCB.Hint = "Tipo de Juego";
            this.ScoringMethodCB.IntegralHeight = false;
            this.ScoringMethodCB.ItemHeight = 43;
            this.ScoringMethodCB.Items.AddRange(new object[] {
            "Classic",
            "NoTap 9",
            "NoTap 8",
            "NoTap 7",
            "3-6-9",
            "Low Game"});
            this.ScoringMethodCB.Location = new System.Drawing.Point(744, 202);
            this.ScoringMethodCB.MaxDropDownItems = 4;
            this.ScoringMethodCB.MouseState = MaterialSkin.MouseState.OUT;
            this.ScoringMethodCB.Name = "ScoringMethodCB";
            this.ScoringMethodCB.Size = new System.Drawing.Size(149, 49);
            this.ScoringMethodCB.TabIndex = 13;
            // 
            // materialLabel5
            // 
            this.materialLabel5.AutoSize = true;
            this.materialLabel5.Depth = 0;
            this.materialLabel5.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel5.Location = new System.Drawing.Point(568, 228);
            this.materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel5.Name = "materialLabel5";
            this.materialLabel5.Size = new System.Drawing.Size(161, 19);
            this.materialLabel5.TabIndex = 14;
            this.materialLabel5.Text = "Método de Puntuación";
            // 
            // AceptBttn
            // 
            this.AceptBttn.AutoSize = false;
            this.AceptBttn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AceptBttn.Depth = 0;
            this.AceptBttn.DrawShadows = true;
            this.AceptBttn.HighEmphasis = true;
            this.AceptBttn.Icon = null;
            this.AceptBttn.Location = new System.Drawing.Point(578, 292);
            this.AceptBttn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.AceptBttn.MouseState = MaterialSkin.MouseState.HOVER;
            this.AceptBttn.Name = "AceptBttn";
            this.AceptBttn.Size = new System.Drawing.Size(145, 36);
            this.AceptBttn.TabIndex = 15;
            this.AceptBttn.Text = "Aceptar";
            this.AceptBttn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.AceptBttn.UseAccentColor = true;
            this.AceptBttn.UseVisualStyleBackColor = true;
            this.AceptBttn.Click += new System.EventHandler(this.AceptBttn_Click);
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
            this.CancelBttn.Location = new System.Drawing.Point(746, 292);
            this.CancelBttn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.CancelBttn.MouseState = MaterialSkin.MouseState.HOVER;
            this.CancelBttn.Name = "CancelBttn";
            this.CancelBttn.Size = new System.Drawing.Size(145, 36);
            this.CancelBttn.TabIndex = 16;
            this.CancelBttn.Text = "Cancelar";
            this.CancelBttn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.CancelBttn.UseAccentColor = true;
            this.CancelBttn.UseVisualStyleBackColor = true;
            // 
            // EditGameFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBttn;
            this.ClientSize = new System.Drawing.Size(942, 717);
            this.Controls.Add(this.CancelBttn);
            this.Controls.Add(this.AceptBttn);
            this.Controls.Add(this.materialLabel5);
            this.Controls.Add(this.ScoringMethodCB);
            this.Controls.Add(this.materialSwitch1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.TeamNameTB);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.PlayersList);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditGameFrm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Abrir Juego...";
            this.Activated += new System.EventHandler(this.EditGameFrm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditGameFrm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialTextBox PlyNameTB;
        private MaterialSkin.Controls.MaterialCheckbox BumpersChckB;
        private MaterialSkin.Controls.MaterialComboBox TypePlyCBox;
        private MaterialSkin.Controls.MaterialButton PrevPlyBttn;
        private MaterialSkin.Controls.MaterialTextBox materialTextBox3;
        private MaterialSkin.Controls.MaterialTextBox materialTextBox2;
        private MaterialSkin.Controls.MaterialComboBox NoTapCBox;
        private MaterialSkin.Controls.MaterialButton AddPlyBttn;
        private MaterialSkin.Controls.MaterialListView PlayersList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private MaterialSkin.Controls.MaterialTextBox TeamNameTB;
        private System.Windows.Forms.GroupBox groupBox2;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialRadioButton ByLinesRB;
        private MaterialSkin.Controls.MaterialRadioButton ByTimeRB;
        private MaterialSkin.Controls.MaterialComboBox ByTimeCB;
        private MaterialSkin.Controls.MaterialComboBox ByLinesCB;
        private System.Windows.Forms.GroupBox groupBox3;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;
        private MaterialSkin.Controls.MaterialButton MovDownPlyBttn;
        private MaterialSkin.Controls.MaterialButton MovUpPlyBttn;
        private MaterialSkin.Controls.MaterialButton PausePlyBttn;
        private MaterialSkin.Controls.MaterialButton DelPlyBttn;
        private MaterialSkin.Controls.MaterialSwitch materialSwitch1;
        private MaterialSkin.Controls.MaterialButton CorrectScoreBttn;
        private MaterialSkin.Controls.MaterialComboBox ScoringMethodCB;
        private MaterialSkin.Controls.MaterialLabel materialLabel5;
        private MaterialSkin.Controls.MaterialButton AceptBttn;
        private MaterialSkin.Controls.MaterialButton CancelBttn;
    }
}