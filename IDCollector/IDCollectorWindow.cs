using System;
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
    public partial class IDCollectorWindow : Form
    {
        StringBuilder IdStrings;
        string path = @"c:\";


        //정규식
        //1. 하이픈 있음
        string patternHyphen = "[0-9]{2}(0[1-9]|1[012])(0[1-9]|1[0-9]|2[0-9]|3[01])-[012349][0-9]{6}";
        //2 하이픈 없음
        string patternNoHyphen = "[0-9]{2}(0[1-9]|1[012])(0[1-9]|1[0-9]|2[0-9]|3[01])[012349][0-9]{6}";

        public delegate void _AddList(string f);

        public IDCollectorWindow()
        {
            InitializeComponent();
            folderLbl.Text = @"c:\";
            ColumnHeader pathHeader = new ColumnHeader();   // 헤더 생성
            pathHeader.Text = "파일주소";     // 헤더에 들어갈 텍스트
            pathHeader.Width = 300;
            pathHeader.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

            resultListView.Columns.Add(pathHeader);  //  ListView에 헤더컬럼 추가

            ColumnHeader idHeader = new ColumnHeader();   // 헤더 생성
            idHeader.Text = "주민등록번호";     // 헤더에 들어갈 텍스트
            idHeader.Width = 300;
            idHeader.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            resultListView.Columns.Add(idHeader);  //  ListView에 헤더컬럼 추가

            mainGridView.Columns[2].FillWeight = 200;
            mainGridView.Columns[1].FillWeight = 400;
            mainGridView.Columns[0].FillWeight = 50;



        }

        private void testerBtn_Click(object sender, EventArgs e)
        {

            resultListView.Items.Clear();


            button4.Enabled = false;
            folderLbl.Text = "Searching...";
            this.Cursor = Cursors.WaitCursor;

            System.Windows.Forms.Application.DoEvents();

            FindFiles(path, (Button)sender);

            MessageBox.Show("검색이 완료되었습니다");
            folderLbl.Text = path;
            this.Cursor = Cursors.Default;

            button4.Enabled = true;
        }

        private void FindFiles(string sDir, Button sender)
        {
            string fileContents = null; //문서가 가지고 있는 내용
            try
            {
                foreach (string filePath in Directory.GetFileSystemEntries(sDir, "*.*").Where(file => System.IO.Path.GetExtension(file).ToLower().Equals(".txt") || System.IO.Path.GetExtension(file).ToLower().Equals(".doc")
                        || System.IO.Path.GetExtension(file).ToLower().Equals(".docx") || System.IO.Path.GetExtension(file).ToLower().Equals(".xls")
                        || System.IO.Path.GetExtension(file).ToLower().Equals(".xlsx")))
                {
                    Console.WriteLine(filePath);
                    switch (System.IO.Path.GetExtension(filePath))
                    {
                        case ".txt":
                            fileContents = File.ReadAllText(filePath);

                            break;

                        case ".doc":
                        case ".docx":
                        case ".xls":
                        case ".xlsx":

                            fileContents = GetDocumentPlainText(sDir, filePath);

                            break;

                    }
                    switch (sender.Name)
                    {
                        case "allCases": // -가 있거나 없는 모든 주민번호
                            if (Regex.IsMatch(fileContents, patternHyphen) || Regex.IsMatch(fileContents, patternNoHyphen))
                            {
                                IdStrings = new StringBuilder();
                                foreach (var s in Regex.Matches(fileContents, patternHyphen))
                                {
                                    if (isId(s.ToString()))
                                        IdStrings.Append(s.ToString() + " ");
                                }
                                foreach (var s in Regex.Matches(fileContents, patternNoHyphen))
                                {
                                    if (isId(s.ToString()))
                                        IdStrings.Append(s.ToString() + " ");
                                }
                                if (!(IdStrings.Length == 0))
                                {
                                    ListViewItem listItem = new ListViewItem(filePath);
                                    listItem.SubItems.Add(IdStrings.ToString());
                                    resultListView.Items.Add(listItem);
                                    //resultListView.Items.Add(f + "   주민번호 : " + IdStrings.ToString());
                                }
                            }
                            break;
                        case "noHyphen": //하이픈 없는것
                            if (Regex.IsMatch(fileContents, patternNoHyphen))
                            {
                                IdStrings = new StringBuilder();
                                foreach (var s in Regex.Matches(fileContents, patternNoHyphen))
                                {
                                    if (isId(s.ToString()))
                                        IdStrings.Append(s.ToString() + " ");
                                }
                                if (!(IdStrings.Length == 0))
                                {
                                    ListViewItem listItem = new ListViewItem(filePath);
                                    listItem.SubItems.Add(IdStrings.ToString());
                                    resultListView.Items.Add(listItem);
                                }
                            }

                            break;
                        case "onlyHyphen": //하이픈 있는것만.
                            if (Regex.IsMatch(fileContents, patternHyphen))
                            {
                                IdStrings = new StringBuilder();
                                foreach (var s in Regex.Matches(fileContents, patternHyphen))
                                {
                                    if (isId(s.ToString()))
                                        IdStrings.Append(s.ToString() + " ");
                                }
                                if (!(IdStrings.Length == 0))
                                {
                                    ListViewItem listItem = new ListViewItem(filePath);
                                    listItem.SubItems.Add(IdStrings.ToString());
                                    resultListView.Items.Add(listItem);
                                }
                            }

                            break;
                    }
                }



            }
            catch
            {
            }

            if ((File.GetAttributes(sDir) & FileAttributes.ReparsePoint) != FileAttributes.ReparsePoint)
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    FindFiles(d, sender);
                }
            }



        }

        private void AddList(string f)
        {
            this.resultListView.Items.Add(f);
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
            int[] id = new int[13];
            int idx = 0;
            int key = 2;
            int result = 0;
            foreach (char s in _id)
            {

                if (!s.Equals('-'))
                {
                    id[idx] = Convert.ToInt32(s.ToString());
                    idx++;
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

        public string GetDocumentPlainText(string dir, string _fileName)
        {
            string plainText = string.Empty;
            string ext = System.IO.Path.GetExtension(_fileName);

            if (ext.Equals(".docx") || ext.Equals(".doc"))
            {
                //Create word document
                Spire.Doc.Document document = new Spire.Doc.Document();

                //load a document

                try
                {
                    document.LoadFromFile(_fileName);
                    plainText = document.GetText();
                }
                catch
                {
                }
            }
            else if (ext.Equals(".xlsx") || ext.Equals(".xls"))
            {

                Console.WriteLine(_fileName + "~~~ ");
                //Create Excel workbook
                Workbook workbook = new Workbook();

                //load a workbook
                workbook.LoadFromFile(_fileName);

                for (int i = 0; i < workbook.Worksheets.Count; i++)
                {
                    string tmpfilename = "tempSheet" + i.ToString() + ".txt";
                    Worksheet sheet = workbook.Worksheets[i];
                    if (!sheet.IsEmpty)
                    {
                        if (System.IO.File.Exists(dir + tmpfilename))
                        {
                            System.IO.File.Delete(dir + tmpfilename);
                        }
                        sheet.SaveToFile(tmpfilename, ", ", Encoding.UTF8);
                        plainText += "--[" + sheet.Name + "]--\r\n";
                        plainText += System.IO.File.ReadAllText(tmpfilename);
                        plainText += "\r\n";
                    }
                }
            }

            return plainText;
        }

        private void resultListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void OpenExplorer(object sender, EventArgs e)
        {

            string path = System.IO.Path.GetDirectoryName(resultListView.SelectedItems[0].Text.ToString());
            Console.WriteLine(path);
            Process.Start(path);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

}

