using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Configuration;

namespace WBL_II
{
    class DataConnection : IDataBase
    {
        private Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        protected string _conStr;
        protected OleDbConnection _con;

        public DataConnection()
        {
            _conStr = _config.AppSettings.Settings["Path"].Value;
            _con = new OleDbConnection(_conStr);
        }
        public void DBConnect()
        {
           
            try
            {
                if(_con.State == System.Data.ConnectionState.Closed)
                {
                    _con.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Connecting to Database: " + ex.Message);
            }
        }   

        public void DBDisconnect()
        {
            try
            {
                _con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error Disconnecting from database: " + ex.Message);
            }
        }
    }
}
