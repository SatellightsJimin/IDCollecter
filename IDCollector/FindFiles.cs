using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IDCollector
{
    public static class FindFiles
    {
        public static List<String> DirSearch(string sDir)
        {
            List<String> files = new List<String>();
            try
            {
                foreach (string f in Directory.GetFiles(sDir, "*.txt"))
                {
                    //Console.WriteLine(f);
                    files.Add(f);
                }
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    //files.AddRange(DirSearch(d));
                    DirSearch(d);
                }
            }
            catch (System.Exception excpt)
            {
               // MessageBox.Show(excpt.Message);
            }

           return files;
        }
    }
}
