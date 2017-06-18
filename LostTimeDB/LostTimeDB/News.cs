using System;
using System.Collections.Generic;
using System.Text;

namespace LostTimeDB
{
    public class News
    {
        public int NewsID { get; set; }

        public string NewsTitle { get; set; }

        public string NewsContent { get; set; }

        public DateTime NewsCreationDate { get; set; }

        public DateTime NewsLastUpdate { get; set; }

        public int NewsGoodVote { get; set; }

        public int NewsBadVote { get; set; }

        public int NewsEditionNb { get; set; }

        public int NewsAuthorID { get; set; }
    }
}
