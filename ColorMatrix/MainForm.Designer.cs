namespace ColorMatrix_ns
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.closeBtn = new System.Windows.Forms.Button();
            this.applyBtn = new System.Windows.Forms.Button();
            this.C0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveProfileBtn = new System.Windows.Forms.Button();
            this.loadProfileBtn = new System.Windows.Forms.Button();
            this.loadImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.loadFilterDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFilterDialog = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.filterFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numBar = new System.Windows.Forms.TrackBar();
            this.numMaxBox = new System.Windows.Forms.NumericUpDown();
            this.numMinBox = new System.Windows.Forms.NumericUpDown();
            this.saveImageDialog = new System.Windows.Forms.SaveFileDialog();
            this.loadFiltersBtn = new System.Windows.Forms.Button();
            this.gridGroupOut = new System.Windows.Forms.GroupBox();
            this.gridGroupIn = new System.Windows.Forms.Panel();
            this.saveFiltersBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.aboutBtn = new System.Windows.Forms.Button();
            this.helpBtn = new System.Windows.Forms.Button();
            this.maxIcon = new System.Windows.Forms.Panel();
            this.minIcon = new System.Windows.Forms.Panel();
            this.icon = new System.Windows.Forms.Panel();
            this.filterGalleryGrp = new System.Windows.Forms.GroupBox();
            this.helpProvider = new System.Windows.Forms.HelpProvider();
            this.imageTable = new System.Windows.Forms.TableLayoutPanel();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.mainTable = new System.Windows.Forms.TableLayoutPanel();
            this.topPanel = new System.Windows.Forms.Panel();
            this.title = new System.Windows.Forms.Label();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.botPanel = new System.Windows.Forms.Panel();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.lrPanel = new System.Windows.Forms.Panel();
            this.llPanel = new System.Windows.Forms.Panel();
            this.ulpanel = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ipanel1 = new ColorMatrix_ns.ImagePanel();
            this.ipanel2 = new ColorMatrix_ns.ImagePanel();
            this.ipanel3 = new ColorMatrix_ns.ImagePanel();
            this.filterGrid = new ColorMatrix_ns.FilterGrid();
            ((System.ComponentModel.ISupportInitialize)(this.numBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinBox)).BeginInit();
            this.gridGroupOut.SuspendLayout();
            this.gridGroupIn.SuspendLayout();
            this.filterGalleryGrp.SuspendLayout();
            this.imageTable.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.mainTable.SuspendLayout();
            this.topPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // closeBtn
            // 
            this.closeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeBtn.BackgroundImage = global::ColorMatrix_ns.Properties.Resources.button;
            this.closeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.closeBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeBtn.ForeColor = System.Drawing.Color.White;
            this.closeBtn.Location = new System.Drawing.Point(837, 627);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(73, 26);
            this.closeBtn.TabIndex = 3;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // applyBtn
            // 
            this.applyBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.applyBtn.Location = new System.Drawing.Point(351, 192);
            this.applyBtn.Name = "applyBtn";
            this.applyBtn.Size = new System.Drawing.Size(95, 25);
            this.applyBtn.TabIndex = 4;
            this.applyBtn.Text = "Apply";
            this.toolTip.SetToolTip(this.applyBtn, "Apply color matrix, update images.");
            this.applyBtn.UseVisualStyleBackColor = false;
            this.applyBtn.Click += new System.EventHandler(this.applyBtn_Click);
            // 
            // C0
            // 
            this.C0.DataPropertyName = "C0";
            this.C0.HeaderText = "Red";
            this.C0.Name = "C0";
            this.C0.Width = 60;
            // 
            // C1
            // 
            this.C1.DataPropertyName = "C1";
            this.C1.HeaderText = "Green";
            this.C1.Name = "C1";
            this.C1.Width = 60;
            // 
            // C2
            // 
            this.C2.DataPropertyName = "C2";
            this.C2.HeaderText = "Blue";
            this.C2.Name = "C2";
            this.C2.Width = 60;
            // 
            // C3
            // 
            this.C3.DataPropertyName = "C3";
            this.C3.HeaderText = "Alpha";
            this.C3.Name = "C3";
            this.C3.Width = 60;
            // 
            // C4
            // 
            this.C4.DataPropertyName = "C4";
            this.C4.HeaderText = "X";
            this.C4.Name = "C4";
            this.C4.Width = 60;
            // 
            // saveProfileBtn
            // 
            this.saveProfileBtn.Location = new System.Drawing.Point(150, 193);
            this.saveProfileBtn.Name = "saveProfileBtn";
            this.saveProfileBtn.Size = new System.Drawing.Size(75, 23);
            this.saveProfileBtn.TabIndex = 8;
            this.saveProfileBtn.Text = "Save...";
            this.toolTip.SetToolTip(this.saveProfileBtn, "Save color matrix to disk file.");
            this.saveProfileBtn.UseVisualStyleBackColor = true;
            this.saveProfileBtn.Click += new System.EventHandler(this.saveFilterBtn_Click);
            // 
            // loadProfileBtn
            // 
            this.loadProfileBtn.Location = new System.Drawing.Point(69, 193);
            this.loadProfileBtn.Name = "loadProfileBtn";
            this.loadProfileBtn.Size = new System.Drawing.Size(75, 23);
            this.loadProfileBtn.TabIndex = 9;
            this.loadProfileBtn.Text = "Load...";
            this.toolTip.SetToolTip(this.loadProfileBtn, "Load previously save color matrix.");
            this.loadProfileBtn.UseVisualStyleBackColor = true;
            this.loadProfileBtn.Click += new System.EventHandler(this.loadFilterBtn_Click);
            // 
            // loadImageDialog
            // 
            this.loadImageDialog.FileName = "openFileDialog";
            this.loadImageDialog.Filter = "Png|*.png|Gif|*.gif|Jpg|*.jpg|All|*.*";
            this.loadImageDialog.FilterIndex = 4;
            this.loadImageDialog.InitialDirectory = ".\\";
            this.loadImageDialog.Title = "Load Image";
            // 
            // loadFilterDialog
            // 
            this.loadFilterDialog.FileName = "openFileDialog1";
            this.loadFilterDialog.Filter = "Xml|*.xml|Any|*.*";
            this.loadFilterDialog.InitialDirectory = ".\\";
            this.loadFilterDialog.Title = "Load Filter";
            // 
            // saveFilterDialog
            // 
            this.saveFilterDialog.Filter = "Xml|*.xml|Any|*.*";
            this.saveFilterDialog.InitialDirectory = ".\\";
            this.saveFilterDialog.Title = "Save Filter";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(293, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 320);
            this.label1.TabIndex = 19;
            this.label1.Text = "+";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(606, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 320);
            this.label2.TabIndex = 20;
            this.label2.Text = "=";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // filterFlow
            // 
            this.filterFlow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.filterFlow.AutoScroll = true;
            this.filterFlow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.filterFlow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.filterFlow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterFlow.Location = new System.Drawing.Point(11, 19);
            this.filterFlow.Name = "filterFlow";
            this.filterFlow.Size = new System.Drawing.Size(425, 223);
            this.filterFlow.TabIndex = 21;
            this.filterFlow.WrapContents = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "C0";
            this.dataGridViewTextBoxColumn1.HeaderText = "Red";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 60;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "C1";
            this.dataGridViewTextBoxColumn2.HeaderText = "Green";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 60;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "C2";
            this.dataGridViewTextBoxColumn3.HeaderText = "Blue";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 60;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "C3";
            this.dataGridViewTextBoxColumn4.HeaderText = "Alpha";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 60;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "C4";
            this.dataGridViewTextBoxColumn5.HeaderText = "X";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 60;
            // 
            // numBar
            // 
            this.numBar.BackColor = System.Drawing.Color.LightGray;
            this.numBar.LargeChange = 10;
            this.numBar.Location = new System.Drawing.Point(63, 223);
            this.numBar.Maximum = 100;
            this.numBar.Name = "numBar";
            this.numBar.Size = new System.Drawing.Size(344, 45);
            this.numBar.TabIndex = 22;
            this.numBar.TickFrequency = 10;
            this.toolTip.SetToolTip(this.numBar, "Select one or more grid cells and use slider to adjust their values.");
            this.numBar.Value = 50;
            this.numBar.Scroll += new System.EventHandler(this.numBar_Scroll);
            // 
            // numMaxBox
            // 
            this.numMaxBox.DecimalPlaces = 2;
            this.numMaxBox.Location = new System.Drawing.Point(413, 220);
            this.numMaxBox.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numMaxBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numMaxBox.Name = "numMaxBox";
            this.numMaxBox.Size = new System.Drawing.Size(51, 20);
            this.numMaxBox.TabIndex = 23;
            this.toolTip.SetToolTip(this.numMaxBox, "Maximum Slider Value");
            this.numMaxBox.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // numMinBox
            // 
            this.numMinBox.DecimalPlaces = 2;
            this.numMinBox.Location = new System.Drawing.Point(6, 220);
            this.numMinBox.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numMinBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numMinBox.Name = "numMinBox";
            this.numMinBox.Size = new System.Drawing.Size(51, 20);
            this.numMinBox.TabIndex = 24;
            this.toolTip.SetToolTip(this.numMinBox, "Minimum slider value");
            // 
            // saveImageDialog
            // 
            this.saveImageDialog.Filter = "Png|*.png|Gif|*.gif|Jpg|*.jpg|All|*.*";
            this.saveImageDialog.InitialDirectory = ".\\";
            this.saveImageDialog.Title = "Save Image";
            // 
            // loadFiltersBtn
            // 
            this.loadFiltersBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.loadFiltersBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadFiltersBtn.Location = new System.Drawing.Point(349, 251);
            this.loadFiltersBtn.Name = "loadFiltersBtn";
            this.loadFiltersBtn.Size = new System.Drawing.Size(87, 23);
            this.loadFiltersBtn.TabIndex = 26;
            this.loadFiltersBtn.Text = "Load Filters...";
            this.toolTip.SetToolTip(this.loadFiltersBtn, "Load one or more color matrix filters..");
            this.loadFiltersBtn.UseVisualStyleBackColor = true;
            this.loadFiltersBtn.Click += new System.EventHandler(this.loadFiltersBtn_Click);
            // 
            // gridGroupOut
            // 
            this.gridGroupOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gridGroupOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gridGroupOut.Controls.Add(this.gridGroupIn);
            this.gridGroupOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridGroupOut.Location = new System.Drawing.Point(0, 336);
            this.gridGroupOut.Margin = new System.Windows.Forms.Padding(10);
            this.gridGroupOut.Name = "gridGroupOut";
            this.gridGroupOut.Padding = new System.Windows.Forms.Padding(10);
            this.gridGroupOut.Size = new System.Drawing.Size(470, 285);
            this.gridGroupOut.TabIndex = 27;
            this.gridGroupOut.TabStop = false;
            this.gridGroupOut.Text = "Active Filter Applied to Overlay Image";
            // 
            // gridGroupIn
            // 
            this.gridGroupIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gridGroupIn.BackColor = System.Drawing.Color.Transparent;
            this.gridGroupIn.Controls.Add(this.numBar);
            this.gridGroupIn.Controls.Add(this.numMinBox);
            this.gridGroupIn.Controls.Add(this.numMaxBox);
            this.gridGroupIn.Controls.Add(this.loadProfileBtn);
            this.gridGroupIn.Controls.Add(this.saveProfileBtn);
            this.gridGroupIn.Controls.Add(this.filterGrid);
            this.gridGroupIn.Controls.Add(this.applyBtn);
            this.gridGroupIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridGroupIn.Location = new System.Drawing.Point(0, 11);
            this.gridGroupIn.Margin = new System.Windows.Forms.Padding(0);
            this.gridGroupIn.Name = "gridGroupIn";
            this.gridGroupIn.Size = new System.Drawing.Size(470, 274);
            this.gridGroupIn.TabIndex = 27;
            this.gridGroupIn.Text = "A";
            // 
            // saveFiltersBtn
            // 
            this.saveFiltersBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveFiltersBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveFiltersBtn.Location = new System.Drawing.Point(243, 251);
            this.saveFiltersBtn.Name = "saveFiltersBtn";
            this.saveFiltersBtn.Size = new System.Drawing.Size(87, 23);
            this.saveFiltersBtn.TabIndex = 28;
            this.saveFiltersBtn.Text = "Save Gzips...";
            this.saveFiltersBtn.UseVisualStyleBackColor = true;
            this.saveFiltersBtn.Visible = false;
            this.saveFiltersBtn.Click += new System.EventHandler(this.saveFiltersBtn_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(150, 251);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 23);
            this.button1.TabIndex = 29;
            this.button1.Text = "Load Gzip...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.LoadCompressedFiltersBtn_Click);
            // 
            // aboutBtn
            // 
            this.aboutBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.aboutBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("aboutBtn.BackgroundImage")));
            this.aboutBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.aboutBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aboutBtn.ForeColor = System.Drawing.Color.White;
            this.aboutBtn.Location = new System.Drawing.Point(758, 627);
            this.aboutBtn.Name = "aboutBtn";
            this.aboutBtn.Size = new System.Drawing.Size(73, 26);
            this.aboutBtn.TabIndex = 33;
            this.aboutBtn.Text = "About";
            this.toolTip.SetToolTip(this.aboutBtn, "About Information");
            this.aboutBtn.UseVisualStyleBackColor = true;
            this.aboutBtn.Click += new System.EventHandler(this.aboutBtn_Click);
            // 
            // helpBtn
            // 
            this.helpBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.helpBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("helpBtn.BackgroundImage")));
            this.helpBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.helpBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpBtn.ForeColor = System.Drawing.Color.White;
            this.helpBtn.Location = new System.Drawing.Point(679, 627);
            this.helpBtn.Name = "helpBtn";
            this.helpBtn.Size = new System.Drawing.Size(73, 26);
            this.helpBtn.TabIndex = 34;
            this.helpBtn.Text = "Help";
            this.toolTip.SetToolTip(this.helpBtn, "Help Information");
            this.helpBtn.UseVisualStyleBackColor = true;
            this.helpBtn.Click += new System.EventHandler(this.helpBtn_Click);
            // 
            // maxIcon
            // 
            this.maxIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.maxIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.maxIcon.BackgroundImage = global::ColorMatrix_ns.Properties.Resources.redDonut48;
            this.maxIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.maxIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.maxIcon.Location = new System.Drawing.Point(889, 1);
            this.maxIcon.Margin = new System.Windows.Forms.Padding(0);
            this.maxIcon.Name = "maxIcon";
            this.maxIcon.Size = new System.Drawing.Size(24, 24);
            this.maxIcon.TabIndex = 3;
            this.toolTip.SetToolTip(this.maxIcon, "Maximize ");
            this.maxIcon.Click += new System.EventHandler(this.maxIcon_Click);
            // 
            // minIcon
            // 
            this.minIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.minIcon.BackgroundImage = global::ColorMatrix_ns.Properties.Resources.redCurve48;
            this.minIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.minIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.minIcon.Location = new System.Drawing.Point(863, 1);
            this.minIcon.Margin = new System.Windows.Forms.Padding(0);
            this.minIcon.Name = "minIcon";
            this.minIcon.Size = new System.Drawing.Size(24, 24);
            this.minIcon.TabIndex = 2;
            this.toolTip.SetToolTip(this.minIcon, "Minimize");
            this.minIcon.Click += new System.EventHandler(this.minIcon_Click);
            // 
            // icon
            // 
            this.icon.BackgroundImage = global::ColorMatrix_ns.Properties.Resources.ColorMatrix;
            this.icon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.icon.Location = new System.Drawing.Point(0, 0);
            this.icon.Margin = new System.Windows.Forms.Padding(0);
            this.icon.Name = "icon";
            this.icon.Size = new System.Drawing.Size(26, 26);
            this.icon.TabIndex = 0;
            this.toolTip.SetToolTip(this.icon, "About Info");
            this.icon.Click += new System.EventHandler(this.aboutBtn_Click);
            // 
            // filterGalleryGrp
            // 
            this.filterGalleryGrp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.filterGalleryGrp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.filterGalleryGrp.Controls.Add(this.button1);
            this.filterGalleryGrp.Controls.Add(this.saveFiltersBtn);
            this.filterGalleryGrp.Controls.Add(this.loadFiltersBtn);
            this.filterGalleryGrp.Controls.Add(this.filterFlow);
            this.filterGalleryGrp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterGalleryGrp.Location = new System.Drawing.Point(483, 336);
            this.filterGalleryGrp.Name = "filterGalleryGrp";
            this.filterGalleryGrp.Size = new System.Drawing.Size(443, 285);
            this.filterGalleryGrp.TabIndex = 33;
            this.filterGalleryGrp.TabStop = false;
            this.filterGalleryGrp.Text = "Filter Gallery";
            // 
            // imageTable
            // 
            this.imageTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.imageTable.BackColor = System.Drawing.Color.Transparent;
            this.imageTable.ColumnCount = 5;
            this.imageTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.imageTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.imageTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.imageTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.imageTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.imageTable.Controls.Add(this.ipanel1, 0, 0);
            this.imageTable.Controls.Add(this.label1, 1, 0);
            this.imageTable.Controls.Add(this.label2, 3, 0);
            this.imageTable.Controls.Add(this.ipanel2, 2, 0);
            this.imageTable.Controls.Add(this.ipanel3, 4, 0);
            this.imageTable.Location = new System.Drawing.Point(3, 3);
            this.imageTable.Name = "imageTable";
            this.imageTable.RowCount = 1;
            this.imageTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.imageTable.Size = new System.Drawing.Size(920, 320);
            this.imageTable.TabIndex = 34;
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.Navy;
            this.mainPanel.BackgroundImage = global::ColorMatrix_ns.Properties.Resources.ColorMatrixBg;
            this.mainPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.mainPanel.Controls.Add(this.closeBtn);
            this.mainPanel.Controls.Add(this.aboutBtn);
            this.mainPanel.Controls.Add(this.helpBtn);
            this.mainPanel.Controls.Add(this.filterGalleryGrp);
            this.mainPanel.Controls.Add(this.imageTable);
            this.mainPanel.Controls.Add(this.gridGroupOut);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(23, 29);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(926, 655);
            this.mainPanel.TabIndex = 0;
            // 
            // mainTable
            // 
            this.mainTable.BackColor = System.Drawing.Color.Navy;
            this.mainTable.ColumnCount = 3;
            this.mainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainTable.Controls.Add(this.mainPanel, 1, 1);
            this.mainTable.Controls.Add(this.topPanel, 1, 0);
            this.mainTable.Controls.Add(this.rightPanel, 2, 1);
            this.mainTable.Controls.Add(this.botPanel, 1, 2);
            this.mainTable.Controls.Add(this.leftPanel, 0, 1);
            this.mainTable.Controls.Add(this.lrPanel, 2, 2);
            this.mainTable.Controls.Add(this.llPanel, 0, 2);
            this.mainTable.Controls.Add(this.ulpanel, 0, 0);
            this.mainTable.Controls.Add(this.panel4, 2, 0);
            this.mainTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTable.ForeColor = System.Drawing.Color.Black;
            this.mainTable.Location = new System.Drawing.Point(0, 0);
            this.mainTable.Name = "mainTable";
            this.mainTable.RowCount = 3;
            this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainTable.Size = new System.Drawing.Size(972, 707);
            this.mainTable.TabIndex = 35;
            // 
            // topPanel
            // 
            this.topPanel.BackgroundImage = global::ColorMatrix_ns.Properties.Resources.top8;
            this.topPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.topPanel.Controls.Add(this.maxIcon);
            this.topPanel.Controls.Add(this.minIcon);
            this.topPanel.Controls.Add(this.icon);
            this.topPanel.Controls.Add(this.title);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topPanel.Location = new System.Drawing.Point(20, 0);
            this.topPanel.Margin = new System.Windows.Forms.Padding(0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(932, 26);
            this.topPanel.TabIndex = 1;
            this.topPanel.MouseLeave += new System.EventHandler(this.MouseLeave_Click);
            this.topPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMove_Click);
            this.topPanel.MouseEnter += new System.EventHandler(this.MouseEnter_Click);
            // 
            // title
            // 
            this.title.BackColor = System.Drawing.Color.Transparent;
            this.title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.ForeColor = System.Drawing.Color.White;
            this.title.Location = new System.Drawing.Point(0, 0);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(932, 26);
            this.title.TabIndex = 1;
            this.title.Text = "ColorMatrix v1.2";
            this.title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.title.MouseLeave += new System.EventHandler(this.MouseLeave_Click);
            this.title.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMove_Click);
            this.title.MouseEnter += new System.EventHandler(this.MouseEnter_Click);
            // 
            // rightPanel
            // 
            this.rightPanel.BackgroundImage = global::ColorMatrix_ns.Properties.Resources.right8;
            this.rightPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel.Location = new System.Drawing.Point(952, 26);
            this.rightPanel.Margin = new System.Windows.Forms.Padding(0);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(20, 661);
            this.rightPanel.TabIndex = 2;
            // 
            // botPanel
            // 
            this.botPanel.BackgroundImage = global::ColorMatrix_ns.Properties.Resources.bot8;
            this.botPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.botPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.botPanel.Location = new System.Drawing.Point(20, 687);
            this.botPanel.Margin = new System.Windows.Forms.Padding(0);
            this.botPanel.Name = "botPanel";
            this.botPanel.Size = new System.Drawing.Size(932, 20);
            this.botPanel.TabIndex = 3;
            // 
            // leftPanel
            // 
            this.leftPanel.BackgroundImage = global::ColorMatrix_ns.Properties.Resources.left8;
            this.leftPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftPanel.Location = new System.Drawing.Point(0, 26);
            this.leftPanel.Margin = new System.Windows.Forms.Padding(0);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(20, 661);
            this.leftPanel.TabIndex = 4;
            // 
            // lrPanel
            // 
            this.lrPanel.BackgroundImage = global::ColorMatrix_ns.Properties.Resources.lr20;
            this.lrPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.lrPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lrPanel.Location = new System.Drawing.Point(952, 687);
            this.lrPanel.Margin = new System.Windows.Forms.Padding(0);
            this.lrPanel.Name = "lrPanel";
            this.lrPanel.Size = new System.Drawing.Size(20, 20);
            this.lrPanel.TabIndex = 5;
            this.lrPanel.Tag = "lowerright";
            this.lrPanel.MouseLeave += new System.EventHandler(this.MouseLeave_Click);
            this.lrPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseSizeMove_Click);
            this.lrPanel.MouseEnter += new System.EventHandler(this.MouseSizeEnter_Click);
            // 
            // llPanel
            // 
            this.llPanel.BackgroundImage = global::ColorMatrix_ns.Properties.Resources.ll20;
            this.llPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.llPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.llPanel.Location = new System.Drawing.Point(0, 687);
            this.llPanel.Margin = new System.Windows.Forms.Padding(0);
            this.llPanel.Name = "llPanel";
            this.llPanel.Size = new System.Drawing.Size(20, 20);
            this.llPanel.TabIndex = 6;
            this.llPanel.Tag = "lowerleft";
            this.llPanel.MouseLeave += new System.EventHandler(this.MouseLeave_Click);
            this.llPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseSizeMove_Click);
            this.llPanel.MouseEnter += new System.EventHandler(this.MouseSizeEnter_Click);
            // 
            // ulpanel
            // 
            this.ulpanel.BackgroundImage = global::ColorMatrix_ns.Properties.Resources.ul20;
            this.ulpanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ulpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ulpanel.Location = new System.Drawing.Point(0, 0);
            this.ulpanel.Margin = new System.Windows.Forms.Padding(0);
            this.ulpanel.Name = "ulpanel";
            this.ulpanel.Size = new System.Drawing.Size(20, 26);
            this.ulpanel.TabIndex = 7;
            this.ulpanel.Tag = "upperleft";
            this.ulpanel.MouseLeave += new System.EventHandler(this.MouseLeave_Click);
            this.ulpanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseSizeMove_Click);
            this.ulpanel.MouseEnter += new System.EventHandler(this.MouseSizeEnter_Click);
            // 
            // panel4
            // 
            this.panel4.BackgroundImage = global::ColorMatrix_ns.Properties.Resources.url20;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(952, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(20, 26);
            this.panel4.TabIndex = 8;
            this.panel4.Tag = "upperright";
            this.panel4.MouseLeave += new System.EventHandler(this.MouseLeave_Click);
            this.panel4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseSizeMove_Click);
            this.panel4.MouseEnter += new System.EventHandler(this.MouseSizeEnter_Click);
            // 
            // ipanel1
            // 
            this.ipanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ipanel1.Image = global::ColorMatrix_ns.Properties.Resources.image3;
            this.ipanel1.Label = "Background";
            this.ipanel1.Location = new System.Drawing.Point(3, 3);
            this.ipanel1.Name = "ipanel1";
            this.ipanel1.Size = new System.Drawing.Size(287, 314);
            this.ipanel1.TabIndex = 0;
            this.toolTip.SetToolTip(this.ipanel1, "Background image");
            // 
            // ipanel2
            // 
            this.ipanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ipanel2.Image = global::ColorMatrix_ns.Properties.Resources.image2;
            this.ipanel2.Label = "Overlay";
            this.ipanel2.Location = new System.Drawing.Point(316, 3);
            this.ipanel2.Name = "ipanel2";
            this.ipanel2.Size = new System.Drawing.Size(287, 314);
            this.ipanel2.TabIndex = 1;
            this.toolTip.SetToolTip(this.ipanel2, "Overlay image");
            // 
            // ipanel3
            // 
            this.ipanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ipanel3.Image = global::ColorMatrix_ns.Properties.Resources.image2;
            this.ipanel3.Label = "Background + Overlay(ColorMatrix)";
            this.ipanel3.Location = new System.Drawing.Point(629, 3);
            this.ipanel3.Name = "ipanel3";
            this.ipanel3.Size = new System.Drawing.Size(288, 314);
            this.ipanel3.TabIndex = 2;
            this.toolTip.SetToolTip(this.ipanel3, "Background image merged with Overlay image.\r\nColorMatrix is applied to Overlay im" +
                    "age .\r\n");
            // 
            // filterGrid
            // 
            this.filterGrid.Image = null;
            this.filterGrid.Label = "";
            this.filterGrid.LabelBackColor = System.Drawing.SystemColors.Control;
            this.filterGrid.Location = new System.Drawing.Point(8, 17);
            this.filterGrid.Name = "filterGrid";
            this.filterGrid.Size = new System.Drawing.Size(456, 171);
            this.filterGrid.Source = null;
            this.filterGrid.TabIndex = 6;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Navy;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(972, 707);
            this.Controls.Add(this.mainTable);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "ColorMatrix v1.0";
            ((System.ComponentModel.ISupportInitialize)(this.numBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinBox)).EndInit();
            this.gridGroupOut.ResumeLayout(false);
            this.gridGroupIn.ResumeLayout(false);
            this.gridGroupIn.PerformLayout();
            this.filterGalleryGrp.ResumeLayout(false);
            this.imageTable.ResumeLayout(false);
            this.imageTable.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.mainTable.ResumeLayout(false);
            this.topPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ImagePanel ipanel1;
        private ImagePanel ipanel2;
        private ImagePanel ipanel3;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button applyBtn;
        private ColorMatrix_ns.FilterGrid filterGrid;
        private System.Windows.Forms.Button saveProfileBtn;
        private System.Windows.Forms.Button loadProfileBtn;
        private System.Windows.Forms.OpenFileDialog loadImageDialog;
        private System.Windows.Forms.OpenFileDialog loadFilterDialog;
        private System.Windows.Forms.SaveFileDialog saveFilterDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn C0;
        private System.Windows.Forms.DataGridViewTextBoxColumn C1;
        private System.Windows.Forms.DataGridViewTextBoxColumn C2;
        private System.Windows.Forms.DataGridViewTextBoxColumn C3;
        private System.Windows.Forms.DataGridViewTextBoxColumn C4;
        private System.Windows.Forms.FlowLayoutPanel filterFlow;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.TrackBar numBar;
        private System.Windows.Forms.NumericUpDown numMaxBox;
        private System.Windows.Forms.NumericUpDown numMinBox;
        private System.Windows.Forms.SaveFileDialog saveImageDialog;
        private System.Windows.Forms.Button loadFiltersBtn;
        private System.Windows.Forms.GroupBox gridGroupOut;
        private System.Windows.Forms.Panel gridGroupIn;
        private System.Windows.Forms.Button saveFiltersBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button aboutBtn;
        private System.Windows.Forms.Button helpBtn;
        private System.Windows.Forms.GroupBox filterGalleryGrp;
        private System.Windows.Forms.HelpProvider helpProvider;
        private System.Windows.Forms.TableLayoutPanel imageTable;
        private System.Windows.Forms.TableLayoutPanel mainTable;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel icon;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.Panel botPanel;
        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.Panel lrPanel;
        private System.Windows.Forms.Panel llPanel;
        private System.Windows.Forms.Panel ulpanel;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Panel minIcon;
        private System.Windows.Forms.Panel maxIcon;
    }
}

