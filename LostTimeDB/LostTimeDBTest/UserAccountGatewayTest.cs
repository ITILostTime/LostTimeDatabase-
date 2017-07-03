using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LostTimeDBTest
{
    [TestFixture]
    public class UserAccountGatewayTest
    {
        readonly string _connectionstring = "Server=(local)\\SqlExpress; Database=LostTimeDB; Trusted_connection=true";


        [Test]
        public void Create_Update_Delete_UserAccount()
        {
            LostTimeDB.UserAccountGateway userAccountGateaway = new LostTimeDB.UserAccountGateway(_connectionstring);
            LostTimeDB.UserAccount userAccount;
            LostTimeDB.UserAccount userAccount2;

            string testUserPseudonym = "TestUserPseudonymTest1";
            string testUserEmail = "TestUserEmailTest1";
            byte[] testUserPassword = Guid.NewGuid().ToByteArray();


            string TestUserUpdatePseudonym = "TestUserUpdatePseudonymTest1";
            string TestUserUpdateEmail = "TestUserUpdateEmailTest1";
            byte[] TestUserUpdatePassword = Guid.NewGuid().ToByteArray();

            userAccountGateaway.CreateNewUserAccount(testUserPseudonym, testUserEmail, testUserPassword, DateTime.Now);

            userAccount = userAccountGateaway.FindByName(testUserPseudonym);
            
            Assert.That(userAccount.UserPseudonym, Is.EqualTo(testUserPseudonym));
            Assert.That(userAccount.UserEmail, Is.EqualTo(testUserEmail));
            Assert.That(userAccount.UserPassword, Is.EqualTo(testUserPassword));

            userAccountGateaway.UpdateUserAccount(userAccount.UserID, TestUserUpdatePseudonym, TestUserUpdateEmail, TestUserUpdatePassword);
            userAccount2 = userAccountGateaway.FindByName(TestUserUpdatePseudonym);
            
            Assert.That(userAccount2.UserPseudonym, Is.EqualTo(TestUserUpdatePseudonym));
            Assert.That(userAccount2.UserEmail, Is.EqualTo(TestUserUpdateEmail));
            Assert.That(userAccount2.UserPassword, Is.EqualTo(TestUserUpdatePassword));

            userAccountGateaway.DeleteUserAccountByName(TestUserUpdatePseudonym);
            userAccount = userAccountGateaway.FindByName(TestUserUpdatePseudonym);
            Assert.That(userAccount, Is.Null);

        }

        [Test]
        public void FindByID_DeleteByIDTest()
        {
            LostTimeDB.UserAccountGateway userAccountGateaway = new LostTimeDB.UserAccountGateway(_connectionstring);
            LostTimeDB.UserAccount userAccount;
            LostTimeDB.UserAccount userAccount2;

            string testUserPseudonym = "TestFindByIDPseudonymTest2";
            string testUserEmail = "TestFindByIDEmailTest2";
            byte[] testUserPassword = Guid.NewGuid().ToByteArray();

            userAccountGateaway.CreateNewUserAccount(testUserPseudonym, testUserEmail, testUserPassword, DateTime.Now);

            userAccount = userAccountGateaway.FindByName(testUserPseudonym);
            userAccount2 = userAccountGateaway.FindByID(userAccount.UserID);
            Assert.That(userAccount.UserID, Is.EqualTo(userAccount2.UserID));
            Assert.That(userAccount.UserGoogleID, Is.EqualTo(null));
            Assert.That(userAccount.UserGoogleToken, Is.EqualTo(null));

            userAccountGateaway.DeleteUserAccountByUserID(userAccount2.UserID);
            userAccount2 = userAccountGateaway.FindByID(userAccount.UserID);
            Assert.That(userAccount2, Is.Null);
        }

        [Test]
        public void DeleteByGoogleIDTest()
        {
            LostTimeDB.UserAccountGateway userAccountGateaway = new LostTimeDB.UserAccountGateway(_connectionstring);
            LostTimeDB.GoogleAccountGateway googleAccountGateaway = new LostTimeDB.GoogleAccountGateway(_connectionstring);

            LostTimeDB.UserAccount userAccount;
            LostTimeDB.UserAccount userAccount2;

            string testUserPseudonym = "TestFindByIDPseudonymTest3";
            string testUserEmail = "TestFindByIDEmailTest3";
            byte[] testUserPassword = Guid.NewGuid().ToByteArray();

            string googleID = "321abc";
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

        [Test]
        public void FindByUserByEmailTest()
        {
            LostTimeDB.UserAccountGateway userAccountGtw = new LostTimeDB.UserAccountGateway(_connectionstring);
            LostTimeDB.UserAccount userAccount;

            userAccountGtw.CreateNewUserAccount("Pseudo", "userPseudonym@gmail.com", Guid.NewGuid().ToByteArray(), DateTime.Now);
            userAccount = userAccountGtw.FindByUserEmail("userPseudonym@gmail.com");

            Assert.That(userAccount.UserPseudonym, Is.EqualTo("Pseudo"));

            userAccountGtw.DeleteUserAccountByName("Pseudo");

            userAccount = userAccountGtw.FindByUserEmail("userPseudonym@gmail.com");

            Assert.That(userAccount, Is.Null);
        }

        [Test]
        public void UpdatePasswordTest()
        {
            LostTimeDB.UserAccountGateway UserAccGtw = new LostTimeDB.UserAccountGateway(_connectionstring);
            LostTimeDB.UserAccount UserAcc;

            byte[] firstPassword = Guid.NewGuid().ToByteArray();
            UserAccGtw.CreateNewUserAccount("Pseudo", "Pseudo@gmail.com", firstPassword, DateTime.Now);
            UserAcc = UserAccGtw.FindByUserEmail("Pseudo@gmail.com");
            Assert.That(UserAcc.UserPseudonym, Is.EqualTo("Pseudo"));
            Assert.That(UserAcc.UserPassword, Is.EqualTo(firstPassword));

            byte[] secondPassword = Guid.NewGuid().ToByteArray();
            UserAccGtw.UpdatePassword(UserAcc.UserEmail, secondPassword);
            UserAcc = UserAccGtw.FindByName("Pseudo");
            Assert.That(UserAcc.UserPassword, Is.EqualTo(secondPassword));

            UserAccGtw.DeleteUserAccountByName("Pseudo");
            UserAcc = UserAccGtw.FindByName("Pseudo");
            Assert.That(UserAcc, Is.Null);
        }

        [Test]
        public void UpdateUserPermissionTest()
        {
            LostTimeDB.UserAccountGateway UserAccGtw = new LostTimeDB.UserAccountGateway(_connectionstring);
            LostTimeDB.UserAccount UserAcc;

            byte[] firstPassword = Guid.NewGuid().ToByteArray();
            UserAccGtw.CreateNewUserAccount("Pseudo", "Pseudo@gmail.com", firstPassword, DateTime.Now);
            UserAcc = UserAccGtw.FindByUserEmail("Pseudo@gmail.com");
            Assert.That(UserAcc.UserPseudonym, Is.EqualTo("Pseudo"));
            Assert.That(UserAcc.UserPermission, Is.EqualTo("USER"));

            UserAccGtw.UpdateUserPermission(UserAcc.UserID, "ADMIN");
            UserAcc = UserAccGtw.FindByName("Pseudo");
            Assert.That(UserAcc.UserPermission, Is.EqualTo("ADMIN"));

            UserAccGtw.DeleteUserAccountByName("Pseudo");
            UserAcc = UserAccGtw.FindByName("Pseudo");
            Assert.That(UserAcc, Is.Null);
        }

        [Test]
        public void GetAllUserAccountTest()
        {
            LostTimeDB.UserAccountGateway gtw = new LostTimeDB.UserAccountGateway(_connectionstring);
            LostTimeDB.UserAccount user2;

            int i = 0;
            

            for (i = 0; i < 5; i++)
            {
                gtw.CreateNewUserAccount("User" + i, "User" + i + "gmail.com", Guid.NewGuid().ToByteArray(), DateTime.Now);
            }

            IEnumerable<LostTimeDB.UserAccount> user = gtw.GetAll();

            i = 0;

            foreach(LostTimeDB.UserAccount n in user)
            {

                Assert.That(n.UserPseudonym, Is.EqualTo("User" + i));
                Assert.That(n.UserEmail, Is.EqualTo("User" + i + "gmail.com"));

                i++;
            }

            i = 0;

            foreach (LostTimeDB.UserAccount n in user)
            {
                gtw.DeleteUserAccountByName(n.UserPseudonym);
                user2 = gtw.FindByName("User" + i);
                Assert.That(user2, Is.Null);

                i++;
            }
        }
    }
}
