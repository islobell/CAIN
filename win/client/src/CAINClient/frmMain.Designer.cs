namespace CAINClient
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menu = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileScan = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFileColumns = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFilePending = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileFind = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFileConfirm = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFilePlaylist = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.servicioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuServiceInstall = new System.Windows.Forms.ToolStripMenuItem();
            this.menuServiceUninstall = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuServiceStart = new System.Windows.Forms.ToolStripMenuItem();
            this.menuServiceStop = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuServiceSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.toolFileScan = new System.Windows.Forms.ToolStripButton();
            this.toolFileUpdate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolFileColumns = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolFileSave = new System.Windows.Forms.ToolStripButton();
            this.toolFileFind = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolFilePending = new System.Windows.Forms.ToolStripButton();
            this.toolFileConfirm = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolFilePlaylist = new System.Windows.Forms.ToolStripButton();
            this.toolPattern = new System.Windows.Forms.ToolStripTextBox();
            this.toolOperations = new System.Windows.Forms.ToolStripComboBox();
            this.toolColumns = new System.Windows.Forms.ToolStripComboBox();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusCataloged = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel8 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusNoCataloged = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusSelected = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusDisplayed = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider4 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider5 = new System.Windows.Forms.ErrorProvider(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblAlbum = new System.Windows.Forms.Label();
            this.txtAlbum = new System.Windows.Forms.TextBox();
            this.lblDuration = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.lblArtists = new System.Windows.Forms.Label();
            this.txtArtists = new System.Windows.Forms.TextBox();
            this.pbxCover = new System.Windows.Forms.PictureBox();
            this.lblCover = new System.Windows.Forms.Label();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.lblTags = new System.Windows.Forms.Label();
            this.ltvTags = new System.Windows.Forms.ListView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnNewTag = new System.Windows.Forms.Button();
            this.btnEditTag = new System.Windows.Forms.Button();
            this.btnDeleteTag = new System.Windows.Forms.Button();
            this.btnAlbum = new System.Windows.Forms.Button();
            this.dgvView = new System.Windows.Forms.DataGridView();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuFind = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.menuAsign = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
            this.menuPlaylist = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
            this.toolBar.SuspendLayout();
            this.statusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCover)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvView)).BeginInit();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.servicioToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1284, 24);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFileScan,
            this.menuFileUpdate,
            this.toolStripMenuItem1,
            this.menuFileColumns,
            this.menuFilePending,
            this.toolStripMenuItem6,
            this.menuFileSave,
            this.menuFileFind,
            this.toolStripMenuItem2,
            this.menuFileConfirm,
            this.toolStripMenuItem3,
            this.menuFilePlaylist,
            this.toolStripMenuItem7,
            this.menuFileExit});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // menuFileScan
            // 
            this.menuFileScan.Image = global::CAINClient.Properties.Resources.folder_magnify;
            this.menuFileScan.Name = "menuFileScan";
            this.menuFileScan.Size = new System.Drawing.Size(225, 22);
            this.menuFileScan.Text = "Escanear carpetas...";
            this.menuFileScan.Click += new System.EventHandler(this.menuFileScan_Click);
            // 
            // menuFileUpdate
            // 
            this.menuFileUpdate.Image = global::CAINClient.Properties.Resources.arrow_refresh;
            this.menuFileUpdate.Name = "menuFileUpdate";
            this.menuFileUpdate.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.menuFileUpdate.Size = new System.Drawing.Size(225, 22);
            this.menuFileUpdate.Text = "Actualizar información";
            this.menuFileUpdate.Click += new System.EventHandler(this.menuFileUpdate_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(222, 6);
            // 
            // menuFileColumns
            // 
            this.menuFileColumns.Image = global::CAINClient.Properties.Resources.eye;
            this.menuFileColumns.Name = "menuFileColumns";
            this.menuFileColumns.Size = new System.Drawing.Size(225, 22);
            this.menuFileColumns.Text = "Mostrar columnas...";
            this.menuFileColumns.Click += new System.EventHandler(this.menuFileColumns_Click);
            // 
            // menuFilePending
            // 
            this.menuFilePending.Image = global::CAINClient.Properties.Resources.error;
            this.menuFilePending.Name = "menuFilePending";
            this.menuFilePending.Size = new System.Drawing.Size(225, 22);
            this.menuFilePending.Text = "Ver sólo archivos pendientes";
            this.menuFilePending.Click += new System.EventHandler(this.menuFilePending_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(222, 6);
            // 
            // menuFileSave
            // 
            this.menuFileSave.Image = global::CAINClient.Properties.Resources.disk;
            this.menuFileSave.Name = "menuFileSave";
            this.menuFileSave.Size = new System.Drawing.Size(225, 22);
            this.menuFileSave.Text = "Guardar cambios";
            this.menuFileSave.Click += new System.EventHandler(this.menuFileSave_Click);
            // 
            // menuFileFind
            // 
            this.menuFileFind.Image = global::CAINClient.Properties.Resources.wand;
            this.menuFileFind.Name = "menuFileFind";
            this.menuFileFind.Size = new System.Drawing.Size(225, 22);
            this.menuFileFind.Text = "Asistente de catalogación...";
            this.menuFileFind.Click += new System.EventHandler(this.menuFileFind_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(222, 6);
            // 
            // menuFileConfirm
            // 
            this.menuFileConfirm.Image = global::CAINClient.Properties.Resources.tag_green;
            this.menuFileConfirm.Name = "menuFileConfirm";
            this.menuFileConfirm.Size = new System.Drawing.Size(225, 22);
            this.menuFileConfirm.Text = "Confirmar catalogación";
            this.menuFileConfirm.Click += new System.EventHandler(this.menuFileConfirm_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(222, 6);
            // 
            // menuFilePlaylist
            // 
            this.menuFilePlaylist.Image = global::CAINClient.Properties.Resources.music;
            this.menuFilePlaylist.Name = "menuFilePlaylist";
            this.menuFilePlaylist.Size = new System.Drawing.Size(225, 22);
            this.menuFilePlaylist.Text = "Crear lista de reproducción...";
            this.menuFilePlaylist.Click += new System.EventHandler(this.menuFilePlaylist_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(222, 6);
            // 
            // menuFileExit
            // 
            this.menuFileExit.Image = global::CAINClient.Properties.Resources.door_in;
            this.menuFileExit.Name = "menuFileExit";
            this.menuFileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.menuFileExit.Size = new System.Drawing.Size(225, 22);
            this.menuFileExit.Text = "Salir";
            this.menuFileExit.Click += new System.EventHandler(this.menuFileExit_Click);
            // 
            // servicioToolStripMenuItem
            // 
            this.servicioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuServiceInstall,
            this.menuServiceUninstall,
            this.toolStripMenuItem4,
            this.menuServiceStart,
            this.menuServiceStop,
            this.toolStripMenuItem5,
            this.menuServiceSettings});
            this.servicioToolStripMenuItem.Name = "servicioToolStripMenuItem";
            this.servicioToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.servicioToolStripMenuItem.Text = "Servicio";
            // 
            // menuServiceInstall
            // 
            this.menuServiceInstall.Image = global::CAINClient.Properties.Resources.cog_add;
            this.menuServiceInstall.Name = "menuServiceInstall";
            this.menuServiceInstall.Size = new System.Drawing.Size(150, 22);
            this.menuServiceInstall.Text = "Instalar";
            this.menuServiceInstall.Click += new System.EventHandler(this.menuServiceInstall_Click);
            // 
            // menuServiceUninstall
            // 
            this.menuServiceUninstall.Image = global::CAINClient.Properties.Resources.cog_delete;
            this.menuServiceUninstall.Name = "menuServiceUninstall";
            this.menuServiceUninstall.Size = new System.Drawing.Size(150, 22);
            this.menuServiceUninstall.Text = "Desinstalar";
            this.menuServiceUninstall.Click += new System.EventHandler(this.menuServiceUninstall_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(147, 6);
            // 
            // menuServiceStart
            // 
            this.menuServiceStart.Image = global::CAINClient.Properties.Resources.control_play_blue;
            this.menuServiceStart.Name = "menuServiceStart";
            this.menuServiceStart.Size = new System.Drawing.Size(150, 22);
            this.menuServiceStart.Text = "Iniciar";
            this.menuServiceStart.Click += new System.EventHandler(this.menuServiceStart_Click);
            // 
            // menuServiceStop
            // 
            this.menuServiceStop.Image = global::CAINClient.Properties.Resources.control_stop_blue;
            this.menuServiceStop.Name = "menuServiceStop";
            this.menuServiceStop.Size = new System.Drawing.Size(150, 22);
            this.menuServiceStop.Text = "Parar";
            this.menuServiceStop.Click += new System.EventHandler(this.menuServiceStop_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(147, 6);
            // 
            // menuServiceSettings
            // 
            this.menuServiceSettings.Image = global::CAINClient.Properties.Resources.control_equalizer_blue;
            this.menuServiceSettings.Name = "menuServiceSettings";
            this.menuServiceSettings.Size = new System.Drawing.Size(150, 22);
            this.menuServiceSettings.Text = "Configuración";
            this.menuServiceSettings.Click += new System.EventHandler(this.menuServiceSettings_Click);
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuHelpAbout});
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // menuHelpAbout
            // 
            this.menuHelpAbout.Image = global::CAINClient.Properties.Resources.help;
            this.menuHelpAbout.Name = "menuHelpAbout";
            this.menuHelpAbout.Size = new System.Drawing.Size(135, 22);
            this.menuHelpAbout.Text = "Acerca de...";
            this.menuHelpAbout.Click += new System.EventHandler(this.menuHelpAbout_Click);
            // 
            // toolBar
            // 
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolFileScan,
            this.toolFileUpdate,
            this.toolStripSeparator1,
            this.toolFileColumns,
            this.toolStripSeparator2,
            this.toolFileSave,
            this.toolFileFind,
            this.toolStripSeparator3,
            this.toolFilePending,
            this.toolFileConfirm,
            this.toolStripSeparator4,
            this.toolFilePlaylist,
            this.toolPattern,
            this.toolOperations,
            this.toolColumns});
            this.toolBar.Location = new System.Drawing.Point(0, 24);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(1284, 25);
            this.toolBar.TabIndex = 1;
            this.toolBar.Text = "toolStrip1";
            // 
            // toolFileScan
            // 
            this.toolFileScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolFileScan.Image = global::CAINClient.Properties.Resources.folder_magnify;
            this.toolFileScan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolFileScan.Name = "toolFileScan";
            this.toolFileScan.Size = new System.Drawing.Size(23, 22);
            this.toolFileScan.Text = "toolStripButton1";
            this.toolFileScan.ToolTipText = "Escanear carpetas...";
            this.toolFileScan.Click += new System.EventHandler(this.menuFileScan_Click);
            // 
            // toolFileUpdate
            // 
            this.toolFileUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolFileUpdate.Image = global::CAINClient.Properties.Resources.arrow_refresh;
            this.toolFileUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolFileUpdate.Name = "toolFileUpdate";
            this.toolFileUpdate.Size = new System.Drawing.Size(23, 22);
            this.toolFileUpdate.Text = "toolStripButton2";
            this.toolFileUpdate.ToolTipText = "Actualizar información";
            this.toolFileUpdate.Click += new System.EventHandler(this.menuFileUpdate_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolFileColumns
            // 
            this.toolFileColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolFileColumns.Image = global::CAINClient.Properties.Resources.eye;
            this.toolFileColumns.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolFileColumns.Name = "toolFileColumns";
            this.toolFileColumns.Size = new System.Drawing.Size(23, 22);
            this.toolFileColumns.Text = "toolStripButton3";
            this.toolFileColumns.ToolTipText = "Mostrar columnas...";
            this.toolFileColumns.Click += new System.EventHandler(this.menuFileColumns_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolFileSave
            // 
            this.toolFileSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolFileSave.Image = global::CAINClient.Properties.Resources.disk;
            this.toolFileSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolFileSave.Name = "toolFileSave";
            this.toolFileSave.Size = new System.Drawing.Size(23, 22);
            this.toolFileSave.Text = "toolStripButton5";
            this.toolFileSave.ToolTipText = "Guardar cambios";
            this.toolFileSave.Click += new System.EventHandler(this.menuFileSave_Click);
            // 
            // toolFileFind
            // 
            this.toolFileFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolFileFind.Image = global::CAINClient.Properties.Resources.wand;
            this.toolFileFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolFileFind.Name = "toolFileFind";
            this.toolFileFind.Size = new System.Drawing.Size(23, 22);
            this.toolFileFind.Text = "toolStripButton4";
            this.toolFileFind.ToolTipText = "Asistente de catalogación...";
            this.toolFileFind.Click += new System.EventHandler(this.menuFileFind_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolFilePending
            // 
            this.toolFilePending.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolFilePending.Image = global::CAINClient.Properties.Resources.error;
            this.toolFilePending.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolFilePending.Name = "toolFilePending";
            this.toolFilePending.Size = new System.Drawing.Size(23, 22);
            this.toolFilePending.Text = "toolStripButton6";
            this.toolFilePending.ToolTipText = "Ver sólo pendientes";
            this.toolFilePending.Click += new System.EventHandler(this.menuFilePending_Click);
            // 
            // toolFileConfirm
            // 
            this.toolFileConfirm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolFileConfirm.Image = global::CAINClient.Properties.Resources.tag_green;
            this.toolFileConfirm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolFileConfirm.Name = "toolFileConfirm";
            this.toolFileConfirm.Size = new System.Drawing.Size(23, 22);
            this.toolFileConfirm.Text = "toolStripButton7";
            this.toolFileConfirm.ToolTipText = "Cambiar estado a \'Catalogada\'";
            this.toolFileConfirm.Click += new System.EventHandler(this.menuFileConfirm_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolFilePlaylist
            // 
            this.toolFilePlaylist.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolFilePlaylist.Image = global::CAINClient.Properties.Resources.music;
            this.toolFilePlaylist.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolFilePlaylist.Name = "toolFilePlaylist";
            this.toolFilePlaylist.Size = new System.Drawing.Size(23, 22);
            this.toolFilePlaylist.Text = "toolStripButton8";
            this.toolFilePlaylist.ToolTipText = "Crear lista de reproducción...";
            this.toolFilePlaylist.Click += new System.EventHandler(this.menuFilePlaylist_Click);
            // 
            // toolPattern
            // 
            this.toolPattern.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolPattern.Margin = new System.Windows.Forms.Padding(1, 0, 5, 0);
            this.toolPattern.Name = "toolPattern";
            this.toolPattern.Size = new System.Drawing.Size(150, 25);
            this.toolPattern.TextChanged += new System.EventHandler(this.toolPattern_TextChanged);
            // 
            // toolOperations
            // 
            this.toolOperations.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolOperations.Margin = new System.Windows.Forms.Padding(1, 0, 5, 0);
            this.toolOperations.Name = "toolOperations";
            this.toolOperations.Size = new System.Drawing.Size(150, 25);
            this.toolOperations.SelectedIndexChanged += new System.EventHandler(this.toolOperations_SelectedIndexChanged);
            // 
            // toolColumns
            // 
            this.toolColumns.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolColumns.Margin = new System.Windows.Forms.Padding(1, 0, 5, 0);
            this.toolColumns.Name = "toolColumns";
            this.toolColumns.Size = new System.Drawing.Size(150, 25);
            this.toolColumns.SelectedIndexChanged += new System.EventHandler(this.toolColumns_SelectedIndexChanged);
            // 
            // statusBar
            // 
            this.statusBar.GripMargin = new System.Windows.Forms.Padding(0);
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel6,
            this.statusCataloged,
            this.toolStripStatusLabel8,
            this.statusNoCataloged,
            this.toolStripStatusLabel2,
            this.statusSelected,
            this.toolStripStatusLabel4,
            this.statusDisplayed,
            this.toolStripStatusLabel3,
            this.statusTotal});
            this.statusBar.Location = new System.Drawing.Point(0, 638);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(1284, 24);
            this.statusBar.TabIndex = 2;
            this.statusBar.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(364, 20);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.AutoSize = false;
            this.toolStripStatusLabel6.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(110, 20);
            this.toolStripStatusLabel6.Text = "Catalogadas: ";
            this.toolStripStatusLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statusCataloged
            // 
            this.statusCataloged.AutoSize = false;
            this.statusCataloged.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.statusCataloged.Margin = new System.Windows.Forms.Padding(0, 2, 5, 2);
            this.statusCataloged.Name = "statusCataloged";
            this.statusCataloged.Size = new System.Drawing.Size(70, 20);
            this.statusCataloged.Text = "0";
            this.statusCataloged.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripStatusLabel8
            // 
            this.toolStripStatusLabel8.AutoSize = false;
            this.toolStripStatusLabel8.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripStatusLabel8.Name = "toolStripStatusLabel8";
            this.toolStripStatusLabel8.Size = new System.Drawing.Size(110, 20);
            this.toolStripStatusLabel8.Text = "No Catalogadas: ";
            this.toolStripStatusLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statusNoCataloged
            // 
            this.statusNoCataloged.AutoSize = false;
            this.statusNoCataloged.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.statusNoCataloged.Margin = new System.Windows.Forms.Padding(0, 2, 5, 2);
            this.statusNoCataloged.Name = "statusNoCataloged";
            this.statusNoCataloged.Size = new System.Drawing.Size(70, 20);
            this.statusNoCataloged.Text = "0";
            this.statusNoCataloged.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.AutoSize = false;
            this.toolStripStatusLabel2.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(110, 20);
            this.toolStripStatusLabel2.Text = "Seleccionadas: ";
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statusSelected
            // 
            this.statusSelected.AutoSize = false;
            this.statusSelected.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.statusSelected.Margin = new System.Windows.Forms.Padding(0, 2, 5, 2);
            this.statusSelected.Name = "statusSelected";
            this.statusSelected.Size = new System.Drawing.Size(70, 20);
            this.statusSelected.Text = "0";
            this.statusSelected.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.AutoSize = false;
            this.toolStripStatusLabel4.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(100, 20);
            this.toolStripStatusLabel4.Text = "Mostradas: ";
            this.toolStripStatusLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statusDisplayed
            // 
            this.statusDisplayed.AutoSize = false;
            this.statusDisplayed.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.statusDisplayed.Margin = new System.Windows.Forms.Padding(0, 2, 5, 2);
            this.statusDisplayed.Name = "statusDisplayed";
            this.statusDisplayed.Size = new System.Drawing.Size(70, 20);
            this.statusDisplayed.Text = "0";
            this.statusDisplayed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.AutoSize = false;
            this.toolStripStatusLabel3.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(100, 20);
            this.toolStripStatusLabel3.Text = "Total: ";
            this.toolStripStatusLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statusTotal
            // 
            this.statusTotal.AutoSize = false;
            this.statusTotal.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.statusTotal.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusTotal.Margin = new System.Windows.Forms.Padding(0, 2, 5, 2);
            this.statusTotal.Name = "statusTotal";
            this.statusTotal.Size = new System.Drawing.Size(70, 20);
            this.statusTotal.Text = "0";
            this.statusTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Panel1MinSize = 320;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvView);
            this.splitContainer1.Panel2MinSize = 320;
            this.splitContainer1.Size = new System.Drawing.Size(1284, 589);
            this.splitContainer1.SplitterDistance = 320;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.Controls.Add(this.lblTitle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtTitle, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblAlbum, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtAlbum, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblDuration, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblYear, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.lblArtists, 0, 10);
            this.tableLayoutPanel1.Controls.Add(this.txtArtists, 0, 11);
            this.tableLayoutPanel1.Controls.Add(this.pbxCover, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblCover, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtDuration, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtYear, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.lblTags, 0, 12);
            this.tableLayoutPanel1.Controls.Add(this.ltvTags, 0, 13);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 14);
            this.tableLayoutPanel1.Controls.Add(this.btnAlbum, 1, 8);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(20);
            this.tableLayoutPanel1.RowCount = 15;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(316, 585);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(35, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Título";
            // 
            // txtTitle
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtTitle, 2);
            this.txtTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTitle.Location = new System.Drawing.Point(20, 36);
            this.txtTitle.Margin = new System.Windows.Forms.Padding(0);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(276, 20);
            this.txtTitle.TabIndex = 1;
            this.txtTitle.TextChanged += new System.EventHandler(this.txtTitle_TextChanged);
            // 
            // lblAlbum
            // 
            this.lblAlbum.AutoSize = true;
            this.lblAlbum.Location = new System.Drawing.Point(20, 66);
            this.lblAlbum.Margin = new System.Windows.Forms.Padding(0, 10, 0, 3);
            this.lblAlbum.Name = "lblAlbum";
            this.lblAlbum.Size = new System.Drawing.Size(36, 13);
            this.lblAlbum.TabIndex = 2;
            this.lblAlbum.Text = "Álbum";
            // 
            // txtAlbum
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtAlbum, 2);
            this.txtAlbum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAlbum.Location = new System.Drawing.Point(20, 82);
            this.txtAlbum.Margin = new System.Windows.Forms.Padding(0);
            this.txtAlbum.Name = "txtAlbum";
            this.txtAlbum.Size = new System.Drawing.Size(276, 20);
            this.txtAlbum.TabIndex = 6;
            this.txtAlbum.TextChanged += new System.EventHandler(this.txtAlbum_TextChanged);
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(216, 112);
            this.lblDuration.Margin = new System.Windows.Forms.Padding(40, 10, 0, 3);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(50, 13);
            this.lblDuration.TabIndex = 3;
            this.lblDuration.Text = "Duración";
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(216, 158);
            this.lblYear.Margin = new System.Windows.Forms.Padding(40, 10, 0, 3);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(26, 13);
            this.lblYear.TabIndex = 4;
            this.lblYear.Text = "Año";
            // 
            // lblArtists
            // 
            this.lblArtists.AutoSize = true;
            this.lblArtists.Location = new System.Drawing.Point(20, 278);
            this.lblArtists.Margin = new System.Windows.Forms.Padding(0, 10, 0, 3);
            this.lblArtists.Name = "lblArtists";
            this.lblArtists.Size = new System.Drawing.Size(47, 13);
            this.lblArtists.TabIndex = 5;
            this.lblArtists.Text = "Artista(s)";
            // 
            // txtArtists
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.txtArtists, 2);
            this.txtArtists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtArtists.Location = new System.Drawing.Point(20, 294);
            this.txtArtists.Margin = new System.Windows.Forms.Padding(0);
            this.txtArtists.Name = "txtArtists";
            this.txtArtists.Size = new System.Drawing.Size(276, 20);
            this.txtArtists.TabIndex = 7;
            this.txtArtists.TextChanged += new System.EventHandler(this.txtArtists_TextChanged);
            // 
            // pbxCover
            // 
            this.pbxCover.BackColor = System.Drawing.SystemColors.Window;
            this.pbxCover.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxCover.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxCover.Location = new System.Drawing.Point(20, 128);
            this.pbxCover.Margin = new System.Windows.Forms.Padding(0);
            this.pbxCover.Name = "pbxCover";
            this.tableLayoutPanel1.SetRowSpan(this.pbxCover, 4);
            this.pbxCover.Size = new System.Drawing.Size(140, 140);
            this.pbxCover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxCover.TabIndex = 8;
            this.pbxCover.TabStop = false;
            this.pbxCover.Click += new System.EventHandler(this.pbxCover_Click);
            // 
            // lblCover
            // 
            this.lblCover.AutoSize = true;
            this.lblCover.Location = new System.Drawing.Point(20, 112);
            this.lblCover.Margin = new System.Windows.Forms.Padding(0, 10, 0, 3);
            this.lblCover.Name = "lblCover";
            this.lblCover.Size = new System.Drawing.Size(46, 13);
            this.lblCover.TabIndex = 9;
            this.lblCover.Text = "Carátula";
            // 
            // txtDuration
            // 
            this.txtDuration.Location = new System.Drawing.Point(216, 128);
            this.txtDuration.Margin = new System.Windows.Forms.Padding(40, 0, 0, 0);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(80, 20);
            this.txtDuration.TabIndex = 10;
            this.txtDuration.TextChanged += new System.EventHandler(this.txtDuration_TextChanged);
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(216, 174);
            this.txtYear.Margin = new System.Windows.Forms.Padding(40, 0, 0, 0);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(80, 20);
            this.txtYear.TabIndex = 11;
            this.txtYear.TextChanged += new System.EventHandler(this.txtYear_TextChanged);
            // 
            // lblTags
            // 
            this.lblTags.AutoSize = true;
            this.lblTags.Location = new System.Drawing.Point(20, 324);
            this.lblTags.Margin = new System.Windows.Forms.Padding(0, 10, 0, 3);
            this.lblTags.Name = "lblTags";
            this.lblTags.Size = new System.Drawing.Size(57, 13);
            this.lblTags.TabIndex = 12;
            this.lblTags.Text = "Etiqueta(s)";
            // 
            // ltvTags
            // 
            this.ltvTags.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.ltvTags, 2);
            this.ltvTags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ltvTags.Location = new System.Drawing.Point(20, 340);
            this.ltvTags.Margin = new System.Windows.Forms.Padding(0);
            this.ltvTags.Name = "ltvTags";
            this.ltvTags.Size = new System.Drawing.Size(276, 195);
            this.ltvTags.TabIndex = 13;
            this.ltvTags.UseCompatibleStateImageBehavior = false;
            this.ltvTags.View = System.Windows.Forms.View.Details;
            this.ltvTags.DoubleClick += new System.EventHandler(this.ltvTags_DoubleClick);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.Controls.Add(this.btnNewTag, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnEditTag, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnDeleteTag, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(20, 535);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(276, 30);
            this.tableLayoutPanel2.TabIndex = 14;
            // 
            // btnNewTag
            // 
            this.btnNewTag.Image = global::CAINClient.Properties.Resources.tag_blue_add1;
            this.btnNewTag.Location = new System.Drawing.Point(0, 5);
            this.btnNewTag.Margin = new System.Windows.Forms.Padding(0);
            this.btnNewTag.Name = "btnNewTag";
            this.btnNewTag.Size = new System.Drawing.Size(75, 25);
            this.btnNewTag.TabIndex = 0;
            this.btnNewTag.Text = "Nueva...";
            this.btnNewTag.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNewTag.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNewTag.UseVisualStyleBackColor = true;
            this.btnNewTag.Click += new System.EventHandler(this.btnNewTag_Click);
            // 
            // btnEditTag
            // 
            this.btnEditTag.Image = global::CAINClient.Properties.Resources.tag_blue_edit1;
            this.btnEditTag.Location = new System.Drawing.Point(100, 5);
            this.btnEditTag.Margin = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.btnEditTag.Name = "btnEditTag";
            this.btnEditTag.Size = new System.Drawing.Size(75, 25);
            this.btnEditTag.TabIndex = 1;
            this.btnEditTag.Text = "Editar...";
            this.btnEditTag.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEditTag.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEditTag.UseVisualStyleBackColor = true;
            this.btnEditTag.Click += new System.EventHandler(this.btnEditTag_Click);
            // 
            // btnDeleteTag
            // 
            this.btnDeleteTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteTag.Image = global::CAINClient.Properties.Resources.tag_blue_delete;
            this.btnDeleteTag.Location = new System.Drawing.Point(201, 5);
            this.btnDeleteTag.Margin = new System.Windows.Forms.Padding(0);
            this.btnDeleteTag.Name = "btnDeleteTag";
            this.btnDeleteTag.Size = new System.Drawing.Size(75, 25);
            this.btnDeleteTag.TabIndex = 2;
            this.btnDeleteTag.Text = "Borrar";
            this.btnDeleteTag.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDeleteTag.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeleteTag.UseVisualStyleBackColor = true;
            this.btnDeleteTag.Click += new System.EventHandler(this.btnDeleteTag_Click);
            // 
            // btnAlbum
            // 
            this.btnAlbum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAlbum.Image = global::CAINClient.Properties.Resources.cd;
            this.btnAlbum.Location = new System.Drawing.Point(216, 243);
            this.btnAlbum.Margin = new System.Windows.Forms.Padding(40, 0, 0, 0);
            this.btnAlbum.Name = "btnAlbum";
            this.btnAlbum.Size = new System.Drawing.Size(80, 25);
            this.btnAlbum.TabIndex = 15;
            this.btnAlbum.Text = "Asignar...";
            this.btnAlbum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAlbum.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAlbum.UseVisualStyleBackColor = true;
            this.btnAlbum.Click += new System.EventHandler(this.btnAlbum_Click);
            // 
            // dgvView
            // 
            this.dgvView.AllowUserToAddRows = false;
            this.dgvView.AllowUserToDeleteRows = false;
            this.dgvView.AllowUserToResizeRows = false;
            this.dgvView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvView.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.dgvView.Location = new System.Drawing.Point(0, 0);
            this.dgvView.Name = "dgvView";
            this.dgvView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvView.Size = new System.Drawing.Size(955, 585);
            this.dgvView.TabIndex = 0;
            this.dgvView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvView_CellMouseClick);
            this.dgvView.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvView_CellMouseMove);
            this.dgvView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvView_ColumnHeaderMouseClick);
            this.dgvView.SelectionChanged += new System.EventHandler(this.dgvView_SelectionChanged);
            this.dgvView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgvView_MouseMove);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFind,
            this.toolStripMenuItem8,
            this.menuAsign,
            this.toolStripMenuItem9,
            this.menuStatus,
            this.toolStripMenuItem10,
            this.menuPlaylist});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(226, 110);
            // 
            // menuFind
            // 
            this.menuFind.Image = global::CAINClient.Properties.Resources.wand;
            this.menuFind.Name = "menuFind";
            this.menuFind.Size = new System.Drawing.Size(225, 22);
            this.menuFind.Text = "Asistente de catalogación...";
            this.menuFind.Click += new System.EventHandler(this.menuFileFind_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(222, 6);
            // 
            // menuAsign
            // 
            this.menuAsign.Image = global::CAINClient.Properties.Resources.cd;
            this.menuAsign.Name = "menuAsign";
            this.menuAsign.Size = new System.Drawing.Size(225, 22);
            this.menuAsign.Text = "Asignar al álbum...";
            this.menuAsign.Click += new System.EventHandler(this.btnAlbum_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(222, 6);
            // 
            // menuStatus
            // 
            this.menuStatus.Image = global::CAINClient.Properties.Resources.tag_green;
            this.menuStatus.Name = "menuStatus";
            this.menuStatus.Size = new System.Drawing.Size(225, 22);
            this.menuStatus.Text = "Confirmar catalogación";
            this.menuStatus.Click += new System.EventHandler(this.menuFileConfirm_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(222, 6);
            // 
            // menuPlaylist
            // 
            this.menuPlaylist.Image = global::CAINClient.Properties.Resources.music;
            this.menuPlaylist.Name = "menuPlaylist";
            this.menuPlaylist.Size = new System.Drawing.Size(225, 22);
            this.menuPlaylist.Text = "Crear lista de reproducción...";
            this.menuPlaylist.Click += new System.EventHandler(this.menuFilePlaylist_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 662);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.toolBar);
            this.Controls.Add(this.menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider5)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCover)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvView)).EndInit();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuFileScan;
        private System.Windows.Forms.ToolStripMenuItem menuFileUpdate;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menuFileColumns;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem menuFileFind;
        private System.Windows.Forms.ToolStripMenuItem menuFileSave;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem menuFilePending;
        private System.Windows.Forms.ToolStripMenuItem menuFileConfirm;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem menuFilePlaylist;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem menuFileExit;
        private System.Windows.Forms.ToolStripMenuItem servicioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuServiceInstall;
        private System.Windows.Forms.ToolStripMenuItem menuServiceUninstall;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem menuServiceStart;
        private System.Windows.Forms.ToolStripMenuItem menuServiceStop;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem menuServiceSettings;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuHelpAbout;
        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripButton toolFileScan;
        private System.Windows.Forms.ToolStripButton toolFileUpdate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolFileColumns;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolFileFind;
        private System.Windows.Forms.ToolStripButton toolFileSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolFilePending;
        private System.Windows.Forms.ToolStripButton toolFileConfirm;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolFilePlaylist;
        private System.Windows.Forms.ToolStripTextBox toolPattern;
        private System.Windows.Forms.ToolStripComboBox toolOperations;
        private System.Windows.Forms.ToolStripComboBox toolColumns;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel statusSelected;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel statusTotal;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel statusCataloged;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel8;
        private System.Windows.Forms.ToolStripStatusLabel statusNoCataloged;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private System.Windows.Forms.ErrorProvider errorProvider4;
        private System.Windows.Forms.ErrorProvider errorProvider5;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgvView;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblAlbum;
        private System.Windows.Forms.TextBox txtAlbum;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Label lblArtists;
        private System.Windows.Forms.TextBox txtArtists;
        private System.Windows.Forms.PictureBox pbxCover;
        private System.Windows.Forms.Label lblCover;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.Label lblTags;
        private System.Windows.Forms.ListView ltvTags;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnNewTag;
        private System.Windows.Forms.Button btnEditTag;
        private System.Windows.Forms.Button btnDeleteTag;
        private System.Windows.Forms.Button btnAlbum;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem menuFind;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem menuAsign;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem menuStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem menuPlaylist;
        private System.Windows.Forms.ToolStripStatusLabel statusDisplayed;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
    }
}

