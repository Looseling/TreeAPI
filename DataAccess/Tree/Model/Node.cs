using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.Tree.Model
{
    public class Node
    {
        public int Id { get; set; }
        public string name { get; set; }
        [JsonIgnore]
        public bool isRoot { get; set; } = false;
        [JsonIgnore]
        public int? parentNodeId { get; set; }
        [JsonIgnore]
        public Node parentNode { get; set; }

        public IEnumerable<Node> children { get; set; }
    }
}
