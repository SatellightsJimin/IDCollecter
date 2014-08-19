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
//using Microsoft.Office.Interop.Word;

//MS WORD, MS EXCEL 파일 파싱에 Spire 무료 라이브러리 사용
using Spire.Doc;
using Spire.Xls;



namespace IDCollector
{
    public delegate void FindFilesDelegate(string sDir);
    
    public partial class IDCollectorWindow : Form
    {

        #region "변수 선언 및 조건 초기화"
        List<FilesPathAndID> filesPathAndIdList;
        string path = @"C:\";

        //주민번호 정규식
        string patternHyphen = "[0-9]{2}(0[1-9]|1[012])(0[1-9]|1[0-9]|2[0-9]|3[01])-?[1234][0-9]{6}";


        public IDCollectorWindow()
        {
            InitializeComponent();
            folderLbl.Text = @"C:\";

            mainGridView.Columns[1].FillWeight = 600;
            mainGridView.Columns[0].FillWeight = 40;
        }
        #endregion

        #region "검색 및 폴더변경"

        //검색 버튼 클릭
        private async void testerBtn_Click(object sender, EventArgs e)
        {
            cts = new CancellationTokenSource();
            //데이터그리드뷰 내용 비우기
            mainGridView.RowCount = 1;
            mainGridView.Rows.Clear();

            //발견된 주민등록번호 리스트 지우기
            idListView.Clear();

            //파일주소와 주민번호 저장하는 리스트 초기화
            filesPathAndIdList = new List<FilesPathAndID>();

            //UI비활성화
            selectFolderBtn.Enabled = false;
            findBtn.Enabled = false;
            mainGridView.Enabled = false;
            idListView.Enabled = false;
            deleteAllFile.Enabled = false;
            deleteFile.Enabled = false;
            deselectBtn.Enabled = false;


            folderLbl.Text = "검색 중...";

            //커서 비활성화
            this.Cursor = Cursors.WaitCursor;

            //메시지 큐의 모든 이벤트 처리.
            System.Windows.Forms.Application.DoEvents();

            //파일찾기 TASK 비동기 실행
            var FindFilesTask = System.Threading.Tasks.Task.Run(() => FindFiles(path));
            await FindFilesTask;

            //데이터그리드뷰에 자료 갱신

            foreach (FilesPathAndID fi in filesPathAndIdList)
            {
                mainGridView.Rows.Add(false, fi.FilesPath);
            }

            MessageBox.Show("검색이 완료되었습니다");
            consoleTextBox.Text = "검색 완료";

            //ui정보 재설정 및 재활성화
            folderLbl.Text = path;
            this.Cursor = Cursors.Default;
            mainGridView.Enabled = true;
            idListView.Enabled = true;
            findBtn.Enabled = true;
            deleteAllFile.Enabled = true;
            deleteFile.Enabled = true;
            deselectBtn.Enabled = true;
            selectFolderBtn.Enabled = true;

        }

        //폴더변경
        private void FolderSelect(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (!result.Equals(DialogResult.Cancel))
            {
                path = folderBrowserDialog1.SelectedPath;
                folderLbl.Text = path;
            }
        }
        #endregion

        #region "조건에 맞는 파일찾기"

        //파일에서 문자열 읽기
        private void FindFiles(object _sDir)
        {
            string sDir = _sDir as string;
            //string fileContents = null; //문서가 가지고 있는 내용
            List<string> idList = null; //파일별 발견된 주민등록번호들

            // Spire.Doc.Documents.TextSelection[] collectedId = null; //정규식에 의해 검색된 주민번호의 리스트.

            foreach (string filePath in Directory.GetFileSystemEntries(sDir, "*.*")
                .Where(file => System.IO.Path.GetExtension(file).ToLower().Equals(".txt")
                    || System.IO.Path.GetExtension(file).ToLower().Equals(".doc")
                    || System.IO.Path.GetExtension(file).ToLower().Equals(".docx")
                    || System.IO.Path.GetExtension(file).ToLower().Equals(".xls")
                    || System.IO.Path.GetExtension(file).ToLower().Equals(".xlsx")))
            {


                // 크로스스레드 해결
                this.Invoke(new MethodInvoker(delegate()
                {
                    consoleTextBox.Text = filePath + " 읽는중..";
                }));

                try
                {
                    string plainText = "";
                    switch (System.IO.Path.GetExtension(filePath))
                    {
                        //TXT파일 
                        case ".txt":
                            idList = new List<string>();
                            plainText = File.ReadAllText(filePath);
                            plainText = plainText.Replace(" ", "");
                            foreach (Match s in Regex.Matches(plainText, patternHyphen))
                            {
                                if (isId(s.Value.ToString()))
                                {
                                    idList.Add(s.ToString());
                                }
                            }
                            break;

                        //MS WORD 파일
                        case ".doc":
                        case ".docx":
                            if (System.IO.File.Exists("temp"))
                            {
                                System.IO.File.Delete("temp");
                            }
                            idList = new List<string>();
                            Spire.Doc.Document document = new Spire.Doc.Document();
                            document.LoadFromFile(filePath);
                            document.SaveToTxt("temp", Encoding.UTF8);
                            plainText = System.IO.File.ReadAllText("temp");
                            foreach (Match s in Regex.Matches(plainText, patternHyphen))
                            {
                                if (isId(s.Value.ToString()))
                                {
                                    idList.Add(s.ToString());
                                }
                            }
                            //collectedId = document.FindAllPattern(new Regex(patternHyphen));
                            //foreach (Spire.Doc.Documents.TextSelection s in collectedId)
                            //{
                            //    if (isId(s.SelectedText.ToString()))
                            //    {
                            //        idList.Add(s.SelectedText.ToString());
                            //    }
                            //}
                            System.IO.File.Delete("temp");
                            break;

                        //MS EXCEL파일
                        case ".xls":
                        case ".xlsx":
                            plainText = "";
                            idList = new List<string>();
                            Workbook workbook = new Workbook();

                            //엑셀파일오픈
                            workbook.LoadFromFile(filePath);

                            //각 워크시트별로 파싱
                            for (int i = 0; i < workbook.Worksheets.Count; i++)
                            {
                                Worksheet sheet = workbook.Worksheets[i];
                                if (!sheet.IsEmpty)
                                {
                                    if (System.IO.File.Exists("temp"))
                                    {
                                        System.IO.File.Delete("temp");
                                    }

                                    //전체 text 읽기 불가. txt파일 저장 후 파싱
                                    sheet.SaveToFile("temp", "|", Encoding.UTF8);
                                    plainText += System.IO.File.ReadAllText("temp");
                                    plainText += "\r\n";
                                }

                            }
                            plainText = plainText.Replace(" ", ""); //공백제거
                            foreach (Match s in Regex.Matches(plainText, patternHyphen))
                            {
                                if (isId(s.Value.ToString()))
                                {
                                    idList.Add(s.ToString());
                                }
                            }
                            System.IO.File.Delete("temp");
                            break;
                    }
                }
                catch
                {
                }

                //발견된 파일이 있으면 리스트에 등록
                if (idList.Count != 0)
                {
                    filesPathAndIdList.Add(new FilesPathAndID(filePath, idList));
                }
            }

            //재귀적으로 디렉토리 탐색
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
            //2000년대생 판별 (뒷자리 첫번쩨 수가 3,4이면 2000년대생)
            if (id[6] == 3 || id[6] == 4)
            {
                int birthDay = Convert.ToInt32(ID.Substring(0, 6));
                if (birthDay > Convert.ToInt32(DateTime.Now.ToString("yymmdd")))
                {
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
        #endregion

        #region "데이터그리드 뷰의 셀 클릭시 행동 정의"
        private void mainGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //ROW의 아무 CELL 클릭시 주민번호 리스트 비움
            idListView.Clear();
            string path = null;
            //현재 클릭한 ROW의 파일주소
            if (e.RowIndex >= 0)
            {
                path = mainGridView["filePathColumn", e.RowIndex].Value.ToString();
            }



            try
            {


                mainGridView.Refresh();
                switch (e.ColumnIndex)
                {
                    case 1:
                        foreach (FilesPathAndID fid in filesPathAndIdList)
                        {
                            if (fid.FilesPath.Equals(path))
                            {
                                foreach (string s in fid.ID)
                                {
                                    idListView.Items.Add(s);
                                }
                            }
                        }
                        break;
                    case 2:
                        Process.Start(System.IO.Path.GetDirectoryName(path));
                        break;
                    case 3:
                        Process.Start(path);
                        break;
                    case 4:
                        DialogResult result = MessageBox.Show("파일을 삭제하시겠습니까?", "경고", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            System.IO.File.Delete(path);
                            MessageBox.Show("파일이 정상적으로 삭제되었습니다");
                            mainGridView.Rows.RemoveAt(e.RowIndex);
                            mainGridView.Refresh();
                        }
                        else
                        {
                        }
                        break;

                }
            }
            catch (Exception ex)
            {
            }

        }
        #endregion

        #region "삭제 및 선택해제 버튼 행동 정의"
        private void deleteFile_Click(object sender, EventArgs e)
        {

            if (mainGridView.RowCount == 0)
            {
                MessageBox.Show("리스트에 파일이 없습니다.", "오류");
                return;
            }
            Button senderBtn = sender as Button;
            string messageString = String.Empty;
            switch (senderBtn.Name)
            {
                case "deleteAllFile":
                    messageString = "모든 파일을 삭제하시겠습니까?";
                    break;
                case "deleteFile":
                    messageString = "선택된 파일을 삭제하시겠습니까?";
                    break;
                case "deselectBtn":
                    messageString = "선택을 취소하시겠습니까?";
                    break;
            }

            if (!senderBtn.Text.Equals("deselectBtn"))
            {
                DialogResult result = MessageBox.Show(messageString, "경고", MessageBoxButtons.YesNo);
                List<DataGridViewRow> deleteRows = new List<DataGridViewRow>();
                if (result == DialogResult.Yes)
                {

                    foreach (DataGridViewRow row in mainGridView.Rows)
                    {
                        if (senderBtn.Name.Equals("deleteFile"))
                        {
                            if (Convert.ToBoolean(row.Cells["checkFile"].Value) == true)
                            {
                                Console.WriteLine(row.Cells["filePathColumn"].Value.ToString());
                                System.IO.File.Delete(row.Cells["filePathColumn"].Value.ToString());
                                deleteRows.Add(row);
                            }

                        }
                        else if (senderBtn.Name.Equals("deleteAllFile"))
                        {
                            System.IO.File.Delete(row.Cells["filePathColumn"].Value.ToString());
                            deleteRows.Add(row);
                        }
                        else
                        {
                            if (Convert.ToBoolean(row.Cells["checkFile"].Value) == true)
                            {
                                row.Cells["checkFile"].Value = false;
                            }
                        }
                    }
                    if (deleteRows.Count != 0)
                    {
                        foreach (DataGridViewRow row in deleteRows)
                        {
                            mainGridView.Rows.Remove(row);
                        }
                    }
                    if (deleteRows == null && senderBtn.Name.Equals("deleteFile"))
                    {
                        MessageBox.Show("선택된 파일이 없습니다.", "오류");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("작업이 완료되었습니다", "작업완료");
                    }
                    mainGridView.Refresh();

                }
            }
        }
        #endregion

    }

}

