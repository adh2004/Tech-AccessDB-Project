using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace WBL_II
{
    class DBCollection: DataConnection
    {
        private string _query;
        private OleDbCommand _cmd;
        private DataSet _ds = new DataSet();
        private OleDbDataAdapter _da;
        private DataTable _dt = new DataTable();

        public DataTable DT { get { return _dt; } }
        public DBCollection(string query) { _query = query; }
        
        public void GetData()
        {
            DataSet ds = new DataSet();
            _cmd = new OleDbCommand(_query, _con);
            _cmd.CommandType = CommandType.Text;

            DBConnect();
            _da = new OleDbDataAdapter(_cmd);
            _da.Fill(_dt);
            DBDisconnect();
        }

    }
}
