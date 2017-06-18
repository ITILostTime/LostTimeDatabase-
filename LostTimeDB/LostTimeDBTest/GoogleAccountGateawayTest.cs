using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LostTimeDBTest
{
    [TestFixture]
    public class GoogleAccountGatewayTest
    {
        readonly string _connectionstring = "Server=(local)\\SqlExpress; Database=LostTimeDB; Trusted_connection=true";

        [Test]
        public void Create_Assign_FindByGoogleAccount()
        {
            LostTimeDB.GoogleAccountGateway googleAccountGateway = new LostTimeDB.GoogleAccountGateway(_connectionstring);
            LostTimeDB.UserAccountGateway userAccountGateway = new LostTimeDB.UserAccountGateway(_connectionstring);
            LostTimeDB.UserAccount userAccount;
            LostTimeDB.UserAccount userAccount2;
            LostTimeDB.UserAccount userAccount3;

            string Pseudo = "PseudoTest";
            string Email = "Email@Test.com";
            byte[] Mdp = Guid.NewGuid().ToByteArray();

            string GoogleID = "123abc";
            string GoogleToken = "googleTokenTest";
            

            userAccountGateway.CreateNewUserAccount(Pseudo, Email, Mdp, DateTime.Now);

            userAccount = userAccountGateway.FindByName(Pseudo);

            Assert.That(userAccount.UserPseudonym, Is.EqualTo(Pseudo));

            googleAccountGateway.Create(GoogleID, GoogleToken);
            googleAccountGateway.AssignGoogleIDToUserID(GoogleID, GoogleToken, userAccount.UserID);

            userAccount2 = userAccountGateway.FindByName(Pseudo);

            Assert.That(userAccount2.UserGoogleID, Is.EqualTo(GoogleID));
            Assert.That(userAccount2.UserGoogleToken, Is.EqualTo(GoogleToken));

            userAccount3 = userAccountGateway.FindByGoogleID(GoogleID);

            Assert.That(userAccount3.UserGoogleID, Is.EqualTo(GoogleID));
            Assert.That(userAccount3.UserGoogleToken, Is.EqualTo(GoogleToken));

            userAccountGateway.DeleteUserAccountByName(Pseudo);
            googleAccountGateway.DeleteGoogleAccountByGoogleID(GoogleID);
        }

        [Test]
        public void UpdateGoogleToken_Assign()
        {
            LostTimeDB.UserAccountGateway userAccountGateway = new LostTimeDB.UserAccountGateway(_connectionstring);
            LostTimeDB.GoogleAccountGateway googleAccountGateway = new LostTimeDB.GoogleAccountGateway(_connectionstring);
            LostTimeDB.UserAccount userAccount;
            

            userAccountGateway.CreateNewUserAccount("Google", "Google@gmail.com", Guid.NewGuid().ToByteArray(), DateTime.Now);
            userAccount = userAccountGateway.FindByName("Google");
            Assert.That(userAccount.UserEmail, Is.EqualTo("Google@gmail.com"));

            googleAccountGateway.Create("GoogleID", "GoogleToken");
            googleAccountGateway.AssignGoogleIDToUserID("GoogleID", "GoogleToken", userAccount.UserID);
            userAccount = userAccountGateway.FindByName("Google");
            Assert.That(userAccount.UserGoogleID, Is.EqualTo("GoogleID"));
            Assert.That(userAccount.UserGoogleToken, Is.EqualTo("GoogleToken"));

            googleAccountGateway.UpdateGoogleToken("GoogleID", "NewGoogleToken");
            googleAccountGateway.AssignGoogleIDToUserID("GoogleID", "NewGoogleToken", userAccount.UserID);
            userAccount = userAccountGateway.FindByName("Google");
            Assert.That(userAccount.UserGoogleToken, Is.EqualTo("NewGoogleToken"));

            userAccountGateway.DeleteUserAccountByName("Google");
            googleAccountGateway.DeleteGoogleAccountByGoogleID("GoogleID");
        }
    }


}
