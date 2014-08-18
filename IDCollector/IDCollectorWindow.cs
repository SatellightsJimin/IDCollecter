using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using Spire.Doc;
using Spire.Xls;



namespace IDCollector
{
    public delegate void FindFilesDelegate(string sDir);
   
    public partial class IDCollectorWindow : Form
    {
        List<FilesPathAndID> filesPathAndIdList;

        StringBuilder IdStrings;
        string path = @"c:\";
        //정규식
        //1. 하이픈 있음
        string patternHyphen = "[0-9]{2}(0[1-9]|1[012])(0[1-9]|1[0-9]|2[0-9]|3[01])-?[1234][0-9]{6}";
        //2 하이픈 없음
       // string patternNoHyphen = "[0-9]{2}(0[1-9]|1[012])(0[1-9]|1[0-9]|2[0-9]|3[01])[012349][0-9]{6}";
        
        
        public delegate void _AddList(string f);




        public IDCollectorWindow()
        {
            InitializeComponent();
            folderLbl.Text = @"c:\";

            mainGridView.Columns[2].FillWeight = 200;
            mainGridView.Columns[1].FillWeight = 400;
            mainGridView.Columns[0].FillWeight = 50;

            
            
        }

        private async void testerBtn_Click(object sender, EventArgs e)
        {

            mainGridView.RowCount = 1;
            mainGridView.Rows.Clear();
            int rowIdx = 0;
            filesPathAndIdList= new List<FilesPathAndID>();

            button4.Enabled = false;
            findBtn.Enabled = false;
            folderLbl.Text = "Searching...";
            this.Cursor = Cursors.WaitCursor;


            System.Windows.Forms.Application.DoEvents();
            var FindFilesTask = System.Threading.Tasks.Task.Run(() => FindFiles(path));
            
            //var SearchingText = System.Threading.Tasks.Task.Run(() => searching_Text());
            //await SearchingText;

            await FindFilesTask;

            foreach (FilesPathAndID fi in filesPathAndIdList)
            {
               
                //dgvcbc.Items.AddRange(fi.ID.ToArray<string>());
           
                mainGridView.Rows.Add(false,fi.FilesPath);
                DataGridViewComboBoxCell dgvcbc = mainGridView["foundIdColumn",rowIdx] as DataGridViewComboBoxCell;
                dgvcbc.DataSource = fi.ID.ToArray<string>();
                rowIdx++;
                
            }


            MessageBox.Show("검색이 완료되었습니다");
            folderLbl.Text = path;
            this.Cursor = Cursors.Default;

            findBtn.Enabled = true;
            button4.Enabled = true;
        }

        private void SetTextCallBack(string s)
        {
            this.consoleTextBox.Text = s + "\n";
        }
        private void FindFiles(string sDir)
        {
            string fileContents = null; //문서가 가지고 있는 내용
            List<string> idList=null;
            IdStrings = new StringBuilder();
            Spire.Doc.Documents.TextSelection[] collectedId = null;

            foreach (string filePath in Directory.GetFileSystemEntries(sDir, "*.*")
                .Where(file => System.IO.Path.GetExtension(file).ToLower().Equals(".txt")
                    || System.IO.Path.GetExtension(file).ToLower().Equals(".doc")
                    || System.IO.Path.GetExtension(file).ToLower().Equals(".docx")
                    || System.IO.Path.GetExtension(file).ToLower().Equals(".xls")
                    || System.IO.Path.GetExtension(file).ToLower().Equals(".xlsx")))
            {



                this.Invoke(new MethodInvoker(delegate()
                {
                    consoleTextBox.Text = filePath;
                }));

                try
                {
         
                    switch (System.IO.Path.GetExtension(filePath))
                    {
                        case ".txt":
                            idList = new List<string>();
                            fileContents = File.ReadAllText(filePath);
                            foreach (Match s in Regex.Matches(fileContents, patternHyphen))
                            {
                                if (isId(s.Value.ToString()))
                                {
                                    IdStrings.Append(s.ToString() + " ");
                                    idList.Add(s.ToString());
                                }
                            }
                            //foreach (Match s in Regex.Matches(fileContents, patternNoHyphen))
                            //{
                            //    if (isId(s.ToString()))
                            //        IdStrings.Append(s.ToString() + " ");
                            //}
                            break;

                        case ".doc":
                        case ".docx":
                            idList = new List<string>();
                            Spire.Doc.Document document = new Spire.Doc.Document();
                            document.LoadFromFile(filePath);
                            collectedId = document.FindAllPattern(new Regex(patternHyphen));
                            foreach (Spire.Doc.Documents.TextSelection s in collectedId)
                            {
                                if (isId(s.SelectedText.ToString()))
                                {
                                    IdStrings.Append(s.SelectedText.ToString() + " ");
                                    idList.Add(s.SelectedText.ToString());
                                }
                            }

                            //collectedId = document.FindAllPattern(new Regex(patternNoHyphen));
                            //foreach (Spire.Doc.Documents.TextSelection s in collectedId)
                            //{
                            //    if (isId(s.SelectedText.ToString()))
                            //        IdStrings.Append(s.SelectedText.ToString() + " ");
                            //}
                            break;
                        case ".xls":
                        case ".xlsx":
                            idList = new List<string>();
                            string plainText = "";
                            Workbook workbook = new Workbook();

                            //load a workbook
                            workbook.LoadFromFile(filePath);

                            for (int i = 0; i < workbook.Worksheets.Count; i++)
                            {
              
                                Worksheet sheet = workbook.Worksheets[i];


                                if (!sheet.IsEmpty)
                                {
                                    if (System.IO.File.Exists("temp"))
                                    {
                                        System.IO.File.Delete("temp");
                                    }
                                    sheet.SaveToFile("temp", ", ", Encoding.UTF8);
                                    plainText += System.IO.File.ReadAllText("temp");
                                    plainText += "\r\n";
                                }
                                
                            }
                            foreach (Match s in Regex.Matches(plainText, patternHyphen))
                            {
                                if (isId(s.Value.ToString()))
                                {
                                    IdStrings.Append(s.ToString() + " ");
                                    idList.Add(s.ToString());
                                }
                            }
                            System.IO.File.Delete("temp");
                            //foreach (Match s in Regex.Matches(plainText, patternNoHyphen))
                            //{
                            //    if (isId(s.Value.ToString()))
                            //        IdStrings.Append(s.ToString() + " ");
                            //}
                            break;
                    }
                }
                catch
                {
                }


                if (idList.Count != 0)
                {
                    //resultListView.Items.Add(f + "   주민번호 : " + IdStrings.ToString());
                    filesPathAndIdList.Add(new FilesPathAndID(filePath, idList));
                    
                }

            }

            if ((File.GetAttributes(sDir) & FileAttributes.ReparsePoint) != FileAttributes.ReparsePoint)
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    try
                    {
                        FindFiles(d);
                    }
                    catch
                    {
                    }
                }
            }

        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void IDCollectorWindow_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            path = folderBrowserDialog1.SelectedPath;
            folderLbl.Text = path;
        }

        //올바른 주민번호 판별 로직.
        private bool isId(string _id)
        {
            string ID = _id.Replace("-", "");
            int[] id = new int[13];
            int idx = 0;
            int key = 2;
            int result = 0;
            foreach (char s in ID)
            {
                id[idx] = Convert.ToInt32(s.ToString());
                idx++;
            }
            //2000년대생 판별
            if (id[6] == 3 || id[6] == 4)
            {
                int birthDay = Convert.ToInt32(ID.Substring(0, 6));
                if(birthDay > Convert.ToInt32(DateTime.Now.ToString("yymmdd"))){
                    return false;
                }
            }
            for (int i = 0; i < 12; i++)
            {
                if (key > 9)
                {
                    key = 2;
                }
                result = result + (id[i] * key);
                key++;
            }
            if ((11 - (result % 11)) == id[12])
            {
                return true;
            }
            return false;
        }




        private void resultListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void OpenExplorer(object sender, EventArgs e)
        {

            //string path = System.IO.Path.GetDirectoryName(resultListView.SelectedItems[0].Text.ToString());
            //Console.WriteLine(path);
            //Process.Start(path);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            idListView.Clear();
            foreach (string s in filesPathAndIdList[e.RowIndex].ID)
            {
                idListView.Items.Add(s);
            }
            mainGridView.Refresh();
            switch(e.ColumnIndex){
                case 3:
                    string path = System.IO.Path.GetDirectoryName(mainGridView["filePathColumn",e.RowIndex].Value.ToString());
                    Process.Start(path);
                    break;
            }  
        
        }
        private void FolderOpenClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

}

