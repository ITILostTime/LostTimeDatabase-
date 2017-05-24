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

            userAccountGateaway.DeleteUserAccountByUserID(userAccount2.UserID);
            userAccount2 = userAccountGateaway.FindByID(userAccount.UserID);
            Assert.That(userAccount2, Is.Null);
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
