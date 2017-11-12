using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Data;

namespace WBL_Project
{
    class Courses
    {
        private string _dBName;
        private string _query = "SELECT * FROM Classes";
        private string _tblName = "Programs";
        private DataSet _programDS = new DataSet();
        private OleDbCommand _cmd;
        private string _conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\WBL_ProjectDB.mdb";
        private OleDbConnection _con;
        private OleDbDataAdapter _da;
        private DataTable _classesdt = new DataTable();

        public string DbName
        {
            get
            {
                return _dBName;
            }
            set
            {
                _dBName = value;
            }

        }
        public DataSet DS
        {
            get { return _programDS; }
        }
        public DataTable DT { get { return _classesdt; } }
        public Courses(string DbName)
        {
            _dBName = DbName;
        }
        public void DBConnect()
        {
            _con = new OleDbConnection(_conStr);
            _cmd = new OleDbCommand(_query, _con);
            _cmd.CommandType = CommandType.Text;

            try
            {
                _con.Open();
                _da = new OleDbDataAdapter(_cmd);
                _da.Fill(_classesdt);
                //_Programdt = _programDS.Tables["Programs"];
                _con.Close();

            }
            catch (OleDbException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
