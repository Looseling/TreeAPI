using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Diagnostics.View
{
    public class VJournalFilter
    {
        public DateTime? from { get; set; }
        public DateTime? to { get; set; }
        public string search { get; set; }
    }
}
