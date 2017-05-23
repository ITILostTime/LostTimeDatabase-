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
        readonly Random _random;
        readonly string _connectionstring = "Server=(local)\\SqlExpress; Database=LostTimeDB; Trusted_connection=true";




        public UserAccountGateawayTest()
        {
            _random = new Random();
        }

        [Test]
        public void Create_Update_Delete_UserAccount()
        {
            LostTimeDB.UserAccountGateaway userAccountGateaway = new LostTimeDB.UserAccountGateaway(_connectionstring);
            LostTimeDB.UserAccount userAccount;
            LostTimeDB.UserAccount userAccount2;

            string testUserPseudonym = "TestUserPseudonym";
            string testUserEmail = "TestUserEmail";
            string testUserPassword = "TestUserPassword";


            string TestUserUpdatePseudonym = "TestUserUpdatePseudonym";
            string TestUserUpdateEmail = "TestUserUpdateEmail";
            string TestUserUpdatePassword = "TestUserUpdatePassword";


            //      Creation UserAccount
            {
                userAccountGateaway.CreateNewUserAccount(testUserPseudonym, testUserEmail, testUserPassword, DateTime.Now);
            }

            //     Find User By Name
            {
                userAccount = userAccountGateaway.FindByName(testUserPseudonym);
                CheckUserAccount(userAccount, userAccount.UserID, testUserPseudonym, testUserEmail, testUserPassword, userAccount.UserAccountCreationDate);
            }

            //      Update UserAccount
            {

                userAccountGateaway.UpdateUserAccount(userAccount.UserID, TestUserUpdatePseudonym, TestUserUpdateEmail, TestUserUpdatePassword);
                userAccount2 = userAccountGateaway.FindByName(TestUserUpdatePseudonym);
                CheckUserAccount(userAccount2, userAccount2.UserID, TestUserUpdatePseudonym, TestUserUpdateEmail, TestUserUpdatePassword, userAccount2.UserAccountCreationDate);

            }

            //      Delete UserAcount
            {
                userAccountGateaway.DeleteUserAccount(TestUserUpdatePseudonym);
                userAccount = userAccountGateaway.FindByName(TestUserUpdatePseudonym);
                Assert.That(userAccount, Is.Null);
            }
            
        }

        [Test]
        public void FindByIDTest()
        {
            LostTimeDB.UserAccountGateaway userAccountGateaway = new LostTimeDB.UserAccountGateaway(_connectionstring);
            LostTimeDB.UserAccount userAccount;
            LostTimeDB.UserAccount userAccount2;

            string testUserPseudonym = "TestFindByIDPseudonym";
            string testUserEmail = "TestFindByIDEmail";
            string testUserPassword = "TestFindByIDPassword";

            userAccountGateaway.CreateNewUserAccount(testUserPseudonym, testUserEmail, testUserPassword, DateTime.Now);

            userAccount = userAccountGateaway.FindByName(testUserPseudonym);
            userAccount2 = userAccountGateaway.FindByID(userAccount.UserID);
            Assert.That(userAccount.UserID, Is.EqualTo(userAccount2.UserID));

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
