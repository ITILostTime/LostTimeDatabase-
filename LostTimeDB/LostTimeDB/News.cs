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
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int AuthorID { get; set; } 
        public int UpVote { get; set; }
        public int DownVote { get; set; }

    }
}
