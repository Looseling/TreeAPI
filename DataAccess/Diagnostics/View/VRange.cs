using DataAccess.Diagnostics.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Diagnostics.View
{
    public class VRange
    {
        public int skip { get; set; }
        public int count { get; set; }
        public List<VJournalInfo> items { get; set; }
    }
}
