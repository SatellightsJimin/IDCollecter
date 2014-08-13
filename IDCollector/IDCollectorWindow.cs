using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IDCollector
{
    public partial class IDCollectorWindow : Form
    {

        string path = @"c:\";
        public delegate void _AddList(string f);
        public IDCollectorWindow()
        {
            InitializeComponent();
            label1.Text = @"c:\";
      
        }

        private void testerBtn_Click(object sender, EventArgs e)
        {
            
            //Thread t1 = new Thread(new ParameterizedThreadStart(AddList));
            //t1.Start(@"d:\");

            listView1.Items.Clear();

            testerBtn.Enabled = false;
            button4.Enabled = false;
            label1.Text= "Searching...";
            this.Cursor = Cursors.WaitCursor;

            Application.DoEvents();

            FindFiles(path);

            label1.Text = "Search";
            this.Cursor = Cursors.Default;
            
            testerBtn.Enabled = true;
            button4.Enabled = true;
        }

        private void FindFiles(string sDir)
        {
            try
            {
                foreach (string f in Directory.GetFiles(sDir, "*.txt"))
                {
                    Console.WriteLine(f);
                    listView1.Items.Add(f);
                }
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    FindFiles(d);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }

        private void AddList(string f)
        {
            this.listView1.Items.Add(f);
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
            label1.Text = path; 
        }
    }
}
