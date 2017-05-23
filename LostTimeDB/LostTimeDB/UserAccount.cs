using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostTimeDB
{
    public class UserAccount
    {
        public int UserID { get; set; }

        public string UserPseudonym { get; set; }

        public string UserEmail { get; set; }

        public string UserPassword { get; set; }

        public DateTime UserAccountCreationDate { get; set; }

        public DateTime UserLastConnectionDate { get; set; }
    }
}
