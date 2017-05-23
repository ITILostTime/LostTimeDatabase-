using System;

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

        public string UserGoogleToken { get; set; }

        public int UserGoogleID { get; set; }
    }
}
