namespace ConnectSQLite_KodanevAndrey
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnReadDB = new System.Windows.Forms.Button();
            this.btnAddDB = new System.Windows.Forms.Button();
            this.btnDeleteDB = new System.Windows.Forms.Button();
            this.btnResetDB = new System.Windows.Forms.Button();
            this.dgvViewer = new System.Windows.Forms.DataGridView();
            this.lbStatusText = new System.Windows.Forms.Label();
            this.btnDeleteAllDB = new System.Windows.Forms.Button();
            this.btnAddImageDB = new System.Windows.Forms.Button();
            this.btnConnectDB = new System.Windows.Forms.Button();
            this.btnCreateNewDB = new System.Windows.Forms.Button();
            this.lbCommand = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReadDB
            // 
            this.btnReadDB.Location = new System.Drawing.Point(318, 12);
            this.btnReadDB.Name = "btnReadDB";
            this.btnReadDB.Size = new System.Drawing.Size(75, 23);
            this.btnReadDB.TabIndex = 0;
            this.btnReadDB.Text = "Read";
            this.btnReadDB.UseVisualStyleBackColor = true;
            this.btnReadDB.Click += new System.EventHandler(this.btnReadDB_Click);
            // 
            // btnAddDB
            // 
            this.btnAddDB.Location = new System.Drawing.Point(399, 12);
            this.btnAddDB.Name = "btnAddDB";
            this.btnAddDB.Size = new System.Drawing.Size(75, 23);
            this.btnAddDB.TabIndex = 1;
            this.btnAddDB.Text = "Add";
            this.btnAddDB.UseVisualStyleBackColor = true;
            this.btnAddDB.Click += new System.EventHandler(this.btnAddDB_Click);
            // 
            // btnDeleteDB
            // 
            this.btnDeleteDB.Location = new System.Drawing.Point(642, 12);
            this.btnDeleteDB.Name = "btnDeleteDB";
            this.btnDeleteDB.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteDB.TabIndex = 2;
            this.btnDeleteDB.Text = "Delete";
            this.btnDeleteDB.UseVisualStyleBackColor = true;
            this.btnDeleteDB.Click += new System.EventHandler(this.btnDeleteDB_Click);
            // 
            // btnResetDB
            // 
            this.btnResetDB.Location = new System.Drawing.Point(561, 12);
            this.btnResetDB.Name = "btnResetDB";
            this.btnResetDB.Size = new System.Drawing.Size(75, 23);
            this.btnResetDB.TabIndex = 3;
            this.btnResetDB.Text = "Reset";
            this.btnResetDB.UseVisualStyleBackColor = true;
            this.btnResetDB.Click += new System.EventHandler(this.btnResetDB_Click);
            // 
            // dgvViewer
            // 
            this.dgvViewer.BackgroundColor = System.Drawing.SystemColors.ButtonShadow;
            this.dgvViewer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvViewer.Location = new System.Drawing.Point(12, 41);
            this.dgvViewer.Name = "dgvViewer";
            this.dgvViewer.Size = new System.Drawing.Size(1050, 542);
            this.dgvViewer.TabIndex = 4;
            this.dgvViewer.Click += new System.EventHandler(this.dgvViewer_Click);
            // 
            // lbStatusText
            // 
            this.lbStatusText.AutoSize = true;
            this.lbStatusText.Location = new System.Drawing.Point(13, 608);
            this.lbStatusText.Name = "lbStatusText";
            this.lbStatusText.Size = new System.Drawing.Size(36, 13);
            this.lbStatusText.TabIndex = 5;
            this.lbStatusText.Text = "isError";
            // 
            // btnDeleteAllDB
            // 
            this.btnDeleteAllDB.Location = new System.Drawing.Point(723, 12);
            this.btnDeleteAllDB.Name = "btnDeleteAllDB";
            this.btnDeleteAllDB.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteAllDB.TabIndex = 17;
            this.btnDeleteAllDB.Text = "Delete All";
            this.btnDeleteAllDB.UseVisualStyleBackColor = true;
            this.btnDeleteAllDB.Click += new System.EventHandler(this.btnDeleteAllDB_Click);
            // 
            // btnAddImageDB
            // 
            this.btnAddImageDB.Location = new System.Drawing.Point(480, 12);
            this.btnAddImageDB.Name = "btnAddImageDB";
            this.btnAddImageDB.Size = new System.Drawing.Size(75, 23);
            this.btnAddImageDB.TabIndex = 18;
            this.btnAddImageDB.Text = "Add Imade";
            this.btnAddImageDB.UseVisualStyleBackColor = true;
            this.btnAddImageDB.Click += new System.EventHandler(this.btnAddImageDB_Click);
            // 
            // btnConnectDB
            // 
            this.btnConnectDB.Location = new System.Drawing.Point(165, 12);
            this.btnConnectDB.Name = "btnConnectDB";
            this.btnConnectDB.Size = new System.Drawing.Size(147, 23);
            this.btnConnectDB.TabIndex = 19;
            this.btnConnectDB.Text = "Connect  DB";
            this.btnConnectDB.UseVisualStyleBackColor = true;
            this.btnConnectDB.Click += new System.EventHandler(this.btnConnectDB_Click);
            // 
            // btnCreateNewDB
            // 
            this.btnCreateNewDB.Location = new System.Drawing.Point(12, 12);
            this.btnCreateNewDB.Name = "btnCreateNewDB";
            this.btnCreateNewDB.Size = new System.Drawing.Size(147, 23);
            this.btnCreateNewDB.TabIndex = 20;
            this.btnCreateNewDB.Text = "Create New DB";
            this.btnCreateNewDB.UseVisualStyleBackColor = true;
            this.btnCreateNewDB.Click += new System.EventHandler(this.btnCreateNewDB_Click);
            // 
            // lbCommand
            // 
            this.lbCommand.AutoSize = true;
            this.lbCommand.Location = new System.Drawing.Point(13, 586);
            this.lbCommand.Name = "lbCommand";
            this.lbCommand.Size = new System.Drawing.Size(54, 13);
            this.lbCommand.TabIndex = 21;
            this.lbCommand.Text = "Command";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(1074, 630);
            this.Controls.Add(this.lbCommand);
            this.Controls.Add(this.btnCreateNewDB);
            this.Controls.Add(this.btnConnectDB);
            this.Controls.Add(this.btnAddImageDB);
            this.Controls.Add(this.btnDeleteAllDB);
            this.Controls.Add(this.lbStatusText);
            this.Controls.Add(this.dgvViewer);
            this.Controls.Add(this.btnResetDB);
            this.Controls.Add(this.btnDeleteDB);
            this.Controls.Add(this.btnAddDB);
            this.Controls.Add(this.btnReadDB);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvViewer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReadDB;
        private System.Windows.Forms.Button btnAddDB;
        private System.Windows.Forms.Button btnDeleteDB;
        private System.Windows.Forms.Button btnResetDB;
        private System.Windows.Forms.DataGridView dgvViewer;
        private System.Windows.Forms.Label lbStatusText;
        private System.Windows.Forms.Button btnDeleteAllDB;
        private System.Windows.Forms.Button btnAddImageDB;
        private System.Windows.Forms.Button btnConnectDB;
        private System.Windows.Forms.Button btnCreateNewDB;
        private System.Windows.Forms.Label lbCommand;
    }
}

