using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
namespace WBL_Project
{
    class FileSave
    {
        private string _path;
        private GroupBox _grp;
        private string[] _progArray;
        public FileSave(string path, GroupBox grp,string[] programs)
        {
            _path = path;
            _grp = grp;
            _progArray = programs;
        }

        
        public void WriteToArray()
        {
            string line = string.Empty;

            foreach(var lbl in _grp.Controls.OfType<Label>())
            {
                line += lbl.Text + ",";
            }
            line += "\r\n";
        }
    }
}
