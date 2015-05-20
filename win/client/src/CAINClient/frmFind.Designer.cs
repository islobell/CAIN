namespace CAINClient
{
    partial class frmFind
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFind));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvView = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.chkSearchCover = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.gbxOptions = new System.Windows.Forms.GroupBox();
            this.sbxTolerance = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.chkIgnoreTitle = new System.Windows.Forms.CheckBox();
            this.chkIgnoreAlbum = new System.Windows.Forms.CheckBox();
            this.chkIgnoreArtists = new System.Windows.Forms.CheckBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblAlbum = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtAlbum = new System.Windows.Forms.TextBox();
            this.lblArtists = new System.Windows.Forms.Label();
            this.txtArtists = new System.Windows.Forms.TextBox();
            this.lblCover = new System.Windows.Forms.Label();
            this.pbxCover = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.lblDuration = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider4 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider5 = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvView)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.gbxOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sbxTolerance)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCover)).BeginInit();
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider5)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvView, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel6, 0, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 215F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(799, 562);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label5, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(15, 15);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(15, 15, 15, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(769, 15);
            this.tableLayoutPanel5.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(175, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Paso 1. Buscar coincidencias";
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(181, 7);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 7, 0, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(588, 2);
            this.label5.TabIndex = 1;
            this.label5.Text = "label5";
            // 
            // dgvView
            // 
            this.dgvView.AllowUserToAddRows = false;
            this.dgvView.AllowUserToDeleteRows = false;
            this.dgvView.AllowUserToOrderColumns = true;
            this.dgvView.AllowUserToResizeRows = false;
            this.dgvView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dgvView.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvView.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgvView.Location = new System.Drawing.Point(15, 110);
            this.dgvView.Margin = new System.Windows.Forms.Padding(15, 0, 15, 15);
            this.dgvView.MultiSelect = false;
            this.dgvView.Name = "dgvView";
            this.dgvView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvView.Size = new System.Drawing.Size(769, 155);
            this.dgvView.TabIndex = 0;
            this.dgvView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvView_CellDoubleClick);
            this.dgvView.SelectionChanged += new System.EventHandler(this.dgvView_SelectionChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.chkSearchCover, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnCancel, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnOK, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 510);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(799, 52);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // chkSearchCover
            // 
            this.chkSearchCover.AutoSize = true;
            this.chkSearchCover.Checked = true;
            this.chkSearchCover.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSearchCover.Location = new System.Drawing.Point(15, 12);
            this.chkSearchCover.Margin = new System.Windows.Forms.Padding(15, 10, 0, 0);
            this.chkSearchCover.Name = "chkSearchCover";
            this.chkSearchCover.Size = new System.Drawing.Size(135, 17);
            this.chkSearchCover.TabIndex = 4;
            this.chkSearchCover.Text = "Buscar carátula al salir.";
            this.chkSearchCover.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.CausesValidation = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::CAINClient.Properties.Resources.cancel;
            this.btnCancel.Location = new System.Drawing.Point(709, 12);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(10, 10, 15, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::CAINClient.Properties.Resources.accept;
            this.btnOK.Location = new System.Drawing.Point(624, 12);
            this.btnOK.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "Aceptar";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tableLayoutPanel2.SetColumnSpan(this.label6, 3);
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(15, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(769, 2);
            this.label6.TabIndex = 5;
            this.label6.Text = "label6";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.gbxOptions, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnSearch, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 30);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(799, 80);
            this.tableLayoutPanel3.TabIndex = 6;
            // 
            // gbxOptions
            // 
            this.gbxOptions.Controls.Add(this.sbxTolerance);
            this.gbxOptions.Controls.Add(this.label1);
            this.gbxOptions.Controls.Add(this.chkIgnoreTitle);
            this.gbxOptions.Controls.Add(this.chkIgnoreAlbum);
            this.gbxOptions.Controls.Add(this.chkIgnoreArtists);
            this.gbxOptions.Location = new System.Drawing.Point(15, 10);
            this.gbxOptions.Margin = new System.Windows.Forms.Padding(15, 10, 15, 15);
            this.gbxOptions.Name = "gbxOptions";
            this.gbxOptions.Size = new System.Drawing.Size(669, 50);
            this.gbxOptions.TabIndex = 2;
            this.gbxOptions.TabStop = false;
            this.gbxOptions.Text = "Opciones";
            // 
            // sbxTolerance
            // 
            this.sbxTolerance.Location = new System.Drawing.Point(569, 19);
            this.sbxTolerance.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.sbxTolerance.Name = "sbxTolerance";
            this.sbxTolerance.Size = new System.Drawing.Size(60, 20);
            this.sbxTolerance.TabIndex = 3;
            this.sbxTolerance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.sbxTolerance.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(503, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tolerancia:";
            // 
            // chkIgnoreTitle
            // 
            this.chkIgnoreTitle.AutoSize = true;
            this.chkIgnoreTitle.Location = new System.Drawing.Point(45, 21);
            this.chkIgnoreTitle.Name = "chkIgnoreTitle";
            this.chkIgnoreTitle.Size = new System.Drawing.Size(90, 17);
            this.chkIgnoreTitle.TabIndex = 0;
            this.chkIgnoreTitle.Text = "Ignorar Título";
            this.chkIgnoreTitle.UseVisualStyleBackColor = true;
            // 
            // chkIgnoreAlbum
            // 
            this.chkIgnoreAlbum.AutoSize = true;
            this.chkIgnoreAlbum.Location = new System.Drawing.Point(393, 21);
            this.chkIgnoreAlbum.Name = "chkIgnoreAlbum";
            this.chkIgnoreAlbum.Size = new System.Drawing.Size(91, 17);
            this.chkIgnoreAlbum.TabIndex = 0;
            this.chkIgnoreAlbum.Text = "Ignorar Álbum";
            this.chkIgnoreAlbum.UseVisualStyleBackColor = true;
            this.chkIgnoreAlbum.CheckedChanged += new System.EventHandler(this.chkIgnoreAlbum_CheckedChanged);
            // 
            // chkIgnoreArtists
            // 
            this.chkIgnoreArtists.AutoSize = true;
            this.chkIgnoreArtists.Location = new System.Drawing.Point(213, 21);
            this.chkIgnoreArtists.Name = "chkIgnoreArtists";
            this.chkIgnoreArtists.Size = new System.Drawing.Size(102, 17);
            this.chkIgnoreArtists.TabIndex = 0;
            this.chkIgnoreArtists.Text = "Ignorar Artista(s)";
            this.chkIgnoreArtists.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Image = global::CAINClient.Properties.Resources.magnifier;
            this.btnSearch.Location = new System.Drawing.Point(699, 15);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(0, 15, 15, 15);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(85, 45);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Buscar";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(15, 280);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(769, 15);
            this.tableLayoutPanel4.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Paso 2. Validar datos";
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(134, 7);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 7, 0, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(635, 2);
            this.label3.TabIndex = 1;
            this.label3.Text = "label3";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 5;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.lblTitle, 2, 0);
            this.tableLayoutPanel6.Controls.Add(this.lblAlbum, 2, 2);
            this.tableLayoutPanel6.Controls.Add(this.txtTitle, 2, 1);
            this.tableLayoutPanel6.Controls.Add(this.txtAlbum, 2, 3);
            this.tableLayoutPanel6.Controls.Add(this.lblArtists, 2, 5);
            this.tableLayoutPanel6.Controls.Add(this.txtArtists, 2, 6);
            this.tableLayoutPanel6.Controls.Add(this.lblCover, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.pbxCover, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 2, 4);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(15, 305);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(15, 10, 15, 15);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 7;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.Size = new System.Drawing.Size(769, 190);
            this.tableLayoutPanel6.TabIndex = 9;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Location = new System.Drawing.Point(325, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(15, 0, 3, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(276, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Título";
            // 
            // lblAlbum
            // 
            this.lblAlbum.AutoSize = true;
            this.lblAlbum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAlbum.Location = new System.Drawing.Point(325, 49);
            this.lblAlbum.Margin = new System.Windows.Forms.Padding(15, 10, 3, 0);
            this.lblAlbum.Name = "lblAlbum";
            this.lblAlbum.Size = new System.Drawing.Size(276, 13);
            this.lblAlbum.TabIndex = 1;
            this.lblAlbum.Text = "Álbum";
            // 
            // txtTitle
            // 
            this.txtTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTitle.Location = new System.Drawing.Point(325, 16);
            this.txtTitle.Margin = new System.Windows.Forms.Padding(15, 3, 3, 3);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(276, 20);
            this.txtTitle.TabIndex = 2;
            this.txtTitle.TextChanged += new System.EventHandler(this.txtTitle_TextChanged);
            // 
            // txtAlbum
            // 
            this.txtAlbum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAlbum.Location = new System.Drawing.Point(325, 65);
            this.txtAlbum.Margin = new System.Windows.Forms.Padding(15, 3, 3, 3);
            this.txtAlbum.Name = "txtAlbum";
            this.txtAlbum.Size = new System.Drawing.Size(276, 20);
            this.txtAlbum.TabIndex = 3;
            this.txtAlbum.TextChanged += new System.EventHandler(this.txtAlbum_TextChanged);
            // 
            // lblArtists
            // 
            this.lblArtists.AutoSize = true;
            this.lblArtists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblArtists.Location = new System.Drawing.Point(325, 148);
            this.lblArtists.Margin = new System.Windows.Forms.Padding(15, 10, 3, 0);
            this.lblArtists.Name = "lblArtists";
            this.lblArtists.Size = new System.Drawing.Size(276, 13);
            this.lblArtists.TabIndex = 4;
            this.lblArtists.Text = "Artista(s)";
            // 
            // txtArtists
            // 
            this.txtArtists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtArtists.Location = new System.Drawing.Point(325, 164);
            this.txtArtists.Margin = new System.Windows.Forms.Padding(15, 3, 3, 3);
            this.txtArtists.Name = "txtArtists";
            this.txtArtists.Size = new System.Drawing.Size(276, 20);
            this.txtArtists.TabIndex = 5;
            this.txtArtists.TextChanged += new System.EventHandler(this.txtArtists_TextChanged);
            // 
            // lblCover
            // 
            this.lblCover.AutoSize = true;
            this.lblCover.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCover.Location = new System.Drawing.Point(167, 0);
            this.lblCover.Name = "lblCover";
            this.lblCover.Size = new System.Drawing.Size(140, 13);
            this.lblCover.TabIndex = 6;
            this.lblCover.Text = "Carátula";
            // 
            // pbxCover
            // 
            this.pbxCover.BackColor = System.Drawing.SystemColors.Window;
            this.pbxCover.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxCover.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxCover.Location = new System.Drawing.Point(167, 16);
            this.pbxCover.Name = "pbxCover";
            this.tableLayoutPanel6.SetRowSpan(this.pbxCover, 5);
            this.pbxCover.Size = new System.Drawing.Size(140, 140);
            this.pbxCover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxCover.TabIndex = 7;
            this.pbxCover.TabStop = false;
            this.pbxCover.Click += new System.EventHandler(this.pbxCover_Click);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.lblDuration, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.lblYear, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.txtDuration, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.txtYear, 1, 1);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(310, 88);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.Size = new System.Drawing.Size(294, 50);
            this.tableLayoutPanel7.TabIndex = 12;
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(15, 10);
            this.lblDuration.Margin = new System.Windows.Forms.Padding(15, 10, 3, 0);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(50, 13);
            this.lblDuration.TabIndex = 8;
            this.lblDuration.Text = "Duración";
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(147, 10);
            this.lblYear.Margin = new System.Windows.Forms.Padding(0, 10, 3, 0);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(26, 13);
            this.lblYear.TabIndex = 10;
            this.lblYear.Text = "Año";
            // 
            // txtDuration
            // 
            this.txtDuration.Location = new System.Drawing.Point(15, 26);
            this.txtDuration.Margin = new System.Windows.Forms.Padding(15, 3, 3, 3);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(80, 20);
            this.txtDuration.TabIndex = 9;
            this.txtDuration.TextChanged += new System.EventHandler(this.txtDuration_TextChanged);
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(147, 26);
            this.txtYear.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(80, 20);
            this.txtYear.TabIndex = 11;
            this.txtYear.TextChanged += new System.EventHandler(this.txtYear_TextChanged);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 500;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // errorProvider3
            // 
            this.errorProvider3.ContainerControl = this;
            // 
            // errorProvider4
            // 
            this.errorProvider4.ContainerControl = this;
            // 
            // errorProvider5
            // 
            this.errorProvider5.ContainerControl = this;
            // 
            // frmFind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(799, 562);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmFind";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Asistente de catalogación";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvView)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.gbxOptions.ResumeLayout(false);
            this.gbxOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sbxTolerance)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCover)).EndInit();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgvView;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox gbxOptions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkIgnoreTitle;
        private System.Windows.Forms.CheckBox chkIgnoreAlbum;
        private System.Windows.Forms.CheckBox chkIgnoreArtists;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.NumericUpDown sbxTolerance;
        private System.Windows.Forms.CheckBox chkSearchCover;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblAlbum;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtAlbum;
        private System.Windows.Forms.Label lblArtists;
        private System.Windows.Forms.TextBox txtArtists;
        private System.Windows.Forms.Label lblCover;
        private System.Windows.Forms.PictureBox pbxCover;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private System.Windows.Forms.ErrorProvider errorProvider4;
        private System.Windows.Forms.ErrorProvider errorProvider5;
    }
}