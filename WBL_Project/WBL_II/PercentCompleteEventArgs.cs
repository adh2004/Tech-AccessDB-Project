using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBL_II
{
    class PercentCompleteEventArgs:EventArgs
    {
        public int PercentComplete { get; set; }
        public PercentCompleteEventArgs(int perComplete) { PercentComplete = perComplete; }
        
    }
}
