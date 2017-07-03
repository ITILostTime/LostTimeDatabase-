using System;
using System.Collections.Generic;
using System.Text;

namespace LostTimeDB
{
    public class Quest
    {
        public int QuestID { get; set; }

        public string QuestTitle { get; set; }

        public string QuestData { get; set; }

        public DateTime QuestLastEdit { get; set; }

        public int AuthorID { get; set; }
    }
}
