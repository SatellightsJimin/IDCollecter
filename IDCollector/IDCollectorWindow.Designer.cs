namespace IDCollector
{
    partial class IDCollectorWindow
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IDCollectorWindow));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.mainGridView = new System.Windows.Forms.DataGridView();
            this.checkFile = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.filePathColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.folderOpenButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.fileOpenColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.deleteFileColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.deleteAllFile = new System.Windows.Forms.Button();
            this.deselectBtn = new System.Windows.Forms.Button();
            this.deleteFile = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.folderLbl = new System.Windows.Forms.Label();
            this.selectFolderBtn = new System.Windows.Forms.Button();
            this.findBtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.idListView = new System.Windows.Forms.ListView();
            this.consoleTextBox = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainGridView)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.mainGridView, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.consoleTextBox, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // mainGridView
            // 
            this.mainGridView.AllowUserToAddRows = false;
            this.mainGridView.AllowUserToDeleteRows = false;
            this.mainGridView.AllowUserToResizeColumns = false;
            this.mainGridView.AllowUserToResizeRows = false;
            this.mainGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.mainGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mainGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkFile,
            this.filePathColumn,
            this.folderOpenButtonColumn,
            this.fileOpenColumn,
            this.deleteFileColumn});
            resources.ApplyResources(this.mainGridView, "mainGridView");
            this.mainGridView.Name = "mainGridView";
            this.mainGridView.RowHeadersVisible = false;
            this.mainGridView.RowTemplate.Height = 23;
            this.mainGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.mainGridView_CellClick);
            // 
            // checkFile
            // 
            this.checkFile.FalseValue = "false";
            resources.ApplyResources(this.checkFile, "checkFile");
            this.checkFile.Name = "checkFile";
            this.checkFile.TrueValue = "true";
            // 
            // filePathColumn
            // 
            this.filePathColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.filePathColumn, "filePathColumn");
            this.filePathColumn.Name = "filePathColumn";
            this.filePathColumn.ReadOnly = true;
            // 
            // folderOpenButtonColumn
            // 
            resources.ApplyResources(this.folderOpenButtonColumn, "folderOpenButtonColumn");
            this.folderOpenButtonColumn.Name = "folderOpenButtonColumn";
            this.folderOpenButtonColumn.Text = "폴더열기";
            this.folderOpenButtonColumn.UseColumnTextForButtonValue = true;
            // 
            // fileOpenColumn
            // 
            resources.ApplyResources(this.fileOpenColumn, "fileOpenColumn");
            this.fileOpenColumn.Name = "fileOpenColumn";
            this.fileOpenColumn.Text = "파일열기";
            this.fileOpenColumn.UseColumnTextForButtonValue = true;
            // 
            // deleteFileColumn
            // 
            resources.ApplyResources(this.deleteFileColumn, "deleteFileColumn");
            this.deleteFileColumn.Name = "deleteFileColumn";
            this.deleteFileColumn.Text = "삭제";
            this.deleteFileColumn.UseColumnTextForButtonValue = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.deleteAllFile);
            this.panel3.Controls.Add(this.deselectBtn);
            this.panel3.Controls.Add(this.deleteFile);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // deleteAllFile
            // 
            resources.ApplyResources(this.deleteAllFile, "deleteAllFile");
            this.deleteAllFile.Name = "deleteAllFile";
            this.deleteAllFile.UseVisualStyleBackColor = true;
            this.deleteAllFile.Click += new System.EventHandler(this.deleteFile_Click);
            // 
            // deselectBtn
            // 
            resources.ApplyResources(this.deselectBtn, "deselectBtn");
            this.deselectBtn.Name = "deselectBtn";
            this.deselectBtn.UseVisualStyleBackColor = true;
            this.deselectBtn.Click += new System.EventHandler(this.deleteFile_Click);
            // 
            // deleteFile
            // 
            resources.ApplyResources(this.deleteFile, "deleteFile");
            this.deleteFile.Name = "deleteFile";
            this.deleteFile.UseVisualStyleBackColor = true;
            this.deleteFile.Click += new System.EventHandler(this.deleteFile_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.folderLbl);
            this.panel1.Controls.Add(this.selectFolderBtn);
            this.panel1.Controls.Add(this.findBtn);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // folderLbl
            // 
            resources.ApplyResources(this.folderLbl, "folderLbl");
            this.folderLbl.Name = "folderLbl";
            // 
            // selectFolderBtn
            // 
            resources.ApplyResources(this.selectFolderBtn, "selectFolderBtn");
            this.selectFolderBtn.Name = "selectFolderBtn";
            this.selectFolderBtn.UseVisualStyleBackColor = true;
            this.selectFolderBtn.Click += new System.EventHandler(this.FolderSelect);
            // 
            // findBtn
            // 
            resources.ApplyResources(this.findBtn, "findBtn");
            this.findBtn.Name = "findBtn";
            this.findBtn.UseVisualStyleBackColor = true;
            this.findBtn.Click += new System.EventHandler(this.testerBtn_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.idListView);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // idListView
            // 
            resources.ApplyResources(this.idListView, "idListView");
            this.idListView.Name = "idListView";
            this.idListView.UseCompatibleStateImageBehavior = false;
            this.idListView.View = System.Windows.Forms.View.List;
            // 
            // consoleTextBox
            // 
            this.consoleTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            resources.ApplyResources(this.consoleTextBox, "consoleTextBox");
            this.consoleTextBox.Name = "consoleTextBox";
            this.consoleTextBox.ReadOnly = true;
            // 
            // IDCollectorWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "IDCollectorWindow";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainGridView)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button findBtn;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button selectFolderBtn;
        private System.Windows.Forms.Label folderLbl;
        private System.Windows.Forms.ListView idListView;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button deleteFile;
        private System.Windows.Forms.TextBox consoleTextBox;
        private System.Windows.Forms.Button deselectBtn;
        private System.Windows.Forms.Button deleteAllFile;
        private System.Windows.Forms.DataGridView mainGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn filePathColumn;
        private System.Windows.Forms.DataGridViewButtonColumn folderOpenButtonColumn;
        private System.Windows.Forms.DataGridViewButtonColumn fileOpenColumn;
        private System.Windows.Forms.DataGridViewButtonColumn deleteFileColumn;

    }
}

