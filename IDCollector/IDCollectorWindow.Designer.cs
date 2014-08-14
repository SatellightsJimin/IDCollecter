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
            this.panel1 = new System.Windows.Forms.Panel();
            this.folderLbl = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.allCases = new System.Windows.Forms.Button();
            this.resultListView = new System.Windows.Forms.ListView();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.deleteFile = new System.Windows.Forms.Button();
            this.onlyHyphen = new System.Windows.Forms.Button();
            this.mainGridView = new System.Windows.Forms.DataGridView();
            this.checkFile = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.filePathColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.foundIdColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.folderOpenButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.fileOpenColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.deleteFileColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.resultListView, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.mainGridView, 0, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.folderLbl);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.allCases);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // folderLbl
            // 
            resources.ApplyResources(this.folderLbl, "folderLbl");
            this.folderLbl.Name = "folderLbl";
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // allCases
            // 
            resources.ApplyResources(this.allCases, "allCases");
            this.allCases.Name = "allCases";
            this.allCases.UseVisualStyleBackColor = true;
            this.allCases.Click += new System.EventHandler(this.testerBtn_Click);
            // 
            // resultListView
            // 
            resources.ApplyResources(this.resultListView, "resultListView");
            this.resultListView.Name = "resultListView";
            this.resultListView.UseCompatibleStateImageBehavior = false;
            this.resultListView.View = System.Windows.Forms.View.Details;
            this.resultListView.SelectedIndexChanged += new System.EventHandler(this.resultListView_SelectedIndexChanged);
            this.resultListView.DoubleClick += new System.EventHandler(this.OpenExplorer);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.deleteFile);
            this.panel2.Controls.Add(this.onlyHyphen);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // deleteFile
            // 
            resources.ApplyResources(this.deleteFile, "deleteFile");
            this.deleteFile.Name = "deleteFile";
            this.deleteFile.UseVisualStyleBackColor = true;
            // 
            // onlyHyphen
            // 
            resources.ApplyResources(this.onlyHyphen, "onlyHyphen");
            this.onlyHyphen.Name = "onlyHyphen";
            this.onlyHyphen.UseVisualStyleBackColor = true;
            // 
            // mainGridView
            // 
            this.mainGridView.AllowUserToDeleteRows = false;
            this.mainGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.mainGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mainGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkFile,
            this.filePathColumn,
            this.foundIdColumn,
            this.folderOpenButtonColumn,
            this.fileOpenColumn,
            this.deleteFileColumn});
            resources.ApplyResources(this.mainGridView, "mainGridView");
            this.mainGridView.Name = "mainGridView";
            this.mainGridView.RowHeadersVisible = false;
            this.mainGridView.RowTemplate.Height = 23;
            this.mainGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // checkFile
            // 
            resources.ApplyResources(this.checkFile, "checkFile");
            this.checkFile.Name = "checkFile";
            // 
            // filePathColumn
            // 
            resources.ApplyResources(this.filePathColumn, "filePathColumn");
            this.filePathColumn.Name = "filePathColumn";
            this.filePathColumn.ReadOnly = true;
            // 
            // foundIdColumn
            // 
            resources.ApplyResources(this.foundIdColumn, "foundIdColumn");
            this.foundIdColumn.Name = "foundIdColumn";
            this.foundIdColumn.ReadOnly = true;
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
            // IDCollectorWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "IDCollectorWindow";
            this.Load += new System.EventHandler(this.IDCollectorWindow_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button allCases;
        private System.Windows.Forms.ListView resultListView;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label folderLbl;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button deleteFile;
        private System.Windows.Forms.Button onlyHyphen;
        private System.Windows.Forms.DataGridView mainGridView;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn filePathColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn foundIdColumn;
        private System.Windows.Forms.DataGridViewButtonColumn folderOpenButtonColumn;
        private System.Windows.Forms.DataGridViewButtonColumn fileOpenColumn;
        private System.Windows.Forms.DataGridViewButtonColumn deleteFileColumn;

    }
}

