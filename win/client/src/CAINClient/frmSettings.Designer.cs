namespace CAINClient
{
    partial class frmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.lbxFolderPaths = new System.Windows.Forms.ListBox();
            this.btnAddFolderPath = new System.Windows.Forms.Button();
            this.btnDeleteFolderPath = new System.Windows.Forms.Button();
            this.btnGoUpFolderPath = new System.Windows.Forms.Button();
            this.btnGoDownFolderPath = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.optSearchOriginal = new System.Windows.Forms.RadioButton();
            this.optSearchByTag = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnPathDst = new System.Windows.Forms.Button();
            this.lblPathDst = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.sbxElapsedTime = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sbxElapsedTime)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // Timer
            // 
            this.Timer.Enabled = true;
            this.Timer.Interval = 500;
            this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // lbxFolderPaths
            // 
            this.lbxFolderPaths.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxFolderPaths.FormattingEnabled = true;
            this.lbxFolderPaths.Location = new System.Drawing.Point(10, 10);
            this.lbxFolderPaths.Margin = new System.Windows.Forms.Padding(10);
            this.lbxFolderPaths.Name = "lbxFolderPaths";
            this.tableLayoutPanel2.SetRowSpan(this.lbxFolderPaths, 5);
            this.lbxFolderPaths.Size = new System.Drawing.Size(367, 163);
            this.lbxFolderPaths.TabIndex = 1;
            // 
            // btnAddFolderPath
            // 
            this.btnAddFolderPath.Image = global::CAINClient.Properties.Resources.add;
            this.btnAddFolderPath.Location = new System.Drawing.Point(387, 10);
            this.btnAddFolderPath.Margin = new System.Windows.Forms.Padding(0, 10, 10, 3);
            this.btnAddFolderPath.Name = "btnAddFolderPath";
            this.btnAddFolderPath.Size = new System.Drawing.Size(75, 23);
            this.btnAddFolderPath.TabIndex = 2;
            this.btnAddFolderPath.Text = "Añadir";
            this.btnAddFolderPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddFolderPath.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddFolderPath.UseVisualStyleBackColor = true;
            this.btnAddFolderPath.Click += new System.EventHandler(this.btnAddFolderPath_Click);
            // 
            // btnDeleteFolderPath
            // 
            this.btnDeleteFolderPath.Image = global::CAINClient.Properties.Resources.delete;
            this.btnDeleteFolderPath.Location = new System.Drawing.Point(387, 39);
            this.btnDeleteFolderPath.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.btnDeleteFolderPath.Name = "btnDeleteFolderPath";
            this.btnDeleteFolderPath.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteFolderPath.TabIndex = 3;
            this.btnDeleteFolderPath.Text = "Borrar";
            this.btnDeleteFolderPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDeleteFolderPath.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDeleteFolderPath.UseVisualStyleBackColor = true;
            this.btnDeleteFolderPath.Click += new System.EventHandler(this.btnDeleteFolderPath_Click);
            // 
            // btnGoUpFolderPath
            // 
            this.btnGoUpFolderPath.Image = global::CAINClient.Properties.Resources.bullet_arrow_up;
            this.btnGoUpFolderPath.Location = new System.Drawing.Point(387, 121);
            this.btnGoUpFolderPath.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.btnGoUpFolderPath.Name = "btnGoUpFolderPath";
            this.btnGoUpFolderPath.Size = new System.Drawing.Size(75, 23);
            this.btnGoUpFolderPath.TabIndex = 4;
            this.btnGoUpFolderPath.Text = "Subir";
            this.btnGoUpFolderPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGoUpFolderPath.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGoUpFolderPath.UseVisualStyleBackColor = true;
            this.btnGoUpFolderPath.Click += new System.EventHandler(this.btnGoUpFolderPath_Click);
            // 
            // btnGoDownFolderPath
            // 
            this.btnGoDownFolderPath.Image = global::CAINClient.Properties.Resources.bullet_arrow_down;
            this.btnGoDownFolderPath.Location = new System.Drawing.Point(387, 150);
            this.btnGoDownFolderPath.Margin = new System.Windows.Forms.Padding(0, 3, 3, 10);
            this.btnGoDownFolderPath.Name = "btnGoDownFolderPath";
            this.btnGoDownFolderPath.Size = new System.Drawing.Size(75, 23);
            this.btnGoDownFolderPath.TabIndex = 5;
            this.btnGoDownFolderPath.Text = "Bajar";
            this.btnGoDownFolderPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGoDownFolderPath.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGoDownFolderPath.UseVisualStyleBackColor = true;
            this.btnGoDownFolderPath.Click += new System.EventHandler(this.btnGoDownFolderPath_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.optSearchOriginal);
            this.groupBox3.Controls.Add(this.optSearchByTag);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(15, 232);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(15, 0, 15, 15);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(249, 83);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Criterio para buscar álbumes";
            // 
            // optSearchOriginal
            // 
            this.optSearchOriginal.AutoSize = true;
            this.optSearchOriginal.Location = new System.Drawing.Point(21, 51);
            this.optSearchOriginal.Name = "optSearchOriginal";
            this.optSearchOriginal.Size = new System.Drawing.Size(139, 17);
            this.optSearchOriginal.TabIndex = 7;
            this.optSearchOriginal.Text = "El original (más antiguo).";
            this.optSearchOriginal.UseVisualStyleBackColor = true;
            // 
            // optSearchByTag
            // 
            this.optSearchByTag.AutoSize = true;
            this.optSearchByTag.Checked = true;
            this.optSearchByTag.Location = new System.Drawing.Point(21, 26);
            this.optSearchByTag.Name = "optSearchByTag";
            this.optSearchByTag.Size = new System.Drawing.Size(199, 17);
            this.optSearchByTag.TabIndex = 6;
            this.optSearchByTag.TabStop = true;
            this.optSearchByTag.Text = "En base a los metadatos del archivo.";
            this.optSearchByTag.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox4, 2);
            this.groupBox4.Controls.Add(this.tableLayoutPanel2);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(15, 15);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(15);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(478, 202);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Carpetas a escanear (max. 50 carpetas)";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.lbxFolderPaths, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnGoDownFolderPath, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.btnGoUpFolderPath, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.btnDeleteFolderPath, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnAddFolderPath, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(472, 183);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Image = global::CAINClient.Properties.Resources.accept;
            this.btnOK.Location = new System.Drawing.Point(333, 12);
            this.btnOK.Margin = new System.Windows.Forms.Padding(15, 10, 0, 15);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 25);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "Aceptar";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::CAINClient.Properties.Resources.cancel;
            this.btnCancel.Location = new System.Drawing.Point(418, 12);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(10, 10, 15, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox5
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox5, 2);
            this.groupBox5.Controls.Add(this.tableLayoutPanel3);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(15, 330);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(15, 0, 15, 15);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(478, 59);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Carpeta donde se guardarán los archivos escaneados";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.btnPathDst, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblPathDst, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(472, 40);
            this.tableLayoutPanel3.TabIndex = 14;
            // 
            // btnPathDst
            // 
            this.btnPathDst.Location = new System.Drawing.Point(437, 10);
            this.btnPathDst.Margin = new System.Windows.Forms.Padding(0, 10, 10, 0);
            this.btnPathDst.Name = "btnPathDst";
            this.btnPathDst.Size = new System.Drawing.Size(25, 20);
            this.btnPathDst.TabIndex = 9;
            this.btnPathDst.Text = "...";
            this.btnPathDst.UseVisualStyleBackColor = true;
            this.btnPathDst.Click += new System.EventHandler(this.btnPathDst_Click);
            // 
            // lblPathDst
            // 
            this.lblPathDst.AutoEllipsis = true;
            this.lblPathDst.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPathDst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPathDst.Location = new System.Drawing.Point(10, 10);
            this.lblPathDst.Margin = new System.Windows.Forms.Padding(10, 10, 0, 10);
            this.lblPathDst.Name = "lblPathDst";
            this.lblPathDst.Padding = new System.Windows.Forms.Padding(2);
            this.lblPathDst.Size = new System.Drawing.Size(427, 20);
            this.lblPathDst.TabIndex = 14;
            this.lblPathDst.Text = "label2";
            this.lblPathDst.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(508, 456);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel5);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(279, 232);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(0, 0, 15, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(214, 83);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lapso de tiempo entre escaneos";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.sbxElapsedTime, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.Padding = new System.Windows.Forms.Padding(20);
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(208, 64);
            this.tableLayoutPanel5.TabIndex = 9;
            // 
            // sbxElapsedTime
            // 
            this.sbxElapsedTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sbxElapsedTime.Location = new System.Drawing.Point(40, 20);
            this.sbxElapsedTime.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.sbxElapsedTime.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.sbxElapsedTime.Name = "sbxElapsedTime";
            this.sbxElapsedTime.Size = new System.Drawing.Size(79, 20);
            this.sbxElapsedTime.TabIndex = 8;
            this.sbxElapsedTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(129, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(10, 3, 10, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "minuto(s)";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel4, 2);
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.btnOK, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.btnCancel, 1, 1);
            this.tableLayoutPanel4.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 404);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(508, 52);
            this.tableLayoutPanel4.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tableLayoutPanel4.SetColumnSpan(this.label2, 2);
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(15, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(478, 2);
            this.label2.TabIndex = 2;
            this.label2.Text = "label2";
            // 
            // frmSettings
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(508, 456);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configurar servicio";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sbxElapsedTime)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer Timer;
        private System.Windows.Forms.ListBox lbxFolderPaths;
        private System.Windows.Forms.Button btnAddFolderPath;
        private System.Windows.Forms.Button btnDeleteFolderPath;
        private System.Windows.Forms.Button btnGoUpFolderPath;
        private System.Windows.Forms.Button btnGoDownFolderPath;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton optSearchOriginal;
        private System.Windows.Forms.RadioButton optSearchByTag;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnPathDst;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.NumericUpDown sbxElapsedTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lblPathDst;
        private System.Windows.Forms.Label label2;
    }
}

