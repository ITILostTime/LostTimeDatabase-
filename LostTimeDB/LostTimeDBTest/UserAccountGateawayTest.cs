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
        public void can_create_find_update_and_delete_student()
        {
            LostTimeDB.UserAccountGateaway userAccountGateaway = new LostTimeDB.UserAccountGateaway(_connectionstring);

            // Information NewAccountTest
            string testUserID = "TestUserID";
            string testUserFirstname = "TestUserFirstname";
            string testUserLastname = "TestUserLastname";
            string testUserEmail = "TestUserEmail";
            string testUserPassword = "TestUserPassword";

            LostTimeDB.UserAccount userAccount;

            // Create UserAccount
            {
                userAccountGateaway.CreateNewUserAccount(testUserID, testUserFirstname, testUserLastname, testUserEmail, testUserPassword);
            }

            // Find UserAccount by firstname and lastname
            {
                userAccount = userAccountGateaway.FindByName(testUserFirstname, testUserLastname);
                CheckUserAccount(userAccount, testUserID, testUserFirstname, testUserLastname, testUserEmail, testUserPassword);
            }

            // Update UserAccount
            {
                testUserFirstname = "TestUserUpdateFirstname";
                testUserLastname = "TestUserUpdateLastName";
                testUserEmail = "TestUserUpdateEmail";
                testUserPassword = "TestUserUpdateEmail";

                userAccountGateaway.UpdateUserAccount(userAccount.UserID, testUserFirstname, testUserLastname, testUserEmail, testUserPassword);

                userAccount = userAccountGateaway.FindByName(testUserFirstname, testUserLastname);
                CheckUserAccount(userAccount, userAccount.UserID, userAccount.UserFirstname, userAccount.UserLastname, userAccount.UserEmail, userAccount.UserPassword);
            }

            // Delete UserAccount
            {
                userAccountGateaway.DeleteUserAccount(testUserID);
                userAccount = userAccountGateaway.FindByName(testUserFirstname, testUserLastname);
                Assert.That(userAccount, Is.Null);
            }
        }

        void CheckUserAccount(LostTimeDB.UserAccount userAccount, string userID, string userFirstname, string userLastname, string userEmail, string userPassword)
        {
            Assert.That(userAccount.UserID, Is.EqualTo(userID));
            Assert.That(userAccount.UserFirstname, Is.EqualTo(userFirstname));
            Assert.That(userAccount.UserLastname, Is.EqualTo(userLastname));
            Assert.That(userAccount.UserEmail, Is.EqualTo(userEmail));
            Assert.That(userAccount.UserPassword, Is.EqualTo(userPassword));
        }
    }
}
