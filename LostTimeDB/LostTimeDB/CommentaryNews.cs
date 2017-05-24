using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostTimeDB
{
    public class CommentaryNews
    {
        public int CommentaryNewsID { get; set; }
        public DateTime CommentaryNewsDate { get; set; }
        public int NewsID { get; set; }
        public string CommentaryNewsContent { get; set; }
        public int CommentaryNewsAuthorID { get; set; }
    }
}
