using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostTimeDB
{
    public class News
    {
        public int NewsID { get; set; }
        public DateTime NewsDate { get; set; }
        public string NewsTitle { get; set; }
        public string NewsContent { get; set; }
        public int AuthorID { get; set; } 
        public int UpVote { get; set; }
        public int DownVote { get; set; }

    }
}
