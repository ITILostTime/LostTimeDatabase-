using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LostTimeDBTest
{
    [TestFixture]
    public class GoogleAccountGateawayTest
    {
        readonly string _connectionstring = "Server=(local)\\SqlExpress; Database=LostTimeDB; Trusted_connection=true";

        [Test]
        public void Create_Assign_FindByGoogleAccount()
        {
            LostTimeDB.GoogleAccountGateaway googleAccountGateaway = new LostTimeDB.GoogleAccountGateaway(_connectionstring);
            LostTimeDB.UserAccountGateaway userAccountGateaway = new LostTimeDB.UserAccountGateaway(_connectionstring);
            LostTimeDB.UserAccount userAccount;
            LostTimeDB.UserAccount userAccount2;
            LostTimeDB.UserAccount userAccount3;

            string Pseudo = "PseudoTest";
            string Email = "Email@Test.com";
            string Mdp = "MdpTest";

            int GoogleID = 123;
            string GoogleToken = "googleTokenTest";
            

            userAccountGateaway.CreateNewUserAccount(Pseudo, Email, Mdp, DateTime.Now);

            userAccount = userAccountGateaway.FindByName(Pseudo);

            Assert.That(userAccount.UserPseudonym, Is.EqualTo(Pseudo));

            googleAccountGateaway.Create(GoogleID, GoogleToken);
            googleAccountGateaway.AssignGoogleIDToUserID(GoogleID, GoogleToken, userAccount.UserID);

            userAccount2 = userAccountGateaway.FindByName(Pseudo);

            Assert.That(userAccount2.UserGoogleID, Is.EqualTo(GoogleID));
            Assert.That(userAccount2.UserGoogleToken, Is.EqualTo(GoogleToken));

            userAccount3 = userAccountGateaway.FindByGoogleID(GoogleID);

            Assert.That(userAccount3.UserGoogleID, Is.EqualTo(GoogleID));
            Assert.That(userAccount3.UserGoogleToken, Is.EqualTo(GoogleToken));

            userAccountGateaway.DeleteUserAccountByName(Pseudo);
            googleAccountGateaway.DeleteGoogleAccountByGoogleID(GoogleID);
        }
    }


}
