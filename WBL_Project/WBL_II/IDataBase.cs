using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBL_II
{
    interface IDataBase
    {
        void DBConnect();
        
        void DBDisconnect();
    }
}
