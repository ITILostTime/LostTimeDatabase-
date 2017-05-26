using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LostTimeDBTest
{
    [TestFixture]
    public class UserAccountGateawayTest
    {
        readonly string _connectionstring = "Server=(local)\\SqlExpress; Database=LostTimeDB; Trusted_connection=true";


        [Test]
        public void Create_Update_Delete_UserAccount()
        {
            LostTimeDB.UserAccountGateaway userAccountGateaway = new LostTimeDB.UserAccountGateaway(_connectionstring);
            LostTimeDB.UserAccount userAccount;
            LostTimeDB.UserAccount userAccount2;

            string testUserPseudonym = "TestUserPseudonymTest1";
            string testUserEmail = "TestUserEmailTest1";
            string testUserPassword = "TestUserPasswordTest1";


            string TestUserUpdatePseudonym = "TestUserUpdatePseudonymTest1";
            string TestUserUpdateEmail = "TestUserUpdateEmailTest1";
            string TestUserUpdatePassword = "TestUserUpdatePasswordTest1";

            userAccountGateaway.CreateNewUserAccount(testUserPseudonym, testUserEmail, testUserPassword, DateTime.Now);

            userAccount = userAccountGateaway.FindByName(testUserPseudonym);
            CheckUserAccount(userAccount, userAccount.UserID, testUserPseudonym, testUserEmail, testUserPassword, userAccount.UserAccountCreationDate);

            userAccountGateaway.UpdateUserAccount(userAccount.UserID, TestUserUpdatePseudonym, TestUserUpdateEmail, TestUserUpdatePassword);
            userAccount2 = userAccountGateaway.FindByName(TestUserUpdatePseudonym);
            CheckUserAccount(userAccount2, userAccount2.UserID, TestUserUpdatePseudonym, TestUserUpdateEmail, TestUserUpdatePassword, userAccount2.UserAccountCreationDate);

            userAccountGateaway.DeleteUserAccountByName(TestUserUpdatePseudonym);
            userAccount = userAccountGateaway.FindByName(TestUserUpdatePseudonym);
            Assert.That(userAccount, Is.Null);

        }

        [Test]
        public void FindByID_DeleteByIDTest()
        {
            LostTimeDB.UserAccountGateaway userAccountGateaway = new LostTimeDB.UserAccountGateaway(_connectionstring);
            LostTimeDB.UserAccount userAccount;
            LostTimeDB.UserAccount userAccount2;

            string testUserPseudonym = "TestFindByIDPseudonymTest2";
            string testUserEmail = "TestFindByIDEmailTest2";
            string testUserPassword = "TestFindByIDPasswordTest2";

            userAccountGateaway.CreateNewUserAccount(testUserPseudonym, testUserEmail, testUserPassword, DateTime.Now);

            userAccount = userAccountGateaway.FindByName(testUserPseudonym);
            userAccount2 = userAccountGateaway.FindByID(userAccount.UserID);
            Assert.That(userAccount.UserID, Is.EqualTo(userAccount2.UserID));
            Assert.That(userAccount.UserGoogleID, Is.EqualTo(0));
            Assert.That(userAccount.UserGoogleToken, Is.EqualTo(null));

            userAccountGateaway.DeleteUserAccountByUserID(userAccount2.UserID);
            userAccount2 = userAccountGateaway.FindByID(userAccount.UserID);
            Assert.That(userAccount2, Is.Null);
        }

        [Test]
        public void DeleteByGoogleIDTest()
        {
            LostTimeDB.UserAccountGateaway userAccountGateaway = new LostTimeDB.UserAccountGateaway(_connectionstring);
            LostTimeDB.GoogleAccountGateaway googleAccountGateaway = new LostTimeDB.GoogleAccountGateaway(_connectionstring);

            LostTimeDB.UserAccount userAccount;
            LostTimeDB.UserAccount userAccount2;

            string testUserPseudonym = "TestFindByIDPseudonymTest3";
            string testUserEmail = "TestFindByIDEmailTest3";
            string testUserPassword = "TestFindByIDPasswordTest3";

            int googleID = 321;
            string googleToken = "googleTokenTest3";

            userAccountGateaway.CreateNewUserAccount(testUserPseudonym, testUserEmail, testUserPassword, DateTime.Now);

            userAccount = userAccountGateaway.FindByName(testUserPseudonym);

            googleAccountGateaway.Create(googleID, googleToken);
            googleAccountGateaway.AssignGoogleIDToUserID(googleID, googleToken, userAccount.UserID);

            userAccount = userAccountGateaway.FindByName(testUserPseudonym);

            Assert.That(userAccount.UserGoogleID, Is.EqualTo(googleID));

            userAccountGateaway.DeleteUserAccountByGoogleID(userAccount.UserGoogleID);

            userAccount2 = userAccountGateaway.FindByGoogleID(userAccount.UserGoogleID);

            Assert.That(userAccount2, Is.Null);

            googleAccountGateaway.DeleteGoogleAccountByGoogleID(googleID);
        }

        void CheckUserAccount(LostTimeDB.UserAccount userAccount, int userID, string userPseudonym, string userEmail, string userPassword, DateTime creationDate)
        {
            Assert.That(userAccount.UserID, Is.EqualTo(userID));
            Assert.That(userAccount.UserPseudonym, Is.EqualTo(userPseudonym));
            Assert.That(userAccount.UserEmail, Is.EqualTo(userEmail));
            Assert.That(userAccount.UserPassword, Is.EqualTo(userPassword));
            Assert.That(userAccount.UserAccountCreationDate, Is.EqualTo(creationDate));
        }
    }
}
