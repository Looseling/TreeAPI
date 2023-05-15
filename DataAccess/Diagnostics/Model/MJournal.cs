using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Diagnostics.Model
{
    public class MJournal
    {
        public int Id { get; set; }
        public int eventId { get; set; }
        public string text { get; set; } = string.Empty;
        public DateTime createdAt { get; set; }
    }
}
